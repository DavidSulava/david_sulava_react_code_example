using AutoMapper;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.ModelPackage;
using Newtonsoft.Json;

namespace DesignGear.Contractor.Core.Data
{
    public class ModelFileParsed
    {
        public Configuration Configuration { get; set; }

        public ModelFileParsed(Guid productVersionId, string json, IMapper mapper)
        {
            var model = JsonConvert.DeserializeObject<DesignGearModelPackage>(json);
            Configuration = mapper.Map<Configuration>(model.Configuration.FirstOrDefault());
            Configuration.ProductVersionId = productVersionId;
            Configuration.ParameterDefinitions = mapper.Map<ICollection<ParameterDefinition>>(model.Parameter.Rows);
            foreach (var param in Configuration.ParameterDefinitions)
            {
                param.ConfigurationId = Configuration.Id;
                param.ValueOptions = mapper.Map<ICollection<ValueOption>>(model.ValueOption.Where(x => x.ParameterId == param.ParameterId));
            }
        }
    }
}
