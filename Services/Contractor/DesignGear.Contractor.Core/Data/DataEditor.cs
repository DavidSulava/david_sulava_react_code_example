using DesignGear.Contractor.Core.Data.Entity;

namespace DesignGear.Contractor.Core.Data
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

        public IQueryable<Organization> Organizations => _context.Organizations;
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<ProductVersion> ProductVersions => _context.ProductVersions;
        public IQueryable<Tariff> Tariffs => _context.Tariffs;
        public IQueryable<User> Users => _context.Users;
        public IQueryable<UserAssignment> UserAssignments => _context.UserAssignments;
        public IQueryable<ProductVersionPreview> ProductVersionPreviews => _context.ProductVersionPreviews;

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
