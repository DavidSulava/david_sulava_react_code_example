using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Compression;
using System.Data;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Services
{
    public class ProductVersionService : IProductVersionService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;
        private readonly IConfigManagerCommunicator _configManagerService;
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";
        private readonly string _designGearPackageFileName = "DesignGearPackageContents.json";

        public ProductVersionService(IMapper mapper, DataAccessor dataAccessor, IConfigManagerCommunicator configManagerService)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
            _configManagerService = configManagerService;
        }

        public async Task<Guid> CreateProductVersionAsync(ProductVersionCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newItem = _mapper.Map<ProductVersion>(create);
            _dataAccessor.Editor.Create(newItem);
            await _dataAccessor.Editor.SaveAsync();

            var newConfiguration = _mapper.Map<VmConfigurationCreate>(create);
            newConfiguration.ProductVersionId = newItem.Id;
            newConfiguration.OrganizationId = (await _dataAccessor.Reader.Products.FirstAsync(x => x.Id == create.ProductId)).OrganizationId;
            await _configManagerService.CreateConfigurationAsync(newConfiguration);
            
            return newItem.Id;
        }

        public async Task UpdateProductVersionAsync(ProductVersionUpdateDto update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var item = await _dataAccessor.Editor.ProductVersions.FirstOrDefaultAsync(x => x.Id == update.Id);
            if (item == null)
            {
                throw new EntityNotFoundException<ProductVersion>(update.Id);
            }

            _mapper.Map(update, item);

            await SaveImageFilesAsync(update.Id, update.ImageFiles);

            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task RemoveProductVersionAsync(Guid id)
        {
            var item = await _dataAccessor.Editor.ProductVersions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                throw new EntityNotFoundException<ProductVersion>(id);
            }

            _dataAccessor.Editor.Delete(item);
            await _dataAccessor.Editor.SaveAsync();
            DeleteFiles(id);
        }

        public async Task<TResult> GetProductVersionsByProductAsync<TResult>(Guid productId, Func<IQueryable<ProductVersionItemDto>, TResult> resultBuilder)
        {
            if (resultBuilder == null)
            {
                throw new ArgumentNullException(nameof(resultBuilder));
            }
            var query = _dataAccessor.Reader.ProductVersions.Where(x => x.ProductId == productId).ProjectTo<ProductVersionItemDto>(_mapper.ConfigurationProvider);
            var result = resultBuilder(query);
            return await Task.FromResult(result);
        }

        public async Task<ProductVersionDto> GetProductVersionAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.ProductVersions.ProjectTo<ProductVersionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<ProductVersion>(id);
            }

            result.ImageFiles = GetImageFileNames(id);

            return result;
        }

        private async Task SaveImageFilesAsync(Guid id, ICollection<AttachmentDto> imageFiles)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                var filePath = $"{_fileBucket}{id}\\images\\";
                var di = new DirectoryInfo(filePath);
                if (!di.Exists)
                    di.Create();
                else
                    foreach (var file in di.EnumerateFiles())
                        file.Delete();

                foreach (var image in imageFiles)
                {
                    var originalFileName = Path.GetFileName(image.FileName);
                    var uniqueFilePath = Path.Combine(filePath, originalFileName);
                    await File.WriteAllBytesAsync(uniqueFilePath, image.Content);
                }
            }
        }

        private async Task SaveModelFileAsync(Guid id, Guid configurationId, AttachmentDto modelFile)
        {
            var filePath = $"{_fileBucket}{id}\\{configurationId}\\";
            var di = new DirectoryInfo(filePath);
            if (!di.Exists)
                di.Create();
            else
                foreach (var file in di.EnumerateFiles())
                    file.Delete();

            var originalFileName = Path.GetFileName(modelFile.FileName);
            var uniqueFilePath = Path.Combine(filePath, originalFileName);
            await File.WriteAllBytesAsync(uniqueFilePath, modelFile.Content);

        }

        private void DeleteFiles(Guid id)
        {
            var filePath = $"{_fileBucket}{id}";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
                di.Delete(true);
        }

        private async Task<AttachmentDto> GetFileAsync(FileInfo file)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(file.FullName, out contentType);
            var result = new AttachmentDto
            {
                FileName = file.Name,
                Content = await File.ReadAllBytesAsync(file.FullName),
                ContentType = contentType ?? "application/octet-stream"
            };
            result.Length = result.Content.Length;
            return result;
        }

        private ICollection<string> GetImageFileNames(Guid id)
        {
            var result = new List<string>();
            var filePath = $"{_fileBucket}{id}\\images\\";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
                foreach (var file in di.EnumerateFiles())
                    result.Add(file.Name);

            return result;
        }

        public async Task<AttachmentDto> GetImageFileAsync(Guid id, string fileName)
        {
            var filePath = $"{_fileBucket}{id}\\images\\";
            var di = new DirectoryInfo(filePath);
            if (di.Exists)
            {
                var file = di.EnumerateFiles().FirstOrDefault(x => x.Name == fileName);
                if (file != null)
                    return await GetFileAsync(file);
            }

            return null;
        }

        private ModelFileParsed? ParseModelFile(Guid id, AttachmentDto modelFile)
        {
            using (var streamContent = new MemoryStream(modelFile.Content))
            using (var archive = new ZipArchive(streamContent))
            {
                var entry = archive.Entries.FirstOrDefault(x => x.Name == _designGearPackageFileName);
                if (entry != null)
                    using (var stream = entry.Open())
                    {
                        string json = new StreamReader(stream).ReadToEnd();
                        return new ModelFileParsed(id, json, _mapper);
                    }
            }

            return null;
        }
    }
}
