using DesignGear.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface ITariffService
    {
        Task<ICollection<TariffDto>> GetTariffs();
    }
}
