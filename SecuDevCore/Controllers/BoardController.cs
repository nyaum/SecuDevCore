﻿using CoreDAL.Configuration.Interface;
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

        public IActionResult Index(int? Page, int PageSize = 10)
        {
            int PageNo = Page ?? 1;

            List<Board> list = new List<Board>();

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_BOARD_LIST");

            DataSet ds = result.DataSet;

            foreach (DataRow b in ds.Tables[0].Rows)
            {
                Board tb = b.ToObject<Board>();
                Users tu = b.ToObject<Users>();
                Category tc = b.ToObject<Category>();

                tb.Users = tu;
                tb.Category = tc;

                list.Add(tb);
            }

            ViewBag.list = list;

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
                b.FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();

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

            Board b = new Board();

            b.Users.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
            b.Category.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            b.Category.BackgroundColor = ds.Tables[0].Rows[0]["BackgroundColor"].ToString();
            b.Category.FontColor = ds.Tables[0].Rows[0]["FontColor"].ToString();
            b.BID = BID;
            b.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            b.Content = ds.Tables[0].Rows[0]["Content"].ToString();
            b.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
            b.FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
            b.InsertDate = ds.Tables[0].Rows[0]["InsertDate"].ToString();

            ViewBag.Read = b;

            return View();
        }

        [HttpPost]
        public int Write(Board b, string[] FilePath)
        {
            int Rtn = -1;

            string dbFilePath = "";
            string FileName = "";

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
                { "CID", b.Category.CID },
                { "UID", HttpContext.Session.GetString("UID") },
                { "Title", b.Title },
                { "Content", b.Content },
                { "FilePath", dbFilePath },
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

            string today = Utility.DateTimeFormat(Utility.GetNowDate(), 8);
            string uploadDir = $"{_env.ContentRootPath}/Upload/File/{today}/";
            string dir = "/" + today + "/";

            string dbFilePath = "";
            string altFileName = "";

            // 경로 확인
            try
            {
                if (file != null)
                {
                    // 경로 확인
                    DirectoryInfo di = new DirectoryInfo(uploadDir);

                    if (di.Exists == false)
                    {
                        di.Create();
                    }

                    foreach (var formFile in file)
                    {
                        if (formFile.Length > 0)
                        {
                            var fileName = Path.GetFileName(formFile.FileName);
                            var fileFullPath = Path.Combine(uploadDir + fileName);
                            var fileDir = Path.Combine(dir + fileName);

                            int filecnt = 1;
                            string newFileName = string.Empty;
                            while (new FileInfo(fileFullPath).Exists)
                            {
                                var idx = formFile.FileName.LastIndexOf('.');
                                var tmp = formFile.FileName.Substring(0, idx);
                                newFileName = tmp + String.Format("({0})", filecnt++) + formFile.FileName.Substring(idx);
                                fileFullPath = uploadDir + newFileName;
                                fileDir = dir + newFileName;    // TODO: 여기에서 다시 파일 경로를 조합하고 있음
                            }

                            //formFile.SaveAs(fileFullPath);

                            using (var stream = new FileStream(fileFullPath, FileMode.Create))
                            {
                                formFile.CopyTo(stream);
                            }


                            // 마지막 배열일경우 | 를 표시하지 않음
                            if (formFile.Equals(file.Last()))
                            {
                                dbFilePath = crypto.Encrypt(fileDir);   // TODO: += 연산자가 필요 없어 보임
                                altFileName = fileName;
                            }
                            else
                            {
                                dbFilePath += fileDir + "|";
                                altFileName += fileName + "|";
                            }
                        }
                    }
                }

                Rtn = "OK";

            }
            catch (Exception ex)
            {

            }

            return Json(new { uniqueFileId = dbFilePath, FileName = altFileName });
        }

        [HttpPost]
        public ActionResult FileDelete(string uniqueFileId)
        {

            string sRtn = "Fail";

            uniqueFileId = $"{_env.ContentRootPath}/Upload/File/{crypto.Decrypt(uniqueFileId)}";

            if (System.IO.File.Exists(uniqueFileId))
            {

                System.IO.File.Delete(uniqueFileId);

            }

            return Json(new { });
        }

        public FileResult Download(string uniqueFileId, string FileName)
        {
            string FilePath = $"{_env.ContentRootPath}/Upload/File/{crypto.Decrypt(uniqueFileId)}";

            byte[] bytes = System.IO.File.ReadAllBytes(FilePath);

            return File(bytes, "application/octet-stream", FileName);

        }

    }
}