using DesignGear.Contractor.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Data
{
    public class DataAccessor
    {
        public DataEditor Editor { get; }
        public DataReader Reader { get; }
        //public UserInfo UserInfo { get; }

        public DataAccessor(DataEditor dataEditor, DataReader dataReader)//, UserInfo userInfo)
        {
            Editor = dataEditor;
            Reader = dataReader;
            //UserInfo = userInfo;
        }
    }
}
