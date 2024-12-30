using System;
using System.Collections;
using System.Data;
using FrameWork.DB;

namespace FrameWork.Bases
{
    public class MdlBase : IDisposable
    {
        /// <summary>
        /// private SqlHelper _Database
        /// </summary>
        private SqlHelper _Database;

        /// <summary>
        /// public SqlHelper Database
        /// </summary>
        public SqlHelper Database { get { return _Database; } }

        /// <summary>
        /// MdlBase Creator
        /// </summary>
        public MdlBase()
        {
            _Database = new SqlHelper();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_Database != null)
                _Database.Dispose();
        }
    }
}