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

        public IQueryable<AppBundle> AppBundles => _context.AppbBundles;
        public IQueryable<Configuration> Configurations => _context.Configurations;
        public IQueryable<ParameterDefinition> ParameterDefinitions => _context.ParameterDefinitions;
        public IQueryable<ValueOption> ValueOptions => _context.ValueOptions;

        public void Create<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
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
