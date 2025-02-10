using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;
using SecuDev.Helper;
using CoreDAL.Configuration.Interface;
using CryptoManager;
using SecuDev;
using SingletonManager;

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
            List<Holiday> hlist = new List<Holiday>();

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

                    hlist = xmlList.Result;

                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(XmlDir);

                    XmlNodeList xmlList = doc.SelectNodes(XmlNode);

                    foreach (XmlNode data in xmlList)
                    {

                        Holiday h = new Holiday();

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
                    h.allDay = true;

                    hlist.Add(h);

                }

                return hlist;

            }

        }
    }
}
