using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.ModelPackage;
using Newtonsoft.Json;
using System.IO.Compression;

namespace DesignGear.ConfigManager.Core.Storage
{
    public class ConfigurationFileStorage : IConfigurationFileStorage
    {
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";
        private readonly string _designGearPackageFileName = "DesignGearPackageContents.json";
        private readonly string _svfFileExtension = "svf";

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
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\{fileName}";
            var file = new FileInfo(filePath);
            if (file.Exists)
                return GetFileStream(file);
            return null;
        }

        public string GetSvfRootFileName(Guid productVersionId, Guid configurationId)
        {
            var filePath = $"{_fileBucket}{productVersionId}\\{configurationId}\\svf\\";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
            {
                var file = di.EnumerateFiles().FirstOrDefault(x => x.Extension == _svfFileExtension);
                if (file != null)
                    return file.Name;
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

        public async Task<DesignGearModelPackage> SaveConfigurationPackageAsync(ConfigurationPackageDto package)
        {
            var filePath = $"{_fileBucket}{package.ProductVersionId}\\{package.ConfigurationId}\\model\\";
            var di = new DirectoryInfo(filePath);
            if (!di.Exists)
                di.Create();
            var originalFileName = Path.GetFileName(package.ConfigurationPackage.FileName);
            var uniqueFilePath = Path.Combine(filePath, originalFileName);
            using (var fileStream = File.Create(uniqueFilePath))
            {
                await package.ConfigurationPackage.CopyToAsync(fileStream);
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
            var originalFileName = Path.GetFileName(svf.FileName);
            var uniqueFilePath = Path.Combine(filePath, originalFileName);
            using(var fileStream = File.Create(uniqueFilePath))
            {
                await svf.Content.CopyToAsync(fileStream);
            }
        }

        private FileStreamDto GetFileStream(FileInfo file)
        {
            var result = new FileStreamDto
            {
                FileName = file.Name,
                Content = File.OpenRead(file.FullName),
                ContentType = "application/octet-stream"
            };
            result.Length = result.Content.Length;
            return result;
        }
    }
}
