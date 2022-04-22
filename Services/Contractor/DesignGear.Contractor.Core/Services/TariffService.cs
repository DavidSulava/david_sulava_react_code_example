using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Services
{
    public class TariffService : ITariffService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public TariffService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<ICollection<TariffDto>> GetTariffsAsync()
        {
            return await _dataAccessor.Reader.Tariffs.ProjectTo<TariffDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
