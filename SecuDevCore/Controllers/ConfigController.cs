using CoreDAL.Configuration.Interface;
using CoreDAL.ORM;
using CryptoManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecuDev;
using SecuDev.Models;
using SingletonManager;
using System.Security.Cryptography;

namespace SecuDevCore.Controllers
{
    public class ConfigController : Controller
    {
        IDatabaseSetup ConnDB = Singletons.Instance.GetKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB);
        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(SetupName.AES256);
        private readonly IWebHostEnvironment _env;

        public ConfigController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string ChangeLogo(IFormFile file)
        {
            string Rtn = "ERR";

            try
            {
                string dir = $"{_env.WebRootPath}/img/";

                string fileName = "logo2.png";

                string fileFullPath = Path.Combine(dir, fileName);

                using (var stream = new FileStream(fileFullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Rtn = "OK";

            }
            catch(Exception ex)
            {
                Rtn = "ERR";
            }

            return Rtn;
        }

        [HttpPost]
        public int AddCategory(Category c, string type)
        {

            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", type },
                { "CategoryName", c.CategoryName }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_CONFIG_CATEGORY", param);

            Rtn = result.ReturnValue;

            return Rtn;

        }

        [HttpPost]
        public int EditCategory(Category c, string type)
        {

            int Rtn = -1;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Type", type },
                { "CategoryName", c.CategoryName },
                { "CID", c.CID }
            };

            SQLResult result = ConnDB.DAL.ExecuteProcedure(ConnDB, "PROC_CONFIG_CATEGORY", param);

            Rtn = result.ReturnValue;

            return Rtn;

        }

        [HttpPost]
        public int AddAuthority(Authority a)
        {

            int Rtn = -1;

            return Rtn;

        }

        [HttpPost]
        public int EditAuthority(Authority a)
        {

            int Rtn = -1;

            return Rtn;

        }
    }
}
