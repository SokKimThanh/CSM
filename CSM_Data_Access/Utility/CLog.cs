
using System.Runtime.CompilerServices;

namespace CSM_Data_Access.Utility
{
    public class CLog
    {
        private string m_strFunction_Code;
        private string m_strFunction_Name;
        private string m_strDescription;
        private double m_dblTotal_Time;

        public CLog()
        {
            Reset_Data();
        }

        public string Function_Code { get => m_strFunction_Code; set => m_strFunction_Code = value.Trim(); }
        public string Function_Name { get => m_strFunction_Name; set => m_strFunction_Name = value.Trim(); }
        public string Description { get => m_strDescription; set => m_strDescription = value.Trim(); }
        public double Total_Time { get => m_dblTotal_Time; set => m_dblTotal_Time = value; }

        public void Reset_Data()
        {
            m_strFunction_Code = CConst.STR_VALUE_NULL;
            m_strFunction_Name = CConst.STR_VALUE_NULL;
            m_strDescription = CConst.STR_VALUE_NULL;
            m_dblTotal_Time = CConst.DB_VALUE_NULL;
        }

        public void Write_To_Txt(string p_strFile_Path)
        {
            // Tạo một đối tượng FileInfo
            FileInfo v_FileInfo = new(p_strFile_Path);

            // Kiểm tra xem tệp có tồn tại không
            if (!v_FileInfo.Exists)
            {
                // Nếu không tồn tại, tạo mới
                using FileStream fs = v_FileInfo.Create();
            }

            // Sử dụng StreamWriter để ghi vào tệp tin
            using (StreamWriter v_sw = v_FileInfo.AppendText())
            {
                StringBuilder v_sb = new();
                v_sb.Append(m_strFunction_Code);
                v_sb.Append("|");
                v_sb.Append(m_strFunction_Name);
                v_sb.Append("|");
                v_sb.Append(m_strDescription);
                v_sb.Append("|");
                v_sb.AppendLine(CUtility.Convert_To_String(m_dblTotal_Time) + "\n");

                v_sw.Write(v_sb.ToString());
            }


        }
    }
}