﻿using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using SecuDev;
using SecuDev.Models;
using SingletonManager;
using System.Data;
using System.Security.Cryptography;
using X.PagedList.Extensions;

namespace SecuDevCore.Controllers
{
    public class UserConfigController : Controller
    {

        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.AES256);
        ICryptoManager SHA256 = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.SHA256);

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

        /// <summary>
        /// -1 : 오류, 0: 중복, 1: 사용가능
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpPost]
        public int CheckDuplicate(string UID)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "UID", UID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_USERCONFIG_DUPLICATE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        /// <summary>
        /// -1 : 오류, 0 : 중복, 1 : 정상 처리
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        [HttpPost]
        public int Write(IFormCollection col)
        {
            int Rtn = -1;

            string UserName = col["UserName"];
            string UID = col["UID"];
            string Password = SHA256.Encrypt(col["Password"]);
            string Email = col["Email"];
            int AuthorityLevel = Int32.Parse(col["AuthorityLevel"]);

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "UserName", UserName },
                { "UID", UID },
                { "Password", Password },
                { "Email", Email },
                { "AuthorityLevel", AuthorityLevel }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_USERCONFIG_REG", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int Change(string UID, string Type)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", Type },
                { "UID", UID },
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_USERCONFIG_CHANGE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }
    }
}