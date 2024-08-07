using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM_Data_Access.Entity.Sys
{
    public class CSys_Thanh_Vien
    {
        private long m_lngAuto_ID;
        private string m_strMa_Dang_Nhap;
        private string m_strMat_Khau;
        private long m_lngNTV_ID;
        private string m_strGhi_Chu;
        private int m_intdeleted;
        private DateTime? m_dtmCreated;
        private string m_strCreated_By;
        private string m_strCreated_by_Function;
        private DateTime? m_dtmLast_Updated;
        private string m_strLast_Updated_By;
        private string m_strLast_Updated_Function;

        public CSys_Thanh_Vien()
        {
            ResetData();
        }

        public void ResetData()
        {
            m_lngAuto_ID = CConst.INT_VALUE_NULL;
            m_strMa_Dang_Nhap = CConst.STR_VALUE_NULL;
            m_strMat_Khau = CConst.STR_VALUE_NULL;
            m_lngNTV_ID = CConst.INT_VALUE_NULL;
            m_strGhi_Chu = CConst.STR_VALUE_NULL;
            m_intdeleted = CConst.INT_VALUE_NULL;
            m_dtmCreated = CConst.DTM_VALUE_NULL;
            m_strCreated_By = CConst.STR_VALUE_NULL;
            m_strCreated_by_Function = CConst.STR_VALUE_NULL;
            m_dtmLast_Updated = CConst.DTM_VALUE_NULL;
            m_strLast_Updated_By = CConst.STR_VALUE_NULL;
            m_strLast_Updated_Function = CConst.STR_VALUE_NULL;
        }

        public long Auto_ID
        {
            get
            {
                return m_lngAuto_ID;
            }
        }

        public string Ma_Dang_Nhap
        {
            get
            {
                return m_strMa_Dang_Nhap;
            }
            set
            {
                m_strMa_Dang_Nhap = value.Trim();
            }
        }

        public string Mat_Khau
        {
            get
            {
                return m_strMat_Khau;
            }
            set
            {
                m_strMat_Khau = value.Trim();
            }
        }

        public long NTV_ID
        {
            get
            {
                return m_lngNTV_ID;
            }
            set
            {
                m_lngNTV_ID = value;
            }
        }

        public string Ghi_Chu
        {
            get
            {
                return m_strGhi_Chu;
            }
            set
            {
                m_strGhi_Chu = value.Trim();
            }
        }

        public int deleted
        {
            get
            {
                return m_intdeleted;
            }
            set
            {
                m_intdeleted = value;
            }
        }

        public DateTime? Created
        {
            get
            {
                return m_dtmCreated;
            }
            set
            {
                m_dtmCreated = value;
            }
        }

        public string Created_By
        {
            get
            {
                return m_strCreated_By;
            }
            set
            {
                m_strCreated_By = value.Trim();
            }
        }

        public string Created_by_Function
        {
            get
            {
                return m_strCreated_by_Function;
            }
            set
            {
                m_strCreated_by_Function = value.Trim();
            }
        }

        public DateTime? Last_Updated
        {
            get
            {
                return m_dtmLast_Updated;
            }
            set
            {
                m_dtmLast_Updated = value;
            }
        }

        public string Last_Updated_By
        {
            get
            {
                return m_strLast_Updated_By;
            }
            set
            {
                m_strLast_Updated_By = value.Trim();
            }
        }

        public string Last_Updated_Function
        {
            get
            {
                return m_strLast_Updated_Function;
            }
            set
            {
                m_strLast_Updated_Function = value.Trim();
            }
        }
    }
}
