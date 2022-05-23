using DesignGear.ConfigManager.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.ConfigManager.Core.Data
{
    public class DataReader
    {
        private readonly DataContext _context;
        //private readonly UserInfo _userInfo;

        public static DataReader Create(DataContext context)//, UserInfo userInfo)
        {
            return new DataReader(context);//, userInfo);
        }

        public DataReader(DataContext context)//, UserInfo userInfo)
        {
            _context = context;
            //_userInfo = userInfo;
        }

        public IQueryable<AppBundle> AppBundles => _context.AppBundles.AsNoTracking();
        public IQueryable<ComponentDefinition> ComponentDefinitions => _context.ComponentDefinitions.AsNoTracking();
        public IQueryable<Configuration> Configurations => _context.Configurations.AsNoTracking();
        public IQueryable<ConfigurationInstance> configurationInstances => _context.ConfigurationInstances.AsNoTracking();
        public IQueryable<ParameterDefinition> ParameterDefinitions => _context.ParameterDefinitions.AsNoTracking();
        public IQueryable<ValueOption> ValueOptions => _context.ValueOptions.AsNoTracking();
    }
}
