using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;
using SecuDev.Helper;
using CoreDAL.Configuration.Interface;
using CryptoManager;
using SecuDev;
using SingletonManager;
using CoreDAL.ORM;
using SecuDevCore.Models;
using SecuDev.Models;
using System.Data;
using CoreDAL.ORM.Extensions;

namespace SecuDevCore.Controllers
{
    public class ScheduleController : Controller
    {
        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.SHA256);

        private readonly IWebHostEnvironment _env;

        public ScheduleController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Schedule> slist = new List<Schedule>();

            string XmlDir = $"{_env.ContentRootPath}/Upload/Data/RestDeInfo.xml";
            string XmlNode = "/response/body/items/item";

            string url = "http://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo";
            url += "?ServiceKey=" + "w7Ycwo9qcSFEoKkPtsvvg1ww8vxweOXvChmlMql3HZxutjR%2FYbmn7vWJONRUy25Zozng3hSvyKOGMM5glY%2BRWw%3D%3D";
            url += "&solYear=" + Utility.DateTimeFormat(Utility.GetNowDate(), 5);
            url += "&numOfRows=50";

            if (System.IO.File.Exists(XmlDir))
            {

                var info = new System.IO.FileInfo(XmlDir);

                var LastUpdate = info.LastWriteTime;

                TimeSpan dateDiff = Convert.ToDateTime(Utility.GetNowDate()) - LastUpdate;

                double diffDay = dateDiff.Days;

                if (diffDay >= 7)
                {
                    var xmlList = XmlSave(url, XmlDir, XmlNode);

                    slist = xmlList.Result;

                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(XmlDir);

                    XmlNodeList xmlList = doc.SelectNodes(XmlNode);

                    foreach (XmlNode data in xmlList)
                    {

                        Schedule s = new Schedule();

                        s.title = data.SelectSingleNode("dateName").InnerText;
                        s.start = data.SelectSingleNode("locdate").InnerText;
                        s.allDay = true;

                        slist.Add(s);

                    }

                }

            }
            else if (!System.IO.File.Exists(XmlDir))
            {

                var xmlList = XmlSave(url, XmlDir, XmlNode);

                slist = xmlList.Result;
            }

            // DB 가져오기

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_SCHEDULE_LIST", param);

            DataSet ds = result.DataSet;

            List<Schedule> list = new List<Schedule>();

            foreach (DataRow i in ds.Tables[0].Rows)
            {
                slist.Add(i.ToObject<Schedule>());
            }

            object a = JToken.Parse(JsonConvert.SerializeObject(slist));

            ViewBag.Schedule = a;

            return View();

        }

        /// <summary>
        /// 공휴일 XML로 저장 후, List로 가져오기
        /// </summary>
        /// <param name="url"></param>
        /// <param name="XmlDir"></param>
        /// <param name="XmlNode"></param>
        /// <returns></returns>
        public async Task<List<Schedule>> XmlSave(string url, string XmlDir, string XmlNode)
        {
            List<Schedule> slist = new List<Schedule>();

            using (var client = new HttpClient())
            {

                var response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                XmlDocument xml = new XmlDocument();

                xml.LoadXml(result);

                XmlNodeList xmlList = xml.SelectNodes(XmlNode);

                xml.Save(XmlDir);

                foreach (XmlNode data in xmlList)
                {

                    Schedule s = new Schedule();

                    s.title = data.SelectSingleNode("dateName").InnerText;
                    s.start = data.SelectSingleNode("locdate").InnerText;
                    s.end = data.SelectSingleNode("locdate").InnerText;
                    s.allDay = true;

                    slist.Add(s);

                }

                return slist;

            }

        }

        [HttpPost]
        public int Add(string start, string end, string content)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "StartDate",  start},
                { "EndDate", end },
                { "ScheduleName", content }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_SCHEDULE_ADD", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }
        [HttpPost]
        public int Edit(int id, string content)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ScheduleID", id },
                { "ScheduleName", content }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_SCHEDULE_EDIT", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int Delete(int id)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ScheduleID", id }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_SCHEDULE_DELETE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }
    }
}
