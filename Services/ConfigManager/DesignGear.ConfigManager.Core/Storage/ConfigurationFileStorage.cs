using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.ModelPackage;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Web;

namespace DesignGear.ConfigManager.Core.Storage
{
    public class ConfigurationFileStorage : IConfigurationFileStorage
    {
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";
        private readonly string _designGearPackageFileName = "DesignGearPackageContents.json";
        //private readonly string _svfFileExtension = "svf";

        public ConfigurationFileStorage()
        {

        }

        public async Task<DesignGearModelPackage> GetPackageModelAsync(Guid productVersionId, Guid configurationId)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\model\\";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
            {
                var file = di.EnumerateFiles().FirstOrDefault();
                if (file != null)
                {
                    using (var archive = await Task.Run(() => ZipFile.OpenRead(file.FullName)))
                    {
                        var entry = archive.Entries.FirstOrDefault(x => x.Name == _designGearPackageFileName);
                        if (entry != null)
                            using (var stream = entry.Open())
                            {
                                string json = new StreamReader(stream).ReadToEnd();
                                return JsonConvert.DeserializeObject<DesignGearModelPackage>(json);
                            }
                    }
                }
            }
            return null;
        }

        public FileStreamDto GetSvf(Guid productVersionId, Guid configurationId, string fileName)
        {
            //var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\{fileName}";
            //var file = new FileInfo(filePath);
            //if (file.Exists)
            //    return GetFileStream(file);
            //return null;

            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\svf.zip";
            var file = new FileInfo(filePath);
            if (file.Exists)
            {
                var ms = new MemoryStream();
                using (var zip = ZipFile.OpenRead(filePath))
                {
                    fileName = HttpUtility.UrlDecode(fileName);
                    var svfFile = zip.Entries.FirstOrDefault(x => x.FullName == fileName);
                    if (svfFile != null)
                    {
                        svfFile.Open().CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var result = new FileStreamDto()
                        {
                            FileName = svfFile.Name,
                            Content = ms,
                            ContentType = "application/octet-stream"
                        };
                        //result.Length = result.Content.Length;
                        return result;
                    }
                }
            }
            return null;
        }

        public string GetSvfRootFileName(Guid productVersionId, Guid configurationId)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\";
            //var di = new DirectoryInfo(filePath);
            //if (di.Exists)
            //{
            //    var file = di.EnumerateFiles().FirstOrDefault(x => x.Extension == _svfFileExtension);
            //    if (file != null)
            //        return file.Name;
            //}
            //return null;

            var zipFilePath = Path.Combine(filePath, "svf.zip");
            var file = new FileInfo(zipFilePath);
            if (file.Exists)
            {
                using (var zip = ZipFile.OpenRead(zipFilePath))
                {
                    var svfFile = zip.Entries.FirstOrDefault(x => x.Name.Substring(x.Name.Length - 3) == "svf");
                    if (svfFile != null)
                            return svfFile.FullName;
                }
            }
            return null;
        }

        public FileStreamDto GetZipArchive(Guid productVersionId, Guid configurationId)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\model\\";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
            {
                var file = di.EnumerateFiles().FirstOrDefault();
                if (file != null)
                    return GetFileStream(file);
            }

            return null;
        }

        public void CopyZipArchive(Guid productVersionId, Guid sourceConfigurationId, Guid destinationConfigurationId, string json)
        {
            var sourceFilePath = $"{_fileBucket}{productVersionId}\\{sourceConfigurationId}\\model\\";
            var destinationFilePath = $"{_fileBucket}{productVersionId}\\{destinationConfigurationId}\\model\\";
            var di = new DirectoryInfo(sourceFilePath);
            if (di.Exists)
            {
                var file = di.EnumerateFiles().FirstOrDefault();
                if (file != null)
                {
                    var newFilePath = Path.Combine(destinationFilePath, file.Name);
                    file.CopyTo(newFilePath, true);
                    using (var archive = ZipFile.Open(newFilePath, ZipArchiveMode.Update))
                    {
                        var entry = archive.Entries.FirstOrDefault(x => x.Name == _designGearPackageFileName);
                        if (entry != null)
                        {
                            entry.Delete();
                            var demoFile = archive.CreateEntry(_designGearPackageFileName);

                            using (var entryStream = demoFile.Open())
                            using (var streamWriter = new StreamWriter(entryStream))
                            {
                                streamWriter.Write(json);
                            }
                        }
                    }
                }
            }
        }

        public async Task<DesignGearModelPackage> SaveConfigurationPackageAsync(ConfigurationPackageDto package)
        {
            var filePath = $"{_fileBucket}{package.ProductVersionId}\\{package.ConfigurationId}\\model\\";
            var di = new DirectoryInfo(filePath);
            if (!di.Exists)
                di.Create();
            else
            {
                //todo - delete all files inside the folder
            }
            var originalFileName = Path.GetFileName(package.ConfigurationPackage.FileName);
            var uniqueFilePath = Path.Combine(filePath, originalFileName);
            using (var fileStream = File.Create(uniqueFilePath))
            {
                await package.ConfigurationPackage.Content.CopyToAsync(fileStream);
            }

            //package.ConfigurationPackage.Content.Seek(0, SeekOrigin.Begin);
            //using (var archive = new ZipArchive(package.ConfigurationPackage.Content))
            using (var archive = ZipFile.OpenRead(uniqueFilePath))
            {
                var entry = archive.Entries.FirstOrDefault(x => x.Name == _designGearPackageFileName);
                if (entry != null)
                    using (var stream = entry.Open())
                    {
                        string json = new StreamReader(stream).ReadToEnd();
                        return JsonConvert.DeserializeObject<DesignGearModelPackage>(json);
                    }
            }
            return null;
        }

        public async Task SaveSvfAsync(Guid productVersionId, Guid configurationId, FileStreamDto svf)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\";
            var di = new DirectoryInfo(filePath);
            if (!di.Exists)
                di.Create();
            var originalFileName = Path.GetFileName(svf.FileName);
            var uniqueFilePath = Path.Combine(filePath, originalFileName);
            using(var fileStream = File.Create(uniqueFilePath))
            {
                await svf.Content.CopyToAsync(fileStream);
            }
        }

        public async Task SaveSvfAsync(Guid productVersionId, Guid configurationId, byte[] svf)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\";
            var di = new DirectoryInfo(filePath);
            if (!di.Exists)
                di.Create();
            var uniqueFilePath = Path.Combine(filePath, "svf.zip");
            await File.WriteAllBytesAsync(uniqueFilePath, svf);
        }

        private FileStreamDto GetFileStream(FileInfo file)
        {
            var result = new FileStreamDto
            {
                FileName = file.Name,
                Content = File.OpenRead(file.FullName),
                ContentType = "application/octet-stream"
            };
            //result.Length = result.Content.Length;
            return result;
        }

        public async Task DeleteConfigurationFilesAsync(Guid productVersionId, Guid configurationId)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
                di.Delete();
        }

        public async Task DeleteConfigurationFilesAsync(Guid productVersionId)
        {
            var filePath = $"{_fileBucket}{productVersionId}";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
                di.Delete();
        }
    }
}
