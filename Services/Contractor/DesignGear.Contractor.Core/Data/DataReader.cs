using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Data
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

        public IQueryable<Organization> Organizations => _context.Organizations.AsNoTracking();
        public IQueryable<Product> Products => _context.Products.AsNoTracking();
        public IQueryable<Tariff> Tariffs => _context.Tariffs.AsNoTracking();
        public IQueryable<User> Users => _context.Users.AsNoTracking();
        public IQueryable<UserAssignment> UserAssignments => _context.UserAssignments.AsNoTracking();
    }
}
