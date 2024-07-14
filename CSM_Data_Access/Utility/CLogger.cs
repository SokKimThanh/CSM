using System.Runtime.InteropServices;

namespace CSM_Data_Access.Utility
{
    public class CLogger
    {
        private static CLog m_objLog = new();

        public static void Save_Trace_Log(string p_strFunc_Code, string p_strFunction_Name, string p_strDescription, double p_dblTime_Second_Excute)
        {
            DateTime v_dtmNow = DateTime.Now;
            string p_strFile_Path = Path.Combine(CConfig.Folder_File_Management_Path, "log_txt", $"Log{v_dtmNow:ddMMyyyy}.txt"); // Lưu log 1 ngày

            m_objLog.Reset_Data();

            m_objLog.Function_Code = p_strFunc_Code;
            m_objLog.Function_Name = p_strFunction_Name;
            m_objLog.Description = p_strDescription;
            m_objLog.Total_Time = p_dblTime_Second_Excute;

            m_objLog.Write_To_Txt(p_strFile_Path);
        }
    }
}