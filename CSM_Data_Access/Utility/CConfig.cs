namespace CSM_Data_Access.Utility
{
    public class CConfig
    {
        //Thông tin chuỗi kết nối
        public static string CSM_Cinema_DB_Conn_String = CConst.STR_VALUE_NULL;

        //Thông tin sản phẩm
        public static string Product_Name = CConst.STR_VALUE_NULL;
        public static string Product_Version = CConst.STR_VALUE_NULL;
        public static string Product_Auth = CConst.STR_VALUE_NULL;
        public static DateTime? Created = null;
        public static string Created_By = CConst.STR_VALUE_NULL;

        //Các chuỗi path thư mục
        public static string Folder_File_Management_Path = "C:\\File_Management\\";

        //Format
        public static string Short_Day_Format_String = CConst.STR_VALUE_NULL;

        //Time out
        public static int Time_Out = CConst.INT_VALUE_NULL;

        //Kết nối để gửi mail
        public static string Smtp_Host = CConst.STR_VALUE_NULL;
        public static bool Smtp_UseDefaultCredentials = CConst.IS_VALUE_NULL;
        public static int Smtp_Port = CConst.INT_VALUE_NULL;
        public static string Smtp_User = CConst.STR_VALUE_NULL;
        public static string Smtp_Pass = CConst.STR_VALUE_NULL;
        public static bool Smtp_EnableSsl = CConst.IS_VALUE_NULL;
        public static string Email_From = CConst.STR_VALUE_NULL;
    }
}