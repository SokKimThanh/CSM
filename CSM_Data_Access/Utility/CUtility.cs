using System.Net.Mail;


namespace CSM_Data_Access.Utility
{
    public class CUtility
    {
        public static Random Rand = new();

        #region Nhóm convert
        public static T Convert_Json_To_Object<T>(string p_strJson_Path)
        {
            // Đọc nội dung của tệp tin
            string v_strJson_Content = File.ReadAllText(p_strJson_Path);
            return JsonConvert.DeserializeObject<T>(v_strJson_Content);
        }

        public static string Convert_To_String(object p_objData)
        {
            if (p_objData == DBNull.Value || p_objData.ToString() == CConst.STR_VALUE_NULL)
                return CConst.STR_VALUE_NULL;

            return Convert.ToString(p_objData);
        }

        public static DateTime? Convert_To_DateTime(object p_objData, string p_strFormat)
        {
            if (p_objData == DBNull.Value)
                return CConst.DTM_VALUE_NULL;

            string v_strDateTime = Convert_To_String(p_objData);
            CultureInfo v_provider = CultureInfo.InvariantCulture;

            return DateTime.ParseExact(v_strDateTime, p_strFormat, v_provider);
        }

        public static DateTime? Convert_To_DateTime(object p_objData)
        {
<<<<<<< HEAD
            if (p_objData != DBNull.Value && p_objData != null)
                return Convert_To_DateTime(p_objData, CConfig.Short_Day_Format_String);
=======
            if (p_objData != System.DBNull.Value && p_objData != null)
                return Convert.ToDateTime(p_objData, CultureInfo.InvariantCulture);
>>>>>>> 8cfec4e2dc6707e6832cc79315f5049f144870d4
            else
                return CConst.DTM_VALUE_NULL;
        }

        public static int Convert_To_Int32(object p_objData)
        {
            if (p_objData == DBNull.Value || Convert_To_String(p_objData) == CConst.STR_VALUE_NULL)
                return CConst.INT_VALUE_NULL;

            return Convert.ToInt32(p_objData);
        }

        public static long Convert_To_Int64(object p_objData)
        {
            if (p_objData == DBNull.Value || Convert_To_String(p_objData) == CConst.STR_VALUE_NULL)
                return CConst.INT_VALUE_NULL;

            return Convert.ToInt64(p_objData);
        }

        public static bool Convert_To_Bool(object p_objData)
        {
            if (p_objData == DBNull.Value || Convert_To_String(p_objData) == CConst.STR_VALUE_NULL)
                return CConst.IS_VALUE_NULL;

            return Convert.ToBoolean(p_objData);
        }

        public static double Convert_To_Double(object p_objData)
        {
            if (p_objData == DBNull.Value || Convert_To_String(p_objData) == CConst.STR_VALUE_NULL)
                return CConst.DB_VALUE_NULL;

            return Convert.ToDouble(p_objData);
        }

        public static string Convert_DateTime_To_String(DateTime? p_dtmData)
        {
            if (p_dtmData == CConst.DTM_VALUE_NULL ||
                Convert_To_String(p_dtmData.Value) == CConst.STR_VALUE_NULL)
            {
                return CConst.STR_VALUE_NULL;
            }

            return p_dtmData.Value.ToString(CConfig.Short_Day_Format_String);
        }

        public static DateTime? Convert_String_To_Datetime(string p_strDate)
        {
            if (p_strDate == CConst.STR_VALUE_NULL)
                return CConst.DTM_VALUE_NULL;

            return Convert_String_To_Datetime(p_strDate, CConfig.Short_Day_Format_String);
        }

        public static DateTime? Convert_String_To_Datetime(string p_strDate, string p_strFormat)
        {
            if (p_strDate == CConst.STR_VALUE_NULL)
                return CConst.DTM_VALUE_NULL;

            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(p_strDate, p_strFormat, provider);
        }

        #endregion 

        /// <summary>
        /// Đọc 1 field trong file json
        /// </summary>
        /// <param name="p_strJson_Path"></param>
        /// <param name="v_strFieldName"></param>
        /// <returns></returns>
        public static string Map_Json_To_Entity_Field(string p_strJson_Path, string v_strField_Name)
        {
            string v_strRes = CConst.STR_VALUE_NULL;
            if (!File.Exists(p_strJson_Path))//Kiểm tra file tồn tại          
                throw new Exception($"File path: [{p_strJson_Path}] doesn't exits.");

            //Chuyển về đối tượng động để xử lý
            dynamic v_objValue = Convert_Json_To_Object<dynamic>(p_strJson_Path);

            //Convert theo cột
            v_strRes = Convert_To_String(v_objValue[v_strField_Name]);

            return v_strRes;
        }

        /// <summary>
        /// Lấy đường dẫn của 1 file 
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string Get_File_Path_By_File_Name(string p_strFileName)
        {
            string v_strRes = CConst.STR_VALUE_NULL;

            v_strRes = Path.GetFullPath(p_strFileName);

            return v_strRes;
        }

        /// <summary>
        /// Chuyển 1 row trong data table thành 1 đối tượng cụ thể
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_Row"></param>
        /// <returns></returns>
        public static T Map_Row_To_Entity<T>(DataRow p_Row) where T : new()
        {
            //Tạo 1 obj
            T v_objItem = new();

            foreach (DataColumn v_colValue in p_Row.Table.Columns)
            {
                //  Tìm property của object
                PropertyInfo v_objItem_Info = v_objItem.GetType().GetProperty(v_colValue.ColumnName);

                // Kiểm tra giá trị có null hay không
                if (v_objItem_Info != null && p_Row[v_colValue] != DBNull.Value)
                {
                    MethodInfo v_objMethodInfo = v_objItem_Info.SetMethod;

                    // Kiểm tra propertie đó có cho phép gán dữ liệu không
                    if (v_objMethodInfo != null)
                    {
                        string v_strTypedata = v_colValue.DataType.Name;
                        switch (v_strTypedata)
                        {
                            case "String": v_objItem_Info.SetValue(v_objItem, Convert_To_String(p_Row[v_colValue])); break;
                            case "Int16": v_objItem_Info.SetValue(v_objItem, Convert_To_Int32(p_Row[v_colValue])); break;
                            case "Int32": v_objItem_Info.SetValue(v_objItem, Convert_To_Int32(p_Row[v_colValue])); break;
                            case "Int64": v_objItem_Info.SetValue(v_objItem, Convert_To_Int64(p_Row[v_colValue])); break;
                            case "DateTime": v_objItem_Info.SetValue(v_objItem, Convert_To_DateTime(p_Row[v_colValue])); break;
                            case "DateTime?": v_objItem_Info.SetValue(v_objItem, Convert_To_DateTime(p_Row[v_colValue])); break;
                            case "Double": v_objItem_Info.SetValue(v_objItem, Convert_To_Double(p_Row[v_colValue]), null); break;
                            case "Decimal": v_objItem_Info.SetValue(v_objItem, Convert_To_Double(p_Row[v_colValue]), null); break;
                            case "Boolean": v_objItem_Info.SetValue(v_objItem, Convert_To_Bool(p_Row[v_colValue]), null); break;
                        }
                    }
                }
            }

            return v_objItem;
        }

        /// <summary>
        /// Dùng để design dữ liệu
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        private static string Get_String_For_Tao_Key(object p_objData)
        {
            if (p_objData == null)
                return "";

            string v_strType = p_objData.GetType().Name.ToLower();
            string v_strRes = "";

            //Lấy kiểu dữ liệu để sử dụng hàm convert tương ứng (vì nó là object) để design text
            switch (v_strType.ToLower())
            {
                case "string": v_strRes = Convert_To_String(p_objData); break;
                case "int32": v_strRes = Convert_To_Int32(p_objData).ToString("###0"); break;
                case "int64": v_strRes = Convert_To_Int64(p_objData).ToString("###0"); break;
                case "double": v_strRes = Convert_To_Double(p_objData).ToString("###0.#####;-###0.#####;0").Replace(",", "."); break;
                case "float": v_strRes = Convert_To_Double(p_objData).ToString("###0.#####;-###0.#####;0").Replace(",", "."); break;
                case "datetime":
                    v_strRes = Convert_DateTime_To_String(Convert_To_DateTime(p_objData));
                    break;
                case "bool":
                    if (Convert_To_Bool(p_objData) == true)
                        v_strRes = "1";
                    else
                        v_strRes = "0";
                    break;
            }

            if (v_strRes == "")
                v_strRes = "";

            return v_strRes;
        }

        /// <summary>
        /// Tạo key
        /// </summary>
        /// <param name="p_arrValue"></param>
        /// <returns></returns>
        public static string Tao_Key(params object[] p_arrValue)
        {
            string v_strKey = "";
            if (p_arrValue == null)
                return "";

            for (int i = 0; i < p_arrValue.Length; i++)
            {
                string v_strValue = Get_String_For_Tao_Key(p_arrValue[i]);

                if (v_strKey == "")
                    v_strKey = v_strValue;
                else
                {
                    v_strKey += "|" + v_strValue;
                }
            }

            return v_strKey.ToUpper();
        }

        /// <summary>
        /// Design dữ liệu cho combobox
        /// </summary>
        /// <param name="p_objData"></param>
        /// <returns></returns>
        private static string Get_String_For_Tao_Combo(object p_objData)
        {
            if (p_objData == null)
                return "";

            string v_strType = p_objData.GetType().Name.ToLower();
            string v_strRes = "";

            //Lấy kiểu dữ liệu để sử dụng hàm convert tương ứng (vì nó là object) để design text
            switch (v_strType.ToLower())
            {
                case "string": v_strRes = Convert_To_String(p_objData); break;
                case "int32": v_strRes = Convert_To_Int32(p_objData).ToString("###0"); break;
                case "int64": v_strRes = Convert_To_Int64(p_objData).ToString("###0"); break;
                case "double": v_strRes = Convert_To_Double(p_objData).ToString("###0.#####;-###0.#####;0").Replace(",", "."); break;
                case "float": v_strRes = Convert_To_Double(p_objData).ToString("###0.#####;-###0.#####;0").Replace(",", "."); break;
                case "datetime":
                    v_strRes = Convert_DateTime_To_String(Convert_To_DateTime(p_objData));
                    break;
                case "bool":
                    if (Convert_To_Bool(p_objData) == true)
                        v_strRes = "1";
                    else
                        v_strRes = "0";
                    break;
            }

            if (v_strRes == "")
                v_strRes = "";

            return v_strRes;
        }

        /// <summary>
        /// Tạo dữ liệu hiển thị cho combo
        /// </summary>
        /// <param name="p_arrValue"></param>
        /// <returns></returns>
        public static string Tao_Combo_Text(params object[] p_arrValue)
        {
            string v_strKey = "";
            if (p_arrValue == null) // Check rỗng
                return "";

            for (int i = 0; i < p_arrValue.Length; i++)
            {
                string v_strValue = Get_String_For_Tao_Combo(p_arrValue[i]);

                if (v_strKey == "")
                    v_strKey = v_strValue;
                else
                {
                    if (v_strValue != "")
                        v_strKey += " (" + v_strValue + ")";
                }
            }

            return v_strKey;
        }

        /// <summary>
        /// Mã hóa 1 chuỗi kí tự theo MD5
        /// </summary>
        /// <param name="p_strSource"></param>
        /// <returns></returns>
        public static string MD5_Encrypt(string p_strSource)
        {
            UTF8Encoding p_utf8encoding = new UTF8Encoding();
            byte[] p_arrData = p_utf8encoding.GetBytes(p_strSource);

            System.Security.Cryptography.MD5CryptoServiceProvider p_md5Encrypt = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] v_arrHashData = p_md5Encrypt.ComputeHash(p_arrData);

            string v_strResult = "";

            for (int i = 0; i < v_arrHashData.Length; i++)
                v_strResult += Convert.ToString(v_arrHashData[i], 16).PadLeft(2, 'j');

            return v_strResult.PadLeft(32, 'n');
        }

        /// <summary>
        /// Kiểm tra dữ liệu có phải kiểu số hay không
        /// </summary>
        /// <param name="p_type"></param>
        /// <returns></returns>
        public static bool Is_Numeric_Type(Type p_type)
        {
            List<Type> v_arrNum_Type = new()
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),    // Int16
                typeof(ushort),
                typeof(int),      // Int32
                typeof(uint),
                typeof(long),     // Int64
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(Int16),
                typeof(Int32),
                typeof(Int64),
                typeof(UInt16),
                typeof(UInt32),
                typeof(UInt64)
            };

            return v_arrNum_Type.Contains(p_type);
        }

        /// <summary>
        /// Hàm tạo id theo chiều dài số vd: lengt = 8 -> id = 12345678
        /// </summary>
        /// <param name="p_iLenght"></param>
        /// <returns></returns>
        public static long Create_Rand_ID(int p_iLenght)
        {
            string v_strRes = CConst.STR_VALUE_NULL;
            for (int i = 0; i < p_iLenght; i++)
            {
                v_strRes += Convert_To_String(Rand.Next(0, 10));
            }

            return Convert_To_Int64(v_strRes);
        }

        /// <summary>
        /// Hàm send maild đẩy đủ
        /// </summary>
        /// <param name="p_strFrom"></param>
        /// <param name="p_strTo"></param>
        /// <param name="p_strSubject"></param>
        /// <param name="p_strMessage"></param>
        /// <param name="p_strHost"></param>
        /// <param name="p_bUseDefaultCredentials"></param>
        /// <param name="p_iPort"></param>
        /// <param name="p_strUser"></param>
        /// <param name="p_strPass"></param>
        /// <param name="p_bEnableSsl"></param>
        /// <param name="p_strAttach"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool Send_Mail_Use_SMTP(string p_strFrom, string p_strTo, string p_strSubject,
                                            string p_strMessage, string p_strHost, bool p_bUseDefaultCredentials, int p_iPort,
                                            string p_strUser, string p_strPass, bool p_bEnableSsl, string p_strAttach)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

            bool result = regex.IsMatch(p_strFrom);
            if (result == false)
            {
                throw new Exception("Email không hợp lệ!!!");
            }
            else
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient smtp = new SmtpClient(p_strHost);

                mail.From = new MailAddress(p_strFrom);

                p_strTo = p_strTo.Replace(",", ";");
                string[] v_arrMail_To = p_strTo.Split(';');

                foreach (string v_strTemp in v_arrMail_To)
                {
                    if (v_strTemp.Trim() != "")
                        mail.To.Add(v_strTemp.Trim());
                }

                mail.Subject = p_strSubject;
                mail.Body = p_strMessage;
                mail.IsBodyHtml = true;

                smtp.UseDefaultCredentials = p_bUseDefaultCredentials;
                smtp.Port = p_iPort;
                smtp.Credentials = new System.Net.NetworkCredential(p_strUser, p_strPass);
                smtp.EnableSsl = p_bEnableSsl;

                if (p_strAttach != "")
                    mail.Attachments.Add(new Attachment(p_strAttach));

                smtp.Send(mail);
                return true;
            }
        }

        /// <summary>
        /// Hàm send mail dựa trên cấu hình đã gán trong appsetting
        /// </summary>
        /// <param name="p_strTo"></param>
        /// <param name="p_strSubject"></param>
        /// <param name="p_strMessage"></param>
        /// <param name="p_strAttach"></param>
        /// <returns></returns>
        public static bool Send_Mail_Use_SMTP(string p_strTo, string p_strSubject, string p_strMessage, string p_strAttach)
        {
            return Send_Mail_Use_SMTP(CConfig.Email_From, p_strTo, p_strSubject, p_strMessage, CConfig.Smtp_Host, CConfig.Smtp_UseDefaultCredentials, CConfig.Smtp_Port,
                CConfig.Email_From, CConfig.Smtp_Pass, CConfig.Smtp_EnableSsl, p_strAttach);
        }
    }
}