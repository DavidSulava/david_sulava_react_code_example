﻿using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<Guid> CreateConfigurationAsync(ConfigurationCreateDto Configuration);

        Task UpdateConfigurationAsync(ConfigurationUpdateDto Configuration);

        Task RemoveConfigurationAsync(Guid id);

        Task<ICollection<ConfigurationDto>> GetConfigurationsByProductVersionAsync(Guid productVersionId);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);
    }
}