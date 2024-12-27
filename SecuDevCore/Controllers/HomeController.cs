using System.Data;
using System.Diagnostics;
using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using SecuDev;
using SecuDev.Filter;
using SecuDev.Helper;
using SecuDev.Models;
using SecuDevCore.Models;
using SingletonManager;

namespace SecuDevCore.Controllers
{
    public class HomeController : Controller
    {

        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.SHA256);

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public ActionResult Main()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
