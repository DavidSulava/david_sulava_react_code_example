using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.ModelPackage;

namespace DesignGear.ConfigManager.Core.Storage.Interfaces {
    public interface IConfigurationFileStorage {
        // Получение JSON из модели
        Task<DesignGearModelPackage> GetPackageModelAsync(Guid productVersionId, Guid configurationId);

        // Получение файла SVF (по имени)
        FileStreamDto GetSvf(Guid productVersionId, Guid configurationId, string fileName);

        //Получение корневого файла для отображения SVF
        string GetSvfRootFileName(Guid productVersionId, Guid configurationId);

        // Сохранение модели на диск и получение JSON
        Task<DesignGearModelPackage> SaveConfigurationPackageAsync(ConfigurationPackageDto package);

        // Сохранение файлов для SVF
        Task SaveSvfAsync(Guid productVersionId, Guid configurationId, FileStreamDto svf);

        //Получение архива с моделью
        FileStreamDto GetZipArchive(Guid productVersionId, Guid configurationId);
    }
}