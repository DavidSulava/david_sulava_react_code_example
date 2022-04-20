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
            //todo Anton воспользоваться расширением маппера ProjectTo, чтобы оно сформировало нужную проекцию сразу
            //Это позволит не тянуть из базы все колонки таблицы, а только те, что нужны для формирования TariffDto
            //Может выглядеть так:
            // var tariffList = await _dataAccessor.Reader.Tariffs.PrjectTo<TariffDto>(_mapper.ConfigurationProvider).ToListAsync();
            // return tariffList;
            //
            //В других методах также использовать ProjectTo при чтении данных в dto
            var tariffList = _dataAccessor.Reader.Tariffs.ToList();
            var result = _mapper.Map<List<TariffDto>>(tariffList);
            return result;
        }
    }
}
