using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Xml;
using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
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

namespace SecuDevCore.Controllers
{
    public class Holiday
    {
        public string id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
    }

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

            List<Holiday> hlist = new List<Holiday>();

            string XmlDir = $"{_env.ContentRootPath}/Upload/Data/RestDeInfo.xml";
            string XmlNode = "/response/body/items/item";

            string url = "http://apis.data.go.kr/B090041/openapi/service/SpcdeInfoService/getRestDeInfo";
            url += "?ServiceKey=" + "w7Ycwo9qcSFEoKkPtsvvg1ww8vxweOXvChmlMql3HZxutjR%2FYbmn7vWJONRUy25Zozng3hSvyKOGMM5glY%2BRWw%3D%3D";
            url += "&solYear=" + Utility.DateTimeFormat(Utility.GetNowDate(), 5);
            url += "&numOfRows=20";

            if (System.IO.File.Exists(XmlDir))
            {

                var info = new System.IO.FileInfo(XmlDir);

                var LastUpdate = info.LastWriteTime;

                TimeSpan dateDiff = Convert.ToDateTime(Utility.GetNowDate()) - LastUpdate;

                double diffDay = dateDiff.Days;

                if (diffDay >= 7)
                {
                    var xmlList = XmlSave(url, XmlDir, XmlNode);

                    hlist = xmlList.Result;

                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(XmlDir);

                    XmlNodeList xmlList = doc.SelectNodes(XmlNode);

                    foreach(XmlNode data in xmlList)
                    {

                        Holiday h = new Holiday();

                        h.id = "holiday";
                        h.title = data.SelectSingleNode("dateName").InnerText;
                        h.start = data.SelectSingleNode("locdate").InnerText;
                        h.allDay = true;

                        hlist.Add(h);

                    }

                }

            }
            else if (!System.IO.File.Exists(XmlDir))
            {

                var xmlList = XmlSave(url, XmlDir, XmlNode);

                hlist = xmlList.Result;
            }

            object a = JToken.Parse(JsonConvert.SerializeObject(hlist));

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
        public async Task<List<Holiday>> XmlSave(string url, string XmlDir, string XmlNode)
        {
            List<Holiday> hlist = new List<Holiday>();

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

                    Holiday h = new Holiday();

                    h.title = data.SelectSingleNode("dateName").InnerText;
                    h.start = data.SelectSingleNode("locdate").InnerText;
                    h.end = data.SelectSingleNode("locdate").InnerText;

                    hlist.Add(h);

                }

                return hlist;

            }

        }
    }
}
