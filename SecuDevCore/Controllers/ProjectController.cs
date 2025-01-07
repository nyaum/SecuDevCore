using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using SecuDev;
using SecuDev.Models;
using SecuDevCore.Models;
using SingletonManager;
using System.Data;
using System.Security.Cryptography;

namespace SecuDevCore.Controllers
{
    public class ProjectController : Controller
    {
        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.AES256);

        private readonly IWebHostEnvironment _env;

        public ProjectController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public IActionResult Index(int TeamID)
        {
            List<Node> list = new List<Node>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", "MasterLocationByTeam" },
                { "SchText", TeamID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_PROJECT_LOCATION_LIST", param);

            DataSet ds = result.DataSet;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

            }

            ViewBag.list = list;

            return View();
        }

        public IActionResult IfRead(int LocationID)
        {

            List<Location> list = new List<Location>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", "LocationByID" },
                { "SchText", LocationID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_PROJECT_LOCATION_LIST", param);

            DataSet ds = result.DataSet;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Location l = dr.ToObject<Location>();

                list.Add(l);
            }

            ViewBag.list = list;

            return View();
        }

        public IActionResult IfDetail(int LocationID)
        {

            List<Project> list = new List<Project>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LocationID", LocationID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_LIST", param);

            DataSet ds = result.DataSet;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Project p = dr.ToObject<Project>();

                list.Add(p);
            }

            ViewBag.list = list;

            return View();
        }
    }
}
