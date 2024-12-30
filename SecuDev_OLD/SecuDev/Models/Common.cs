using System;
using System.Data;
using System.Transactions;
using FrameWork.Bases;
using FrameWork.DB;

namespace WebAdmin.Models
{
    public class Common : MdlBase
    {
        /// <summary>
        /// DataSet Return
        /// </summary>
        /// <param name="param">파라미터</param>
        /// <param name="ProcName">프로시저명</param>
        public DataSet MdlList(SqlParamCollection param, string ProcName)
        {
            DataSet ds = null;

            if (!ProcName.Equals(""))
            {
                ds = Database.ExecuteDataSet(ProcName, param, CommandType.StoredProcedure);
            }

            return ds;
        }

        /// <summary>
        /// INSERT, UPDATE and DataSet Return
        /// </summary> 
        /// <param name="param"></param>
        /// <param name="ProcName"></param>
        /// <returns></returns>
        public DataSet MdlRegList(SqlParamCollection param, string ProcName)
        {
            DataSet ds = null;

            if (!ProcName.Equals(""))
            {
                ds = Database.ExecuteDataSet(ProcName, param, CommandType.StoredProcedure, true);
            }

            return ds;
        }

        /// <summary>
        /// INSERT, UPDATE
        /// </summary>
        /// <param name="param">파라미터</param>
        /// <param name="ProcName">프로시저명</param>
        public int MdlReg(SqlParamCollection param, string ProcName)
        {
            int rtn = 0;

            if (!ProcName.Equals(""))
            {
                using (TransactionScope t = new TransactionScope())
                {
                    rtn = Database.ExecuteNonQuery(ProcName, param, CommandType.StoredProcedure);
                    t.Complete();
                }
            }

            return rtn;
        }
        /// <summary>
        /// string형 리턴값이 있는 INSERT, UPDATE, SELECT
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ProcName"></param>
        /// <param name="ReturnValue"></param>
        /// <returns></returns>
        public string MdlRegRtn(SqlParamCollection param, string ProcName)
        {
            string rtn = "";

            if (!ProcName.Equals(""))
            {
                using (TransactionScope t = new TransactionScope())
                {
                    param.Add("Return_Value", SqlDbType.VarChar, 255, ParameterDirection.ReturnValue);

                    Database.ExecuteNonQuery(ProcName, param, CommandType.StoredProcedure);

                    rtn = param["Return_Value"].Value.ToString();

                    t.Complete();
                }
            }

            return rtn;
        }

        public int MdlRegIntRtn(SqlParamCollection param, string ProcName)
        {
            int rtn = -1;

            if (!ProcName.Equals(""))
            {
                using (TransactionScope t = new TransactionScope())
                {
                    param.Add("Return_Value", SqlDbType.Int, 255, ParameterDirection.ReturnValue);

                    Database.ExecuteNonQuery(ProcName, param, CommandType.StoredProcedure);

                    rtn = Int32.Parse(param["Return_Value"].Value.ToString());

                    t.Complete();
                }
            }

            return rtn;
        }
    }
}