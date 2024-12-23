using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using SecuDev;
using SecuDev.Models;
using SingletonManager;
using System.Data;
using X.PagedList.Extensions;

namespace SecuDevCore.Controllers
{
    public class UserConfigController : Controller
    {

        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.AES256);

        private readonly IWebHostEnvironment _env;

        public UserConfigController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public IActionResult Index(int? Page, int PageSize = 5)
        {
            int PageNo = Page ?? 1;

            List<Users> list = new List<Users>();

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_USERCONFIG_LIST");

            DataSet ds = result.DataSet;

            foreach (DataRow u in ds.Tables[0].Rows)
            {
                Users tu = u.ToObject<Users>();
                Authority ta = u.ToObject<Authority>();

                tu.Authority = ta;

                list.Add(tu);

            }


            return View(list.ToPagedList(PageNo, PageSize));
        }
    }
}
