using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CryptoManager;
using CryptoManager.Services;
using SingletonManager;
using FileIOHelper;
using FileIOHelper.Helpers;
using CoreDAL.Configuration.Interface;
using CoreDAL.Configuration;
using CoreDAL.Configuration.Models;
using System.Text;
using System.Diagnostics;

namespace SecuDev
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string SHA256 = "SHA256";
        public static string AES256 = "AES256";
        public static string Setupini = "Setupini";
        public static string ConnDB = "ConnDB";

        protected void Application_Start()
        {
            Singletons.Instance.AddKeyedSingleton<ICryptoManager>(SHA256, new SecuSHA256());

            ICryptoManager EncAES256 = new AES256("secu13579");
            Singletons.Instance.AddKeyedSingleton<ICryptoManager>(AES256, EncAES256);

            IDatabaseSetup setup = new DatabaseSetup(DatabaseType.MSSQL, new IniFileHelper($"{Server.MapPath("/")}" + "Upload\\Data\\WebSetup.ini"), "SECUDEV", EncAES256);
            IDbConnectionInfo info = new MsSqlConnectionInfo { Server = "127.0.0.1", UserId = "sa", Password = "s1access!", Database = "SECUDEV" };
            Singletons.Instance.AddKeyedSingleton<IDatabaseSetup>(ConnDB, setup);
            setup.UpdateConnectionInfo(info);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest()
        {
            // 디버깅중일때는 화면에 오류코드 보이게
            if (!Debugger.IsAttached)
            {
                int ERRCode = Context.Response.StatusCode;
                string ERRPage = String.Format("/Error?ERRCode={0}", ERRCode);


                if (ERRCode == 404)
                {
                    Response.Redirect(ERRPage);
                }
                else if (ERRCode == 500)
                {
                    Response.Redirect(ERRPage);
                }
            }
        }
    }
}
