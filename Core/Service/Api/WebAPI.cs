using Medibox.Model;
using Medibox.Presenter;
using Medibox.Service.Model;
using Medibox.Utility;
using Sanita.Utility;
using Sanita.Utility.Encryption;
using Sanita.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using Medibox.Database;
using System.IO;

namespace Medibox.Service.Api
{
    public class WebAPI : ApiBase, IWebAPI
    {
        private const String TAG = "WebAPI";

        public API_TroLyAoData_Out TroLyAo_Execute(API_TroLyAoData_In data)
        {
            //Xử lý input
            data.Data = data.Data ?? "";
            data.Data = data.Data.ToLower();
            data.Data = data.Data.Trim();
            data.Data = data.Data.TrimSpace();

            data.Data = data.Data.Replace("i", "i");
            data.Data = data.Data.Replace("ìì", "ì");
            data.Data = data.Data.Replace("íí", "í");

            data.Data = data.Data.Replace("ơ", "ơ");
            data.Data = data.Data.Replace("ờờ", "ờ");
            data.Data = data.Data.Replace("ớớ", "ớ");

            data.Data = data.Data.Replace("uu", "u");
            data.Data = data.Data.Replace("úú", "ú");
            data.Data = data.Data.Replace("ùù", "ù");

            data.Data = data.Data.Replace("ưư", "ư");
            data.Data = data.Data.Replace("ừừ", "ừ");
            data.Data = data.Data.Replace("ứứ", "ứ");

            data.Data = data.Data.Replace("yy", "y");
            data.Data = data.Data.Replace("ýý", "ý");
            data.Data = data.Data.Replace("ỳỳ", "ỳ");

            //Output
            API_TroLyAoData_Out mAPI_TroLyAoData_Out = new API_TroLyAoData_Out();

            //Check
            if (data.Data.Contains("thời tiết") && data.Data.Contains("hôm nay"))
            {
                String thu = "";
                if (SystemInfo.NOW.DayOfWeek == DayOfWeek.Sunday)
                {
                    thu = String.Format("chủ nhật");
                }
                else
                {
                    thu = String.Format("thứ {0}", (int)SystemInfo.NOW.DayOfWeek + 1);
                }

                mAPI_TroLyAoData_Out.Data = String.Format("Dự báo thời tiết hôm nay ở Quan Khê, Hải Dương {0} ngày {1:dd/MM} có mưa rào nhẹ, nhiệt độ từ 26 đến 32 độ, độ ẩm 94%", thu, SystemInfo.NOW);
            }
            else if ((data.Data.Contains("thời tiết") && data.Data.Contains("ngày mai")) || data.Data.Contains("còn ngày mai"))
            {
                String thu = "";
                if (SystemInfo.NOW.AddDays(1).DayOfWeek == DayOfWeek.Sunday)
                {
                    thu = String.Format("chủ nhật");
                }
                else
                {
                    thu = String.Format("thứ {0}", (int)SystemInfo.NOW.AddDays(1).DayOfWeek + 1);
                }

                mAPI_TroLyAoData_Out.Data = String.Format("Dự báo thời tiết ngày mai ở Quan Khê, Hải Dương {0} ngày {1:dd/MM} trời nhiều mây và nắng nhẹ, nhiệt độ từ 26 đến 33 độ, độ ẩm 76%", thu, SystemInfo.NOW.AddDays(1));
            }
            else if (data.Data.Contains("mấy giờ rồi") || data.Data.Contains("bây giờ là mấy giờ"))
            {
                mAPI_TroLyAoData_Out.Data = String.Format("Thời gian là {0:HH:mm} phút", SystemInfo.NOW);
            }
            else if (data.Data.Contains("hôm nay") && data.Data.Contains("thứ mấy"))
            {
                if (SystemInfo.NOW.DayOfWeek == DayOfWeek.Sunday)
                {
                    mAPI_TroLyAoData_Out.Data = String.Format("Hôm nay là chủ nhật");
                }
                else
                {
                    mAPI_TroLyAoData_Out.Data = String.Format("Hôm nay là thứ {0}", (int)SystemInfo.NOW.DayOfWeek + 1);
                }
            }
            else if (data.Data.Contains("ngày mai") && data.Data.Contains("thứ mấy"))
            {
                if (SystemInfo.NOW.AddDays(1).DayOfWeek == DayOfWeek.Sunday)
                {
                    mAPI_TroLyAoData_Out.Data = String.Format("Ngày mai là chủ nhật");
                }
                else
                {
                    mAPI_TroLyAoData_Out.Data = String.Format("Ngày mai là thứ {0}", (int)SystemInfo.NOW.AddDays(1).DayOfWeek + 1);
                }
            }
            else if (data.Data.EqualText("bạn là ai") || data.Data.EqualText("bạn là cái gì"))
            {
                mAPI_TroLyAoData_Out.Data = "Tôi là trợ lý ảo của bạn đây";
            }
            else if (data.Data.EqualText("tên bạn là gì"))
            {
                mAPI_TroLyAoData_Out.Data = "Tôi chưa tự giới thiệu sao ? Tên tôi là Daisy !";
            }
            else if (data.Data.Contains("bạn biết hát không") || data.Data.Contains("bạn có biết hát không") || data.Data.Contains("hát cho tôi nghe"))
            {
                mAPI_TroLyAoData_Out.Data += "<speak>";
                mAPI_TroLyAoData_Out.Data += "<p>";

                mAPI_TroLyAoData_Out.Data += "<s>Xin lỗi, giờ tôi vẫn chưa biết hát.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Thay vào đó, tôi có thể đọc lời bài hát cho bạn tự tưởng tượng theo.</s>";
                mAPI_TroLyAoData_Out.Data += "<break time=\"300ms\"/>";

                mAPI_TroLyAoData_Out.Data += "<s>Kìa con bướm vàng.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Kìa con bướm vàng.</s>";

                mAPI_TroLyAoData_Out.Data += "<s>Xòe đôi cánh.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Xòe đôi cánh.</s>";

                mAPI_TroLyAoData_Out.Data += "<s>Tung cánh bay năm ba vòng.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Tung cánh bay năm ba vòng.</s>";

                mAPI_TroLyAoData_Out.Data += "<s>Em ngồi xem...</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Em ngồi xem...</s>";

                mAPI_TroLyAoData_Out.Data += "</p>";
                mAPI_TroLyAoData_Out.Data += "</speak>";
            }
            else if (data.Data.Contains("bạn đang ở đâu"))
            {
                mAPI_TroLyAoData_Out.Data += "<speak>";
                mAPI_TroLyAoData_Out.Data += "<p>";

                mAPI_TroLyAoData_Out.Data += "<s>Tôi ở trong thiết bị này.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>cả máy tính bảng có ngay khi cần.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Google home cũng có phần.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Ôi nhiều nhà quá phân vân chọn hoài.</s>";

                mAPI_TroLyAoData_Out.Data += "</p>";
                mAPI_TroLyAoData_Out.Data += "</speak>";
            }
            else if (data.Data.Contains("truyện cười") || data.Data.Contains("chuyện cười"))
            {
                mAPI_TroLyAoData_Out.Data += "<speak>";
                mAPI_TroLyAoData_Out.Data += "<p>";
                
                mAPI_TroLyAoData_Out.Data += "<s>Được, bạn nghe nhé !</s>";
                mAPI_TroLyAoData_Out.Data += "<break time=\"300ms\"/>";

                mAPI_TroLyAoData_Out.Data += "<s>Trong giờ địa lý, thầy hỏi trò.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Em hãy nói ba lý do khiến em chắc rằng trái đất hình cầu.</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Thưa thầy, bố em nói thế, mẹ em nói thế và thầy cũng nói thế ạ !</s>";

                mAPI_TroLyAoData_Out.Data += "<audio src=\"https://ia601507.us.archive.org/31/items/google_cuoi/google_cuoi.ogg\"/>";

                mAPI_TroLyAoData_Out.Data += "</p>";
                mAPI_TroLyAoData_Out.Data += "</speak>";
            }
            else if (data.Data.Contains("truyện khác") || data.Data.Contains("chuyện khác"))
            {
                mAPI_TroLyAoData_Out.Data += "<speak>";
                mAPI_TroLyAoData_Out.Data += "<p>";
                
                mAPI_TroLyAoData_Out.Data += "<s>Được, nghe tôi kể đây này !</s>";                
                mAPI_TroLyAoData_Out.Data += "<break time=\"300ms\"/>";

                mAPI_TroLyAoData_Out.Data += "<s>Ngày đầu tiên bé đi học về, bố mẹ hỏi. Ở lớp thế nào con ?</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Vui lắm bố ạ, cô giáo con xinh lắm !</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Thế cô dạy con những gì ?</s>";
                mAPI_TroLyAoData_Out.Data += "<s>Cô chẳng biết gì cả ! Cái gì cũng phải hỏi : Em nào cho cô biết nào.</s>";

                mAPI_TroLyAoData_Out.Data += "<audio src=\"https://ia601507.us.archive.org/31/items/google_cuoi/google_cuoi.ogg\"/>";                

                mAPI_TroLyAoData_Out.Data += "</p>";
                mAPI_TroLyAoData_Out.Data += "</speak>";
            }
            else if (data.Data.EqualText("bật đèn phòng khách"))
            {
                mAPI_TroLyAoData_Out.Data = "OK, đã bật đèn phòng khách";
            }
            else if (data.Data.EqualText("tắt đèn phòng khách"))
            {
                mAPI_TroLyAoData_Out.Data = "OK, đã tắt đèn phòng khách";
            }
            else if (data.Data.EqualText("bật điều hòa") || data.Data.EqualText("tôi nóng quá"))
            {
                mAPI_TroLyAoData_Out.Data = "OK, đã bật điều hòa";
            }
            else if (data.Data.EqualText("để điều hòa 26 độ"))
            {
                mAPI_TroLyAoData_Out.Data = "OK, đã để điều hòa 26 độ C";
            }

            //Return
            if (String.IsNullOrEmpty(mAPI_TroLyAoData_Out.Data))
            {
                mAPI_TroLyAoData_Out.Data = "Xin lỗi... Tôi không hiểu !";
            }

            mAPI_TroLyAoData_Out.ID = Sanita.Utility.Encryption.CryptorEngine.CreateMD5Hash(mAPI_TroLyAoData_Out.Data);
            mAPI_TroLyAoData_Out.Language = "vi-VN";

            return mAPI_TroLyAoData_Out;
        }
    }
}
