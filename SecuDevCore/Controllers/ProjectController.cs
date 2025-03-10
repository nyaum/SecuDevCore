﻿using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using CryptoManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SecuDev;
using SecuDev.Models;
using SecuDevCore.Helper;
using SecuDevCore.Models;
using SingletonManager;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Xml.Linq;
using X.PagedList.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var jsTreeData = TreeHelper.JsTreeFormat(list);
            ViewBag.tree = JsonConvert.SerializeObject(jsTreeData);

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

        public IActionResult IfWriteHistory(int LocationID, int ProjectID = 0)
        {

            Project p = new Project();

            if (ProjectID > 0)
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "ProjectID", ProjectID }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_READ", param);

                DataSet ds = result.DataSet;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    p = ds.Tables[0].Rows[0].ToObject<Project>();
                }

            }

            ViewBag.LocationID = LocationID;
            ViewBag.Project = p;

            return View();
        }

        [HttpPost]
        public int WriteHistory(string SoftwareID, string InstallTypeID, int LocationID, string UID, string Content, string Version, string[] FilePath)
        {

            int Rtn = -1;

            try
            {
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
                    { "SoftwareID", SoftwareID },
                    { "InstallTypeID", InstallTypeID },
                    { "LocationID", LocationID },
                    { "UID", UID },
                    { "Content", Content },
                    { "Version", Version },
                    { "UUID", dbFilePath },
                    { "FileName", FileName }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_WRITE", param);

                Rtn = result.ReturnValue;

            }
            catch (Exception ex)
            {
                Rtn = -1;
            }

            return Rtn;
        }

        [HttpPost]
        public int CheckLocation(int LocationID)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "LocationID", LocationID },
                };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_PROJECT_LOCATION_CHECK", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int WriteLocation(int id, int ParentLocationID, string name)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "LocationID", id },
                    { "ParentLocationID", ParentLocationID },
                    { "LocationName", name },
                };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_PROJECT_LOCATION_WRITE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int DeleteLocation(int LocationID) 
        { 
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "LocationID", LocationID },
                };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_PROJECT_LOCATION_DELETE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        public IActionResult IfAddContact(int LocationID)
        {
            List<Contact> contacts = new List<Contact>();

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LocationID", LocationID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_LIST", param);

            DataSet ds = result.DataSet;

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                Contact c = dr.ToObject<Contact>();
                contacts.Add(c);
            }

            ViewBag.LocationID = LocationID;
            ViewBag.Contact = contacts;

            return View();
        }

        [HttpPost]
        public int AddContact(int LocationID, string ContactName, string ContactGrade, string ContactCompany, string ContactTel, string ContactEmail)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LocationID", LocationID },
                { "ContactName", ContactName },
                { "Grade", ContactGrade },
                { "Company", ContactCompany },
                { "Tel", ContactTel },
                { "Email", ContactEmail }
            };
            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_CONTACT_WRITE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int RemoveContact(int ContactID, int LocationID)
        {
            int Rtn = -1;
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ContactID", ContactID },
                { "LocationID", LocationID }
            };
            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_CONTACT_DELETE", param);
            Rtn = result.ReturnValue;
            return Rtn;
        }

        public IActionResult IfReadHistory(int ProjectID)
        {

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ProjectID", ProjectID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_READ", param);

            DataSet ds = result.DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                Project p = ds.Tables[0].Rows[0].ToObject<Project>();

                ViewBag.Project = p;
            }

            return View();
        }

        [HttpPost]
        public string GetLocationNote(int LocationID)
        {
            string Rtn = "";

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LocationID", LocationID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_LOCATION_READ", param);

            DataSet ds = result.DataSet;

            Rtn = ds.Tables[0].Rows[0]["Note"].ToString();

            return Rtn;
        }

        [HttpPost]
        public int EditNote(int LocationID, string Note)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "LocationID", LocationID },
                { "Note", Note }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_NOTE_UPDATE", param);

            Rtn = result.ReturnValue;

            return Rtn;
        }

        [HttpPost]
        public int DeleteHistory(int ProjectID)
        {
            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ProjectID", ProjectID },
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_DELETE", param);

            Rtn = result.ReturnValue;
            return Rtn;
        }

        [HttpPost]
        public int EditHistory(string SoftwareID, string InstallTypeID, int LocationID, string UID, string Content, string Version, string[] FilePath, int ProjectID)
        {
            int Rtn = -1;

            try
            {
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
                    { "ProjectID", ProjectID },
                    { "SoftwareID", SoftwareID },
                    { "InstallTypeID", InstallTypeID },
                    { "UID", UID },
                    { "Content", Content },
                    { "Version", Version },
                    { "UUID", dbFilePath },
                    { "FileName", FileName }
                };

                SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_EDIT", param);

                Rtn = result.ReturnValue;

            }
            catch (Exception ex)
            {
                Rtn = -1;
            }

            return Rtn;
        }

        //public IActionResult IfDetail(int LocationID)
        //{

        //    List<Project> list = new List<Project>();

        //    Dictionary<string, object> param = new Dictionary<string, object>
        //    {
        //        { "LocationID", LocationID }
        //    };

        //    SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_IF_PROJECT_LIST", param);

        //    DataSet ds = result.DataSet;

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Project p = dr.ToObject<Project>();

        //        list.Add(p);
        //    }

        //    ViewBag.list = list;

        //    return View();
        //}
    }
}
