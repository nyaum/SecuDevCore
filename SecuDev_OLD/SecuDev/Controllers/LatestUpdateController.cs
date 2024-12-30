using FrameWork.DB;
using SecuDev.Helper;
using SecuDev.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;
using PagedList;
using SecuDev.Filter;

namespace SecuDev.Controllers
{
    [SessionFilter]
    public class LatestUpdateController : Controller
    {
        // GET: LatestUpdate
        public ActionResult Index(int? Page, int PageSize = 10)
        {
            int PageNo = Page ?? 1;

            SqlParamCollection param = new SqlParamCollection();

            DataSet ds = (new Common()).MdlList(param, "usp_GetLatestUpdatesByLocation");

            List<Location> list = new List<Location>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                Location l = new Location();

                l.InstallationID = Int32.Parse(ds.Tables[0].Rows[i]["InstallationID"].ToString());
                l.LocationName = ds.Tables[0].Rows[i]["LocationName"].ToString();
                l.CorpsName = ds.Tables[0].Rows[i]["CorpsName"].ToString();
                l.GateName = ds.Tables[0].Rows[i]["GateName"].ToString();
                l.InstallationDate = Utility.DateTimeFormat(ds.Tables[0].Rows[i]["InstallationDate"].ToString(), 1);
                l.InstallationType = ds.Tables[0].Rows[i]["InstallationType"].ToString();
                l.SoftwareName = ds.Tables[0].Rows[i]["SoftwareName"].ToString();
                l.Version = ds.Tables[0].Rows[i]["Version"].ToString();
                l.Notes = ds.Tables[0].Rows[i]["Notes"].ToString();

                list.Add(l);
            }

            return View(list.ToPagedList(PageNo, PageSize));
        }

        [HttpPost]
        public JsonResult GetUpdatedHistory(FormCollection col)
        {
            SqlParamCollection param = new SqlParamCollection();

            param.Add("@InstallationID", col["InstallationID"]);

            DataSet ds = (new Common()).MdlList(param, "usp_GetLatestUpdatesByLocation");

            Location l = new Location();

            l.InstallationID = Int32.Parse(ds.Tables[0].Rows[0]["InstallationID"].ToString());
            l.LocationName = ds.Tables[0].Rows[0]["LocationName"].ToString();
            l.CorpsName = ds.Tables[0].Rows[0]["CorpsName"].ToString();
            l.GateName = ds.Tables[0].Rows[0]["GateName"].ToString();
            l.InstallationDate = Utility.DateTimeFormat(ds.Tables[0].Rows[0]["InstallationDate"].ToString(), 1);
            l.InstallationType = ds.Tables[0].Rows[0]["InstallationType"].ToString();
            l.SoftwareName = ds.Tables[0].Rows[0]["SoftwareName"].ToString();
            l.Version = ds.Tables[0].Rows[0]["Version"].ToString();
            l.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();

            return Json(new { Result = l }); ;
        }
    }
}