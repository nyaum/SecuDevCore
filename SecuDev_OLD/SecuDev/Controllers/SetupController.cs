
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebAdmin.Models;

namespace SecuDev.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        public ActionResult Index()
        {
            return View();
        }

        ///// <summary>
        ///// ini 파일 체크
        ///// </summary>
        //private void FileCreateChk()
        //{
        //    string SetupFile = Server.MapPath("/Upload/Data/") + "WebSetup.ini";

        //    if (System.IO.File.Exists(SetupFile))
        //    {
        //        Response.Write("<script>alert('" + String.Format("DB 정보가 이미 생성되었습니다.{0}운영설정WEB으로 이동하겠습니다.", "\\n\\n") + "');location.href = '/'</script>");
        //        Response.End();
        //    }
        //}

        ///// <summary>
        ///// .ini 파일 생성
        ///// </summary>
        ///// <param name="col">FormCollection</param>
        ///// <returns>string</returns>
        //[HttpPost]
        //public string Conn(FormCollection col)
        //{
        //    string sRtn = String.Empty;
        //    string _ConnectionString = "Data Source=" + col["DBHost"] + "," + col["Port"] + ";Initial Catalog=" + col["DBName"] + ";Persist Security Info=True;User ID=" + col["User"] + ";Password=" + col["Password"];

        //    string SetupFile = Server.MapPath("/Upload/Data/") + "WebSetup.ini";


        //    SqlConnection _Con = null;
        //    try
        //    {
        //        using (_Con = new SqlConnection(_ConnectionString))
        //        {
        //            _Con = new SqlConnection(_ConnectionString);

        //            _Con.Open();
        //        }

        //        //셋업 파일이 있으면...
        //        if (System.IO.File.Exists(SetupFile))
        //        {
        //            sRtn = "File";
        //        }
        //        else
        //        {
        //            sRtn = "Open";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        sRtn = "Close";
        //    }


        //    return sRtn;
        //}

        //[HttpPost]
        //public string FileCreate(FormCollection col)
        //{
        //    string sRtn = String.Empty;
        //    string value = "Data Source=" + col["DBHost"] + "," + col["Port"] + ";Initial Catalog=" + col["DBName"] + ";Persist Security Info=True;User ID=" + col["User"] + ";Password=" + col["Password"];

        //    string AdminDataPath = Server.MapPath("/Upload/Data/");


        //    string EncValue = CryptoManager.AESEncrypt256(value);

        //    if (!Directory.Exists(AdminDataPath)) Directory.CreateDirectory(AdminDataPath);
        //    StreamWriter sw = new StreamWriter(AdminDataPath + "WebSetup.ini");
        //    sw.Write("DB:" + EncValue);
        //    sw.Close();

        //    sRtn = "OK";

        //    return sRtn;
        //}

        //[HttpPost]
        //public string iniCheck(){


        //    string sRtn = string.Empty;

        //    string DBinf = Manager.IOManager.SetupFileInfo("DB");

        //    string DecValue = CryptoManager.AESDecrypt256(DBinf);

        //    return sRtn;
        //}
    }
}