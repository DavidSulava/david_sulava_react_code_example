using DesignGear.ConfigManager.Core.Data.Entity;

namespace DesignGear.ConfigManager.Core.Data
{
    public class DataEditor
    {
        private readonly DataContext _context;
        //private readonly UserInfo _userInfo;

        public static DataEditor Create(DataContext context)//, UserInfo userInfo)
        {
            return new DataEditor(context);//, userInfo);
        }

        public DataEditor(DataContext context)//, UserInfo userInfo)
        {
            _context = context;
            //_userInfo = userInfo;
        }

        public IQueryable<AppBundle> AppBundles => _context.AppBundles;
        public IQueryable<ComponentDefinition> ComponentDefinitions => _context.ComponentDefinitions;
        public IQueryable<Configuration> Configurations => _context.Configurations;
        public IQueryable<ConfigurationInstance> ConfigurationInstances => _context.ConfigurationInstances;
        public IQueryable<FileItem> FileItems => _context.FileItems;
        public IQueryable<ParameterDefinition> ParameterDefinitions => _context.ParameterDefinitions;
        public IQueryable<ValueOption> ValueOptions => _context.ValueOptions;

        public void Create<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public async Task CreateAsync<T>(T entity) where T : class {
            await _context.Set<T>().AddAsync(entity);
        }


        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
