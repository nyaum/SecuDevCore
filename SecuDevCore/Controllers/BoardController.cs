using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuDev;
using SecuDev.Helper;
using SecuDev.Models;
using SingletonManager;
using System.Data;
using System.Drawing.Printing;
using System.Security.Cryptography;
using X.PagedList.Extensions;

namespace SecuDevCore.Controllers
{
    public class BoardController : Controller
    {

        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.AES256);

        private readonly IWebHostEnvironment _env;

        public BoardController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] Board b, [FromQuery] Users u, [FromQuery] Category c, int? Page, int PageSize = 10)
        {
            int PageNo = Page ?? 1;

            int CID = c.CID;
            string Title = b.Title ?? "";
            string UserName = u.UserName ?? "";

            List<Board> list = new List<Board>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CID", CID },
                { "Title", Title },
                { "UserName", UserName }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_LIST", param);

            DataSet ds = result.DataSet;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Board tb = dr.ToObject<Board>();
                Users tu = dr.ToObject<Users>();
                Category tc = dr.ToObject<Category>();

                tb.Users = tu;
                tb.Category = tc;

                list.Add(tb);
            }

            ViewBag.list = list;
            ViewBag.CID = CID;
            ViewBag.Title = Title;
            ViewBag.UserName = UserName;

            return View(list.ToPagedList(PageNo, PageSize));
        }

        public IActionResult Edit(string PageType, int? BID)
        {

            Board b = new Board();

            if (PageType == "E")
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "BID", BID }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_READ", param);

                DataSet ds = result.DataSet;

                b.Category.CID = Int32.Parse(ds.Tables[0].Rows[0]["CID"].ToString());
                b.Category.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                b.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                b.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                b.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                b.UUID = ds.Tables[0].Rows[0]["UUID"].ToString();

            }

            ViewBag.PageType = PageType;
            ViewBag.Board = b;

            return View();
        }
        public IActionResult Read(int BID)
        {

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "BID",BID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_READ", param);

            DataSet ds = result.DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                Board b = new Board();

                b.Users.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                b.Category.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                b.Category.BackgroundColor = ds.Tables[0].Rows[0]["BackgroundColor"].ToString();
                b.Category.FontColor = ds.Tables[0].Rows[0]["FontColor"].ToString();
                b.BID = BID;
                b.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                b.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                b.UUID = ds.Tables[0].Rows[0]["UUID"].ToString();
                b.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                b.InsertDate = ds.Tables[0].Rows[0]["InsertDate"].ToString();

                ViewBag.Read = b;
            }

            return View();
        }

        [HttpPost]
        public int Write(Board b, int? CID, string[] FilePath, string[] DeleteFilePath)
        {
            int Rtn = -1;

            string dbFilePath = "";
            string FileName = "";

            // 삭제부터 처리
            if (DeleteFilePath != null)
            {
                for (int i = 0; i < DeleteFilePath.Length; i++)
                {
                    dbFilePath = DeleteFilePath[i].Split(',')[0];

                    FileDelete(dbFilePath);
                }
            }

            dbFilePath = "";
            FileName = "";

            if (FilePath != null)
            {
                for (int i = 0; i < FilePath.Length; i++)
                {
                    if (i == 0)
                    {
                        dbFilePath = FilePath[i].Split(',')[0];
                        FileName = FilePath[i].Split(',')[1];
                    }
                    else
                    {
                        dbFilePath += "|" + FilePath[i].Split(',')[0];
                        FileName += "|" + FilePath[i].Split(',')[1];
                    }
                }
            }


            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CID", CID },
                { "UID", HttpContext.Session.GetString("UID") },
                { "Title", b.Title },
                { "Content", b.Content },
                { "UUID", dbFilePath },
                { "FileName", FileName },
                { "IPAddress", HttpContext.Session.GetString("IPAddress") }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_WRITE", param);

            Rtn = result.ReturnValue;

            return Rtn;

        }

        [HttpPost]
        public int Delete(int BID)
        {
            int Rtn = -1;

            try
            {

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "UpdateUID", HttpContext.Session.GetString("UID") },
                    { "BID", BID },
                    { "UpdateIP", HttpContext.Session.GetString("IPAddress") },
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_DELETE", param);

                Rtn = result.ReturnValue;

            }
            catch (Exception ex)
            {

                Rtn = -1;

            }

            return Rtn;
        }

        [HttpPost]
        public ActionResult FileUpload(List<IFormFile> file)
        {

            string Rtn = "FAIL";

            string dir = $"{_env.ContentRootPath}/Upload/File/";

            string dbFilePath = "";
            string altFileName = "";

            string uuid = "";

            // 경로 확인
            try
            {
                if (file != null)
                {
                    // 경로 확인
                    DirectoryInfo di = new DirectoryInfo(dir);

                    if (di.Exists == false)
                    {
                        di.Create();
                    }

                    foreach (var formFile in file)
                    {
                        if (formFile.Length > 0)
                        {
                            var _uuid = Guid.NewGuid().ToString();
                            var fileName = Path.GetFileName(formFile.FileName);
                            var fileFullPath = Path.Combine(dir + _uuid);
                            var fileDir = Path.Combine(dir + _uuid);

                            using (var stream = new FileStream(fileFullPath, FileMode.Create))
                            {
                                formFile.CopyTo(stream);
                            }


                            // 마지막 배열일경우 | 를 표시하지 않음
                            if (formFile.Equals(file.Last()))
                            {
                                dbFilePath = crypto.Encrypt(fileDir);   // TODO: += 연산자가 필요 없어 보임
                                altFileName = fileName;
                                uuid = _uuid;
                            }
                            else
                            {
                                dbFilePath += fileDir + "|";
                                altFileName += fileName + "|";
                                uuid += _uuid + "|";
                            }
                        }
                    }
                }

                Rtn = "OK";

            }
            catch (Exception ex)
            {

            }

            return Json(new { uniqueFileId = uuid, FileName = altFileName, uuid = uuid });
        }

        [HttpPost]
        public ActionResult FileDelete(string uniqueFileId)
        {

            string sRtn = "Fail";

            uniqueFileId = $"{_env.ContentRootPath}/Upload/File/{uniqueFileId}";

            if (System.IO.File.Exists(uniqueFileId))
            {

                System.IO.File.Delete(uniqueFileId);

            }

            return Json(new { });
        }

        public FileResult Download(string uniqueFileId, string FileName)
        {
            string FilePath = $"{_env.ContentRootPath}/Upload/File/{uniqueFileId}";

            byte[] bytes = System.IO.File.ReadAllBytes(FilePath);

            return File(bytes, "application/octet-stream", FileName);

        }

    }
}
