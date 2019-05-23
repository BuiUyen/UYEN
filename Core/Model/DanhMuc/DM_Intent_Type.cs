using System;
using System.Collections.Generic;
using System.Linq;

namespace Medibox.Model
{
    [Serializable()]
    public class DM_Intent_Type
    {        
        public static readonly int THOI_GIAN = 1;
        public static readonly int NGAY_THANG = 2;
        public static readonly int THOI_TIET = 3;
        public static readonly int KE_CHUYEN = 4;
        public static readonly int LINH_TINH = 5;
        public static readonly int TIN_TUC = 6;
        public static readonly int HOME_CONTROL = 7;

        public int DM_Intent_TypeDBID { get; set; }        
        public int DM_Intent_TypeID { get; set; }
        public string DM_Intent_TypeName { get; set; }
        public int DM_Intent_TypeHardcode { get; set; }
        public int DM_Intent_TypeDisable { get; set; }

        //Contructor
        public DM_Intent_Type()
        {
            DM_Intent_TypeHardcode = 0;
            DM_Intent_TypeDisable = 0;
        }

        public DM_Intent_Type(int _id, String _name, int _DM_Intent_TypeHardcode)
        {
            DM_Intent_TypeID = _id;
            DM_Intent_TypeName = _name;
            DM_Intent_TypeHardcode = _DM_Intent_TypeHardcode;
            DM_Intent_TypeDisable = 0;
        }

        //Method
        public static int GetID(Object data)
        {
            if (data == null)
            {
                return 0;
            }
            if (!(data is DM_Intent_Type))
            {
                return 0;
            }
            return (data as DM_Intent_Type).DM_Intent_TypeID;
        }
        public static DM_Intent_Type GetDefault(Object list, int _id)
        {
            if (list == null)
            {
                return null;
            }

            if (!(list is IList<DM_Intent_Type>))
            {
                return null;
            }

            IList<DM_Intent_Type> list_data = list as IList<DM_Intent_Type>;
            return list_data.FirstOrDefault(p => p.DM_Intent_TypeID == _id);
        }

        public static DM_Intent_Type GetDefault(int _id)
        {
            DM_Intent_Type data = GetDefaultList(0).FirstOrDefault(p => p.DM_Intent_TypeID == _id);
            if (data == null)
            {
                data = new DM_Intent_Type();
            }

            return data;
        }

        private static IList<DM_Intent_Type> _DefaultList = null;
        public static void InitDefaultList(IList<DM_Intent_Type> list_data)
        {
            _DefaultList = null;
            GetDefaultList(1);

            foreach (DM_Intent_Type data in list_data)
            {
                DM_Intent_Type checkdata = _DefaultList.FirstOrDefault(p => p.DM_Intent_TypeID == data.DM_Intent_TypeID);
                if (checkdata != null)
                {
                    checkdata.DM_Intent_TypeDBID = data.DM_Intent_TypeDBID;
                    checkdata.DM_Intent_TypeID = data.DM_Intent_TypeID;
                    checkdata.DM_Intent_TypeName = data.DM_Intent_TypeName;
                    checkdata.DM_Intent_TypeDisable = data.DM_Intent_TypeDisable;
                    checkdata.DM_Intent_TypeHardcode = data.DM_Intent_TypeHardcode;
                }
                else
                {
                    _DefaultList.Add(data);
                }
            }
        }

        public static IList<DM_Intent_Type> GetDefaultList(int include_deactive)
        {
            if (_DefaultList != null)
            {
                if (include_deactive == 0)
                {
                    //Deactive
                    _DefaultList =
                        (from p in _DefaultList
                         where p.DM_Intent_TypeDisable == 0
                         select p).ToList();
                }
                return _DefaultList;
            }
            _DefaultList = new List<DM_Intent_Type>();

            _DefaultList.Add(new DM_Intent_Type(THOI_GIAN, "Thời gian".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(NGAY_THANG, "Ngày tháng".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(THOI_TIET, "Thời tiết".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(KE_CHUYEN, "Kể chuyện".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(LINH_TINH, "Linh tinh".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(TIN_TUC, "Tin tức".Translate(), 1));
            _DefaultList.Add(new DM_Intent_Type(HOME_CONTROL, "Điều khiển thiết bị".Translate(), 1));

            return _DefaultList;
        }
    }
}
