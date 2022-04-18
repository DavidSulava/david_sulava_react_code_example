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
    }
}
