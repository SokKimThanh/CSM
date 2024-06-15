namespace CSM_Data_Access.Utility
{
    public class CUtility_Date
    {
        /// <summary>
        /// Convert ngày về đầu ngày.
        /// VD: 03/01/2017 14:22:11 thì sẽ chuyển thành 03/01/2017 00:00:00
        /// </summary>
        /// <param name="p_dtmDate"></param>
        /// <returns></returns>
        public static DateTime? Convert_To_Dau_Ngay(DateTime? p_dtmDate)
        {
            DateTime? v_dtmRes = p_dtmDate;

            if (p_dtmDate == null)
                return null;

            v_dtmRes = CUtility.Convert_String_To_Datetime(p_dtmDate.Value.ToString("dd/MM/yyyy") + " 00:00:00", "dd/MM/yyyy HH:mm:ss");
            return v_dtmRes;
        }

        public static DateTime Convert_To_Dau_Ngay(DateTime p_dtmDate)
        {
            DateTime v_dtmRes = p_dtmDate;
            v_dtmRes = CUtility.Convert_String_To_Datetime(p_dtmDate.ToString("dd/MM/yyyy") + " 00:00:00", "dd/MM/yyyy HH:mm:ss").Value;
            return v_dtmRes;
        }

        /// <summary>
        /// Convert ngày về cuối ngày.
        /// VD: 03/01/2017 14:22:11 thì sẽ chuyển thành 03/01/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmDate"></param>
        /// <returns></returns>
        public static DateTime? Convert_To_Cuoi_Ngay(DateTime? p_dtmDate)
        {
            DateTime? v_dtmRes = p_dtmDate;

            if (p_dtmDate == null)
                return null;

            v_dtmRes = CUtility.Convert_String_To_Datetime(p_dtmDate.Value.ToString("dd/MM/yyyy") + " 23:59:59", "dd/MM/yyyy HH:mm:ss");
            return v_dtmRes;
        }

        /// <summary>
        /// Convert ngày về cuối ngày.
        /// VD: 03/01/2017 14:22:11 thì sẽ chuyển thành 03/01/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmDate"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Cuoi_Ngay(DateTime p_dtmDate)
        {
            DateTime v_dtmRes = p_dtmDate;

            v_dtmRes = (DateTime)CUtility.Convert_String_To_Datetime(p_dtmDate.ToString("dd/MM/yyyy") + " 23:59:59", "dd/MM/yyyy HH:mm:ss");
            return v_dtmRes;
        }
    }
}