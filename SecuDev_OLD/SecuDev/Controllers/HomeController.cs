//using FrameWork.DB;
using SecuDev.Helper;
using SecuDev.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebAdmin.Models;
using PagedList;
//using SecuDEV.Manager;
using SecuDev.Filter;
using CryptoManager;
using SingletonManager;
using System.Threading.Tasks;
using CoreDAL.ORM;
using CoreDAL.Configuration.Interface;

namespace SecuDev.Controllers
{
    public class HomeController : Controller
    {
        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(MvcApplication.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(MvcApplication.SHA256);

        public ActionResult Index(string alertType = "")
        {
            // 세션 초기화
            Session.Clear();

            var list = Utility.GetCategoryList();

            ViewBag.alertType = alertType;

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection col)
        {

            // 세션 초기화
            Session.Clear();

            string Result = "";

            try
            {
                Dictionary<string, object> param = new Dictionary<string, object> 
                {
                    { "UID", col["UID"] },
                    { "Password", crypto.Encrypt(col["Password"]) }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_LOGIN", param);

                DataSet ds = result.DataSet;

                if (ds.Tables[0].Rows.Count == 1)
                {

                    Session["UID"] = ds.Tables[0].Rows[0]["UID"].ToString();
                    Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                    Session["AuthorityLevel"] = ds.Tables[0].Rows[0]["AuthorityLevel"].ToString();
                    Session["IPAddress"] = Utility.GetIP4Address();
                    Result = ds.Tables[0].Rows[0]["Result"].ToString();

                }


                //SqlParamCollection param = new SqlParamCollection();

                //param.Add("@UID", col["UID"]);
                //param.Add("@Password", crypto.Encrypt(col["Password"]));

                //DataSet ds = (new Common()).MdlList(param, "PROC_LOGIN");

                //if (ds.Tables[0].Rows.Count == 1)
                //{

                //    Session["UID"] = ds.Tables[0].Rows[0]["UID"].ToString();
                //    Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                //    Session["AuthorityLevel"] = ds.Tables[0].Rows[0]["AuthorityLevel"].ToString();
                //    Session["IPAddress"] = Utility.GetIP4Address();

                //    Result = ds.Tables[0].Rows[0]["Result"].ToString();
                //}
                //else
                //{
                //    Result = "Invalid";
                //}

            }
            catch (Exception ex)
            {
                Result = "ERR";
            }


            return Json(new { Result });
        }

        [SessionFilter]
        public ActionResult Main()
        {


            return View();
        }

    }
}