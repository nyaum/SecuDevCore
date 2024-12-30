using CryptoManager;
using SecuDev;
using SecuDev.Manager;
using SingletonManager;
//using SecuDEV.Manager;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace FrameWork.DB
{
    public class SqlHelper : IDisposable
    {

        ICryptoManager crypto = Singletons.Instance.GetKeyedSingleton<ICryptoManager>(MvcApplication.AES256);

        #region GLOVAL VARIABLES
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string _ConnectionString = CryptoManager.AESDecrypt256(IOManager.SetupFileInfo(IOManager.iniFile.DB));

        /// <summary>
        /// _ComTimeOut
        /// </summary>
        public int _ComTimeOut = 600;

        /// <summary>
        /// SqlConnection init
        /// </summary>
        private SqlConnection _Con;

        /// <summary>
        /// SqlCommand init
        /// </summary>
        private SqlCommand _Com;

        /// <summary>
        /// SqlDataAdapter init
        /// </summary>
        private SqlDataAdapter _Adp;

        /// <summary>
        /// DataSet init
        /// </summary>
        private DataSet _Ds;
        #endregion

        /// <summary>
        /// SqlHelper
        /// </summary>
        /// <param name="iTime">Optional int Command TimeOut Value</param>
        public SqlHelper(int iTime = 600)
        {
            this._ComTimeOut = iTime;
        }

        /// <summary>
        /// SqlHelper
        /// </summary>
        /// <param name="cs">DB ConnectionString</param>
        /// <param name="iTime">Optional Command TimeOut Value</param>
        public SqlHelper(string cs, int iTime = 600)
        {
            this._ConnectionString = cs;
            this._ComTimeOut = iTime;
        }

        /// <summary>
        /// ExecuteDataSet
        /// </summary>
        /// <param name="proc">프로시저</param>
        /// <param name="param">SqlParamCollection</param>
        /// <param name="ct">CommandType</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string proc, SqlParamCollection param, CommandType ct, bool Logging = false)
        {
            try
            {
                string _thisConnectionString = CryptoManager.AESDecrypt256(IOManager.SetupFileInfo(IOManager.iniFile.DB));

                using (_Con = new SqlConnection(_thisConnectionString))
                {
                    _Con = new SqlConnection(_thisConnectionString);

                    _Com = new SqlCommand(proc, _Con);
                    _Com.CommandType = ct;

                    _Com.CommandTimeout = _ComTimeOut;

                    _Adp = new SqlDataAdapter(_Com);
                    _Ds = new DataSet();

                    if (param != null && param._Count > 0)
                    {
                        IEnumerator i = param.GetEnumerator();
                        while (i.MoveNext()) _Com.Parameters.Add(i.Current);
                    }

                    _Con.Open();
                    _Adp.Fill(_Ds);
                    _Con.Close();

                    
                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
            finally
            {
                Dispose();
            }

            return _Ds;
        }
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="proc">프로시저</param>
        /// <param name="param">SqlParamCollection</param>
        /// <param name="ct">CommandType</param>
        /// <returns>int</returns>
        public int ExecuteNonQuery(string proc, SqlParamCollection param, CommandType ct, bool Logging = true)
        {
            int r = 0;

            try
            {
                string _thisConnectionString = CryptoManager.AESDecrypt256(IOManager.SetupFileInfo(IOManager.iniFile.DB));

                using (_Con = new SqlConnection(_thisConnectionString))
                {
                    _Con = new SqlConnection(_thisConnectionString);

                    _Com = new SqlCommand(proc, _Con);
                    _Com.CommandType = ct;

                    _Com.CommandTimeout = 600;

                    if (param != null && param._Count > 0)
                    {
                        IEnumerator i = param.GetEnumerator();
                        while (i.MoveNext()) _Com.Parameters.Add(i.Current);
                    }

                    _Con.Open();
                    r = _Com.ExecuteNonQuery();
                    _Con.Close();


                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
            finally
            {
                Dispose();
            }
            return r;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="proc">프로시저</param>
        /// <param name="param">SqlParamCollection</param>
        /// <param name="ct">CommandType</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string proc, SqlParamCollection param, CommandType ct)
        {
            object r = null;
            try
            {
                string _thisConnectionString = CryptoManager.AESDecrypt256(IOManager.SetupFileInfo(IOManager.iniFile.DB));

                using (_Con = new SqlConnection(_thisConnectionString))
                {
                    _Con = new SqlConnection(_thisConnectionString);

                    _Com = new SqlCommand(proc, _Con);
                    _Com.CommandType = ct;

                    _Com.CommandTimeout = 600;

                    if (param != null && param._Count > 0)
                    {
                        IEnumerator i = param.GetEnumerator();
                        while (i.MoveNext()) _Com.Parameters.Add(i.Current);
                    }

                    _Con.Open();
                    r = _Com.ExecuteScalar();
                    _Con.Close();


                }
            }
            catch (Exception ex)
            {


                throw ex;
            }
            finally
            {
                Dispose();
            }

            return r;
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_Con != null) _Con.Dispose();
            if (_Com != null) _Com.Dispose();
            if (_Adp != null) _Adp.Dispose();
            if (_Ds != null) _Ds.Dispose();
        }
    }
}
