namespace CSM_Data_Access.DataLayer
{
    public class CSqlHelper
    {
        public static SqlConnection CreateConnection(string p_strConnection_String)
        {
            SqlConnection v_conn = new();

            try
            {
                v_conn.ConnectionString = p_strConnection_String.Trim();

                v_conn.Open();

                v_conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return v_conn;
        }

        /// <summary>
        /// Lấy dữ liệu từ db
        /// </summary>
        /// <param name="p_conn"></param>
        /// <param name="p_trans"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void Fill_Data_Table(SqlConnection p_conn, SqlTransaction p_trans, DataTable p_dt, string p_strStored_Name, params object[] p_arrParams)
        {

            SqlCommand v_command = new SqlCommand();
            v_command.CommandType = CommandType.StoredProcedure;
            v_command.Connection = p_conn;
            v_command.Transaction = p_trans;
            v_command.CommandTimeout = CConfig.Time_Out;
            v_command.CommandText = p_strStored_Name;

            SqlCommandBuilder.DeriveParameters(v_command);

            if (v_command.Parameters.Count - 1 != p_arrParams.Length)
                throw new Exception("Số lượng tham số truyền vào không đúng.");

            if (p_arrParams != null && p_arrParams.Length > 0)
            {
                for (int i = 0; i < p_arrParams.Length; i++)
                {
                    if (p_arrParams[i] == null)
                        v_command.Parameters[i + 1].Value = DBNull.Value;
                    else
                        v_command.Parameters[i + 1].Value = p_arrParams[i];
                }
            }

            SqlDataAdapter v_da = new(v_command);
            v_da.Fill(p_dt);
        }

        /// <summary>
        /// Lấy dữ liệu từ db
        /// </summary>
        /// <param name="p_strConnection_String"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        /// <returns></returns>
        public static void Fill_Data_Table(string p_strConnection_String, DataTable p_dt, string p_strStored_Name, params object[] p_arrParams)
        {

            SqlConnection v_conn = null;
            SqlTransaction v_trans = null;

            DateTime v_dtmStart = DateTime.Now;

            try
            {
                v_conn = CreateConnection(p_strConnection_String);
                v_conn.Open();
                v_trans = v_conn.BeginTransaction();

                Fill_Data_Table(v_conn, v_trans, p_dt, p_strStored_Name, p_arrParams);

                v_trans.Commit();
            }
            catch (Exception ex)
            {
                TimeSpan v_span = DateTime.Now - v_dtmStart;

                CLogger.Save_Trace_Log("Sql-Fill_Data_Table", "Fill_Data_Table", "Error: " + ex.Message, v_span.TotalSeconds); // Ghi log

                if (v_trans != null)
                    v_trans.Rollback();

                throw ex;
            }
            finally
            {
                if (v_trans != null)
                    v_trans.Dispose();

                if (v_conn != null)
                    v_conn.Close();
            }

        }

        /// <summary>
        /// Thực thi store có giá trị trả về
        /// </summary>
        /// <param name="p_conn"></param>
        /// <param name="p_trans"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object Execute_Scalar(SqlConnection p_conn, SqlTransaction p_trans, string p_strStored_Name, params object[] p_arrParams)
        {
            object v_objRes = CConst.OBJ_VALUE_NULL;

            SqlCommand v_command = new();
            v_command.CommandType = CommandType.StoredProcedure;
            v_command.Connection = p_conn;
            v_command.Transaction = p_trans;
            v_command.CommandTimeout = CConfig.Time_Out;
            v_command.CommandText = p_strStored_Name;

            SqlCommandBuilder.DeriveParameters(v_command);

            if (v_command.Parameters.Count - 1 != p_arrParams.Length)
                throw new Exception("Số lượng tham số truyền vào không đúng.");

            if (p_arrParams != null && p_arrParams.Length > 0)
            {
                for (int i = 0; i < p_arrParams.Length; i++)
                {
                    if (p_arrParams[i] == null)
                        v_command.Parameters[i + 1].Value = DBNull.Value;
                    else
                        v_command.Parameters[i + 1].Value = p_arrParams[i];
                }
            }

            v_objRes = v_command.ExecuteScalar();

            return v_objRes;
        }

        /// <summary>
        /// Thực thi store có giá trị trả về
        /// </summary>
        /// <param name="p_strConnection_String"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        /// <returns></returns>
        public static object Execute_Scalar(string p_strConnection_String, string p_strStored_Name, params object[] p_arrParams)
        {
            object v_objRes = CConst.OBJ_VALUE_NULL;
            SqlConnection v_conn = null;
            SqlTransaction v_trans = null;
            DateTime v_dtmStart = DateTime.Now;

            try
            {
                v_conn = CreateConnection(p_strConnection_String);
                v_conn.Open();
                v_trans = v_conn.BeginTransaction();

                v_objRes = Execute_Scalar(v_conn, v_trans, p_strStored_Name, p_arrParams);

                v_trans.Commit();

            }
            catch (Exception ex)
            {
                TimeSpan v_span = DateTime.Now - v_dtmStart;

                CLogger.Save_Trace_Log("Sql-Execute_Scalar", "Execute_Scalar", "Error: " + ex.Message, v_span.TotalSeconds); // Ghi log

                if (v_trans != null)
                    v_trans.Rollback();

                throw ex;
            }
            finally
            {
                if (v_trans != null)
                    v_trans.Dispose();

                if (v_conn != null)
                    v_conn.Close();
            }

            return v_objRes;
        }

        /// <summary>
        /// Thực thi store không có tham số trả về
        /// </summary>
        /// <param name="p_conn"></param>
        /// <param name="p_trans"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        /// <exception cref="Exception"></exception>
        public static void Execute_Nonquery(SqlConnection p_conn, SqlTransaction p_trans, string p_strStored_Name, params object[] p_arrParams)
        {
            SqlCommand v_command = new SqlCommand();
            v_command.CommandType = CommandType.StoredProcedure;
            v_command.Connection = p_conn;
            v_command.Transaction = p_trans;
            v_command.CommandTimeout = CConfig.Time_Out;
            v_command.CommandText = p_strStored_Name;

            SqlCommandBuilder.DeriveParameters(v_command);

            if (v_command.Parameters.Count - 1 != p_arrParams.Length)
                throw new Exception("Số lượng tham số truyền vào không đúng.");

            if (p_arrParams != null && p_arrParams.Length > 0)
            {
                for (int i = 0; i < p_arrParams.Length; i++)
                {
                    if (p_arrParams[i] == null)
                        v_command.Parameters[i + 1].Value = DBNull.Value;
                    else
                        v_command.Parameters[i + 1].Value = p_arrParams[i];
                }
            }

            v_command.ExecuteNonQuery();
        }

        /// <summary>
        /// Thực thi store không có tham số trả về
        /// </summary>
        /// <param name="p_strConnection_String"></param>
        /// <param name="p_strStored_Name"></param>
        /// <param name="p_arrParams"></param>
        public static void Execute_Nonquery(string p_strConnection_String, string p_strStored_Name, params object[] p_arrParams)
        {
            SqlConnection v_conn = null;
            SqlTransaction v_trans = null;

            DateTime v_dtmStart = DateTime.Now;

            try
            {
                v_conn = CreateConnection(p_strConnection_String);
                v_conn.Open();
                v_trans = v_conn.BeginTransaction();

                Execute_Nonquery(v_conn, v_trans, p_strStored_Name, p_arrParams);

                v_trans.Commit();
            }
            catch (Exception ex)
            {
                TimeSpan v_span = DateTime.Now - v_dtmStart;

                CLogger.Save_Trace_Log("Sql-Execute_Nonquery", "Execute_Nonquery", "Execute: Error" + ex.Message, v_span.TotalSeconds); // Ghi log

                if (v_trans != null)
                    v_trans.Rollback();

                throw ex;
            }
            finally
            {
                if (v_trans != null)
                    v_trans.Dispose();

                if (v_conn != null)
                    v_conn.Close();
            }

        }
    }
}