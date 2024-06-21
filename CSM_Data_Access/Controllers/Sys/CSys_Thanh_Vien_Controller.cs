using CSM_Data_Access.DataLayer;
using CSM_Data_Access.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM_Data_Access.Controllers.Sys
{
    public class CSys_Thanh_Vien_Controller
    {
        public List<CSys_Thanh_Vien> FSys_001_List_Thanh_Vien(DateTime? p_dtmFrom, DateTime? p_dtmTo)
        {
            List<CSys_Thanh_Vien> v_arrRes = new List<CSys_Thanh_Vien>();
            DataTable v_dt = new DataTable();

            try
            {
                p_dtmFrom = CUtility_Date.Convert_To_Dau_Ngay(p_dtmFrom);
                p_dtmTo = CUtility_Date.Convert_To_Cuoi_Ngay(p_dtmTo);

                CSqlHelper.Fill_Data_Table(CConfig.CSM_Cinema_DB_Conn_String, v_dt, "FSys_001_List_Thanh_Vien", p_dtmFrom, p_dtmTo);

                foreach (DataRow v_row in v_dt.Rows)
                {
                    CSys_Thanh_Vien v_objRes = CUtility.Map_Row_To_Entity<CSys_Thanh_Vien>(v_row);
                    v_arrRes.Add(v_objRes);
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_arrRes;
        }

        public List<CSys_Thanh_Vien> FSys_001_Cache_Thanh_Vien()
        {
            List<CSys_Thanh_Vien> v_arrRes = new List<CSys_Thanh_Vien>();
            DataTable v_dt = new DataTable();

            try
            {
                CSqlHelper.Fill_Data_Table(CConfig.CSM_Cinema_DB_Conn_String, v_dt, "FSys_001_Cache_Thanh_Vien");

                foreach (DataRow v_row in v_dt.Rows)
                {
                    CSys_Thanh_Vien v_objRes = CUtility.Map_Row_To_Entity<CSys_Thanh_Vien>(v_row);
                    v_arrRes.Add(v_objRes);
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_arrRes;
        }

        public CSys_Thanh_Vien FSys_001_Get_Thanh_Vien_By_ID(long p_iID)
        {
            CSys_Thanh_Vien v_objRes = null;
            DataTable v_dt = new DataTable();

            try
            {
                CSqlHelper.Fill_Data_Table(CConfig.CSM_Cinema_DB_Conn_String, v_dt, "FSys_001_Get_Thanh_Vien_By_ID", p_iID);

                if (v_dt.Rows.Count > 0)
                {
                    v_objRes = CUtility.Map_Row_To_Entity<CSys_Thanh_Vien>(v_dt.Rows[0]);
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_objRes;
        }

        public long FSys_001_Insert_Thanh_Vien(CSys_Thanh_Vien p_objData)
        {
            long v_iRes = CConst.INT_VALUE_NULL;

            try
            {
                v_iRes = Convert.ToInt64(CSqlHelper.Execute_Scalar(CConfig.CSM_Cinema_DB_Conn_String, "FSys_001_Insert_Thanh_Vien",
                    p_objData.Ma_Dang_Nhap, p_objData.Mat_Khau, p_objData.NTV_ID, p_objData.Ghi_Chu, p_objData.Last_Updated_By,
                    p_objData.Last_Updated_Function));
            }

            catch (Exception)
            {
                throw;
            }

            return v_iRes;
        }

        public long FSys_001_Insert_Thanh_Vien(SqlConnection p_conn, SqlTransaction p_trans, CSys_Thanh_Vien p_objData)
        {
            long v_iRes = CConst.INT_VALUE_NULL;

            try
            {
                v_iRes = Convert.ToInt64(CSqlHelper.Execute_Scalar(p_conn, p_trans, CConfig.CSM_Cinema_DB_Conn_String, "FSys_001_Insert_Thanh_Vien",
                    p_objData.Ma_Dang_Nhap, p_objData.Mat_Khau, p_objData.NTV_ID, p_objData.Ghi_Chu, p_objData.Last_Updated_By,
                    p_objData.Last_Updated_Function));
            }

            catch (Exception)
            {
                throw;
            }

            return v_iRes;
        }

        public void FSys_001_Update_Thanh_Vien(CSys_Thanh_Vien p_objData)
        {
            try
            {
                CSqlHelper.Execute_Nonquery(CConfig.CSM_Cinema_DB_Conn_String, "FQ_531_TV_sp_upd_Update", p_objData.Auto_ID,
                    p_objData.Ma_Dang_Nhap, p_objData.Mat_Khau, p_objData.NTV_ID, p_objData.Ghi_Chu, p_objData.Last_Updated_By,
                    p_objData.Last_Updated_Function);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void FSys_001_Update_Thanh_Vien(SqlConnection p_conn, SqlTransaction p_trans, CSys_Thanh_Vien p_objData)
        {
            try
            {
                CSqlHelper.Execute_Nonquery(p_conn, p_trans, CConfig.CSM_Cinema_DB_Conn_String, "FQ_531_TV_sp_upd_Update", p_objData.Auto_ID,
                    p_objData.Ma_Dang_Nhap, p_objData.Mat_Khau, p_objData.NTV_ID, p_objData.Ghi_Chu, p_objData.Last_Updated_By,
                    p_objData.Last_Updated_Function);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void FSys_001_Delete_Thanh_Vien_By_ID(long p_iAuto_ID, string p_strLast_Updated_By, string p_strLast_Updated_By_Function)
        {
            try
            {
                CSqlHelper.Execute_Nonquery(CConfig.CSM_Cinema_DB_Conn_String, "FSys_001_Delete_Thanh_Vien_By_ID", p_iAuto_ID, p_strLast_Updated_By, p_strLast_Updated_By_Function);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
