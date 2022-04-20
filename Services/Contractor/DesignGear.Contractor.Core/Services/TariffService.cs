using AutoMapper;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;

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

        public ICollection<TariffDto> GetTariffs()
        {
            var tariffList = _dataAccessor.Reader.Tariffs.ToList();
            var result = _mapper.Map<List<TariffDto>>(tariffList);
            return result;
        }
    }
}
