using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Medibox.Database;
using Medibox.Model;
using Sanita.Utility.Database.BaseDao;
using Sanita.Utility.Database.Utility;
using Sanita.Utility.ExtendedThread;

namespace Medibox.Presenter
{
    public class BasePresenter
    {
        private const String TAG = "BasePresenter";

        //Base DAO      
        protected static IBaseDao baseDAO = MediboxDatabaseUtility.GetDatabaseDAO();        
    }
}
