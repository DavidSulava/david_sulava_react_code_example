using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services
{
    internal class TariffService : ITariffService
    {
        private readonly DataAccessor _dataAccessor;

        public TariffService(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public ICollection<TariffDto> GetTariffs()
        {
            return _dataAccessor.Reader.Tariffs.Select(x => new TariffDto
            {
                Name = x.Name
            }).ToList();
        }
    }
}
