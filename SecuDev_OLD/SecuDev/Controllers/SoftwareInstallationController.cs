using FrameWork.DB;
using PagedList;
using SecuDev.Filter;
using SecuDev.Helper;
using SecuDev.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace SecuDev.Controllers
{
    [SessionFilter]
    public class SoftwareInstallationController : Controller
    {
        public ActionResult Index(int? Page, int PageSize = 10, bool search = false)
        {
            int PageNo = Page ?? 1;

            // 검색하면 페이지는 1번으로
            if (search)
            {
                PageNo = 1;
            }

            string InstallSDate = Utility.DateTimeFormat(Request["InstallSDate"], 1) ?? "";
            string InstallEDate = Utility.DateTimeFormat(Request["InstallEDate"], 1) ?? "";

            string LocationID = Request["LocationID"] ?? "";
            string SoftwareID = Request["SoftwareID"] ?? "";

            string CorpsName = Request["CorpsName"] ?? "";
            string GateName = Request["GateName"] ?? "";

            SqlParamCollection param = new SqlParamCollection();

            param.Add("@InstallSDate", InstallSDate);
            param.Add("@InstallEDate", InstallEDate);
            param.Add("@LocationID", LocationID);
            param.Add("@SoftwareID", SoftwareID);
            param.Add("@CorpsName", CorpsName);
            param.Add("@GateName", GateName);

            DataSet ds = (new Common()).MdlList(param, "USP_GET_UPDATEBYLOCATION");

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

            ViewBag.Count = list.Count;
            ViewBag.InstallSDate = InstallSDate;
            ViewBag.InstallEDate = InstallEDate;
            ViewBag.LocationID = LocationID;
            ViewBag.SoftwareID = SoftwareID;
            ViewBag.CorpsName  = CorpsName;
            ViewBag.GateName   = GateName;

            return View(list.ToPagedList(PageNo, PageSize));
        }
    }
}