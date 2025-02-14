using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Xml;
using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecuDev;
using SecuDev.Filter;
using SecuDev.Helper;
using SecuDev.Models;
using SecuDevCore.Models;
using SingletonManager;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SecuDevCore.Controllers
{
    //public class Holiday
    //{
    //    public string id { get; set; }
    //    public string title { get; set; }
    //    public string start { get; set; }
    //    public string end { get; set; }
    //    public bool allDay { get; set; }
    //}

    public class HomeController : Controller
    {

        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.SHA256);

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public IActionResult Index(string alertType = "")
        {
            // 세션 초기화
            HttpContext.Session.Clear();

            var list = Utility.GetCategoryList();

            ViewBag.alertType = alertType;

            return View();
        }

        [HttpPost]
        public string Login(IFormCollection col)
        {

            // 세션 초기화
            HttpContext.Session.Clear();

            string Rtn = "Invalid";

            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "UID", col["UID"].ToString() },
                    { "Password", crypto.Encrypt(col["Password"]) }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_LOGIN", param);

                DataSet ds = result.DataSet;

                if (ds.Tables[0].Rows.Count == 1)
                {

                    HttpContext.Session.SetString("UID", ds.Tables[0].Rows[0]["UID"].ToString());
                    HttpContext.Session.SetString("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
                    HttpContext.Session.SetString("AuthorityLevel", ds.Tables[0].Rows[0]["AuthorityLevel"].ToString());

                    HttpContext.Session.SetString("IPAddress", Utility.GetIP4Address());

                    Rtn = ds.Tables[0].Rows[0]["Result"].ToString();

                }

            }
            catch (Exception ex)
            {
                Rtn = "ERR";
            }

            return Rtn;

        }

        [SessionFilter]
        public IActionResult Main()
        {

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;

            if (settings["test"] == null) settings.Add("test", "1");
            else settings["test"].Value = (int.Parse(settings["test"].Value) + 1).ToString();

            config.Save(ConfigurationSaveMode.Modified); //파일에 저장

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
    }
}
