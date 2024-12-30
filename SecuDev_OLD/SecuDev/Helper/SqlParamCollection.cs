using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FrameWork.DB
{
    public class SqlParamCollection : IEnumerable<SqlParameter>
    {
        #region GLOVAL VARIABLES
        /// <summary>
        /// SqlParameter init
        /// </summary>
        private List<SqlParameter> _List;

        /// <summary>
        /// Count init
        /// </summary>
        public int _Count { get { return this._List.Count; } }

        /// <summary>
        /// SqlParameter return
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>SqlParameter</returns>
        public SqlParameter this[int index] { get { return this._List[index]; } }

        /// <summary>
        /// SqlParameter return
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>SqlParameter</returns>
        public SqlParameter this[string name] { get { return this._List.Find(x => x.ParameterName == name); } }
        #endregion

        /// <summary>
        /// SqlParamCollection
        /// </summary>
        public SqlParamCollection()
        {
            _List = new List<SqlParameter>();
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name">name</param>
        public void Add(string name)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;

            this._List.Add(param);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="vlu">value</param>
        public void Add(string name, object vlu)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = name;
            param.Value = vlu;

            this._List.Add(param);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="pt">SqlDbType</param>
        /// <param name="size">size</param>
        /// <param name="pd">ParameterDirection</param>
        public void Add(string name, SqlDbType pt, int size, ParameterDirection pd)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = name;
            param.SqlDbType = pt;
            param.Size = size;
            param.Direction = pd;

            this._List.Add(param);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            this._List.Clear();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<SqlParameter>)this).GetEnumerator();
        }

        /// <summary>
        /// IEnumerable<SqlParameter>.GetEnumerator
        /// </summary>
        /// <returns>IEnumerator<SqlParameter></returns>
        IEnumerator<SqlParameter> IEnumerable<SqlParameter>.GetEnumerator()
        {
            for (int index = 0; index < this._Count; index++)
            {
                yield return _List[index];
            }
        }
    }
}
