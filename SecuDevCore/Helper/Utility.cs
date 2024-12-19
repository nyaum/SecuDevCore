//using FrameWork.DB;
using SecuDev.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using WebAdmin.Models;
using System.Net;
using CoreDAL.Configuration.Interface;
using SingletonManager;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using System.Threading.Tasks;

namespace SecuDev.Helper
{

    public class Utility
    {

        //public static IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(MvcApplication.ConnDB);
        public static IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);

        /// <summary>
        /// <para>Get Now DateTime </para>
        /// </summary>
        /// <returns></returns>
        public static string GetNowDate()
        {
            string Rtn = "";

            Rtn = DateTime.Now.ToString();

            return Rtn;
        }

        /// <summary>
        /// <para>DateTime Formatter</para>
        /// <para>format 1 : yyyy-MM-dd</para>
        /// <para>format 2 : yyyy-MM-dd HH:mm:ss</para>
        /// <para>format 3 : yyyy-MM-dd dddd HH:mm:ss</para>
        /// <para>format 4 : yyyy-MM-dd tt hh:mm:ss</para>
        /// <para>format 5 : yyyy</para>
        /// <para>format 6 : yyyy년 MM월 dd일</para>
        /// <para>format 7 : yyyyMMddHHmmss</para>
        /// <para>format 8 : DateTime Differ</para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTimeFormat(string date, int format)
        {
            string Rtn = "";


            if (date != "" && date != null)
            {

                DateTime dt = Convert.ToDateTime(date);

                switch (format)
                {
                    case 1:
                        Rtn = String.Format("{0:yyyy-MM-dd}", dt);
                        break;
                    case 2:
                        Rtn = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
                        break;
                    case 3:
                        Rtn = String.Format("{0:yyyy-MM-dd dddd HH:mm:ss}", dt);
                        break;
                    case 4:
                        Rtn = String.Format("{0:yyyy-MM-dd tt hh:mm:ss}", dt);
                        break;
                    case 5:
                        Rtn = String.Format("{0:yyyy}", dt);
                        break;
                    case 6:
                        Rtn = String.Format("{0:yyyy년 MM월 dd일}", dt);
                        break;
                    case 7:
                        Rtn = String.Format("{0:yyyyMMddHHmmss}", dt);
                        break;
                    case 8:
                        Rtn = String.Format("{0:yyMMdd}", dt);
                        break;
                    case 9:

                        TimeSpan dateDiff = Convert.ToDateTime(GetNowDate()) - dt;

                        double diffDay = dateDiff.Days;

                        double diffMonth = Math.Truncate(diffDay / 30);

                        double diffYear = Math.Truncate(diffMonth / 12);

                        double diffHour = dateDiff.Hours;
                        double diffMinute = dateDiff.Minutes;
                        double diffSecond = dateDiff.Seconds;

                        if (diffYear > 0)
                        {
                            Rtn = String.Format("{0}년 전", diffYear);
                            break;
                        }
                        
                        if (diffMonth > 0)
                        {
                            Rtn = String.Format("{0}달 전", diffMonth);
                            break;
                        }

                        if (diffDay > 0)
                        {
                            Rtn = String.Format("{0}일 전", diffDay);
                            break;
                        }

                        if (diffHour > 0)
                        {
                            Rtn = String.Format("{0}시간 전", diffHour);
                            break;
                        }

                        if (diffMinute > 0)
                        {
                            Rtn = String.Format("{0}분 전", diffMinute);
                            break;
                        }

                        if (diffSecond > 0)
                        {
                            Rtn = String.Format("{0}초 전", diffSecond);
                            break;
                        }

                        break;
                }
            }

            return Rtn;

        }

        /// <summary>
        /// <para>Get Category List</para>
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetCategoryList()
        {

            Dictionary<string, object> param = new Dictionary<string, object>
            {

                { "Type", "Category" }

            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_LIST", param);

            DataSet ds = result.DataSet;

            List<Category> list = new List<Category>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                list.Add(i.ToObject<Category>());

            }

            return list;
        }

        /// <summary>
        /// 사용자 아이피 주소
        /// </summary>
        /// <returns></returns>
        public static string GetIP4Address()
        {
            string strIP4Address = String.Empty;

            foreach(IPAddress objIP in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if(objIP.AddressFamily.ToString() == "InterNetwork")
                {
                    strIP4Address = objIP.ToString();
                    break;
                }
            }
            return strIP4Address;
        }
    }
}