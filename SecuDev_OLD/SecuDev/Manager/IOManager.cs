using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
//using FrameWork.DB;

namespace SecuDev.Manager
{
    public class IOManager
    {

        #region WebSetup 파일 정보
        public static SetupInfo iniFile
        {
            get
            {
                return (new SetupInfo());
            }
        }

        /// <summary>
        /// ini 파일 정보
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string SetupFileInfo(string Type)
        {
            string SetupFile = FilePath.DataConn + "WebSetup.ini";
            string RtnVlu = "";
            if (System.IO.File.Exists(SetupFile))
            {
                StreamReader sr = new StreamReader(SetupFile, Encoding.Default);
                string Line;
                string[] arryLine;
                int i = 0;

                //셋업 파일이 있으면...
                while (sr.Peek() > -1)
                {
                    Line = sr.ReadLine();
                    arryLine = Line.Split(':');

                    if (arryLine[0].ToLower() == Type.ToLower())
                    {
                        RtnVlu = arryLine[1];
                        break;
                    }

                    i++;
                }
                sr.Close();

                if (RtnVlu == "")
                {
                    HttpContext.Current.Response.Write("<script>alert('" + String.Format("WebSetup.ini 파일에 [{0}] 정보가 없습니다.{1}관리자에게 문의하세요.", Type, "\\n\\n") + "');</script>");
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                //셋업 파일이 없으면...
                HttpContext.Current.Response.Write("<script>location.href = '/Setup'</script>");
                HttpContext.Current.Response.End();
            }

            return RtnVlu;
        }
        #endregion

        /// <summary>
        /// DB 정보 읽어오기
        /// </summary>
        public class SetupInfo
        {
            public string DB { get { return "DB"; } }
        }

        /// <summary>
        /// 파일 경로
        /// </summary>
        public static FilePathClass FilePath
        {
            get
            {
                return (new FilePathClass());
            }
        }

        /// <summary>
        /// AppSettings
        /// </summary>
        public class ConfigManager
        {
            public static string AppSettings(string key)
            {
                string ret = string.Empty;

                if (ConfigurationManager.AppSettings[key] != null) ret = ConfigurationManager.AppSettings[key].ToString();

                return ret;
            }
        }

        /// <summary>
        /// 파일경로 클래스
        /// </summary>
        public class FilePathClass
        {
            string DefaultDataPath = HttpContext.Current.Server.MapPath("/Upload/");
            public string DataConn { get { return DefaultDataPath + "Data/"; } }
        }


    }
}