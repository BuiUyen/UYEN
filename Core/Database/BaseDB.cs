using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;
using Medibox.Model;

namespace Medibox.Database
{
    public class BaseDB
    {
        //Constant
        private const String TAG = "BaseDB";

        //Private
        protected Object lockObject = new object();
        protected IBaseDao baseDAO = MediboxDatabaseUtility.GetDatabaseDAO();
    }
}