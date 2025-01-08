using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SecuDev;
using SecuDev.Models;
using SecuDevCore.Models;
using SingletonManager;
using System.Data;
using System.Drawing.Printing;
using System.Security.Cryptography;
using X.PagedList.Extensions;

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
            List<Tree> list = new List<Tree>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", "LocationTreeView" }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_LIST", param);

            DataSet ds = result.DataSet;

            list = ds.Tables[0].ToObject<Tree>() as List<Tree>;

            var tree = list.ToLookup(x => x.ParentLocationID);

            foreach (var t in list)
            {
                if (tree[t.LocationID].Count() > 0)
                {
                    t.nodes = tree[t.LocationID].ToList<Tree>();
                }
            }

            ViewBag.tree = JsonConvert.SerializeObject(tree[null].ToList());

            return View();
        }

        public IActionResult IfRead(int LocationID, int? Page, int PageSize = 5)
        {
            int PageNo = Page ?? 1;

            List<Project> list = new List<Project>();
            List<Contact> contacts = new List<Contact>();

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

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                Contact c = dr.ToObject<Contact>();
                contacts.Add(c);
            }

            ViewBag.list = list;
            ViewBag.Contact = contacts;
            ViewBag.LocationID = LocationID;

            return View(list.ToPagedList(PageNo, PageSize));
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
