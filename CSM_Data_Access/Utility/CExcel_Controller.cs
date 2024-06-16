
namespace CSM_Data_Access.Utility
{
    public class CExcel_Controller
    {

        private ExcelPackage m_objExcel_Package = new ExcelPackage();

        public CExcel_Controller(FileInfo p_objFile_Info)
        {
            m_objExcel_Package = new ExcelPackage(p_objFile_Info);
        }

        /// <summary>
        /// Check kiểu file
        /// </summary>
        /// <param name="p_strFileExtension "></param>
        /// <returns></returns>
        public static bool Check_Excel_File_Type(string p_strFileExtension)
        {
            if (p_strFileExtension != ".xls" && p_strFileExtension != ".xlsx")
                return false;
            return true;
        }

        /// <summary>
        /// Check kiểu file
        /// </summary>
        /// <param name="p_arrList_Extensions"></param>
        /// <param name="p_strCheck"></param>
        /// <returns></returns>
        public static bool Check_Excel_File_Type(List<string> p_arrList_Extensions, string p_strCheck)
        {
            if (p_arrList_Extensions.Contains(p_strCheck))
                return true;
            return false;
        }

        /// <summary>
        /// Đọc file excel với index sheet, từ ô -> ô
        /// VD: A2 -> B2
        /// </summary>
        /// <param name="p_intSheet_Index"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strCell_End"></param>
        /// <returns></returns>
        public DataTable List_Range_Value(int p_intSheet_Index, string p_strCell_From, string p_strCell_End)
        {
            DataTable v_dt = new DataTable();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ExcelWorksheet v_excelWorksheet = m_objExcel_Package.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
            int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
            int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
            int v_intRowEnd = v_excelWorksheet.Cells[p_strCell_End].End.Row;
            int v_intColumnEnd = v_excelWorksheet.Cells[p_strCell_End].End.Column;
            foreach (var cell in v_excelWorksheet.Cells[v_intRowStart, v_intColumnStart, v_intRowStart, v_intColumnEnd])
            {
                string v_strColumnName = cell.Text.Trim();
                v_dt.Columns.Add(v_strColumnName);
            }
            for (int i = v_intRowStart; i <= v_intRowEnd; i++)
            {
                var row = v_excelWorksheet.Cells[i, v_intColumnStart, i, v_intColumnEnd];
                DataRow v_Row = v_dt.NewRow();
                int v_intColumn = 0;
                for (int j = v_intColumnStart; j <= v_intColumnEnd; j++)
                {
                    v_Row[v_intColumn] = row[i, j].Value;
                    v_intColumn++;
                }
                v_dt.Rows.Add(v_Row);
            }

            return v_dt;
        }

        /// <summary>
        /// Đọc file excel với mặt đinh sheet 1 từ ô -> ô
        /// VD: A2 -> B
        /// </summary>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strCell_End"></param>
        /// <returns></returns>
        public DataTable List_Range_Value(string p_strCell_From, string p_strCell_End)
        {
            return List_Range_Value(1, p_strCell_From, p_strCell_End);
        }

        /// <summary>
        /// Đọc file excel từ ô -> hết cột
        /// VD: A2 -> B
        /// </summary>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strCell_End"></param>
        /// <returns></returns>
        public DataTable List_Range_Value_To_End(string p_strCell_From, string p_strCell_End)
        {
            //3.5 thi STT start sheet = 1,  > 3.5 thi = 0
            return List_Range_Value_To_End(1, p_strCell_From, p_strCell_End);
        }

        /// <summary>
        /// Đọc file excel có sheet name từ ô -> hết cột
        /// VD: A2 -> B
        /// </summary>
        /// <param name="p_strSheet_Name"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strCell_End"></param>
        /// <returns></returns>
        public DataTable List_Range_Value_To_End(string p_strSheet_Name, string p_strCell_From, string p_strCell_End)
        {
            DataTable v_dt = new DataTable();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ExcelWorksheet v_excelWorksheet = m_objExcel_Package.Workbook.Worksheets[p_strSheet_Name];
            int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
            int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
            int v_intRowEnd = v_excelWorksheet.Dimension.End.Row;
            p_strCell_End = p_strCell_End + v_intRowEnd.ToString();
            int v_intColumnEnd = v_excelWorksheet.Cells[p_strCell_End].End.Column;

            //doc tiêu đề làm Col name
            for (int v_i = v_intColumnStart; v_i <= v_intColumnEnd; v_i++)
                v_dt.Columns.Add(v_i.ToString());

            for (int i = v_intRowStart; i <= v_intRowEnd; i++)
            {
                var row = v_excelWorksheet.Cells[i, v_intColumnStart, i, v_intColumnEnd];
                DataRow v_Row = v_dt.NewRow();
                int v_intColumn = 0;
                for (int j = v_intColumnStart; j <= v_intColumnEnd; j++)
                {
                    v_Row[v_intColumn] = row[i, j].Value;
                    v_intColumn++;
                }
                v_dt.Rows.Add(v_Row);
            }

            return v_dt;
        }

        /// <summary>
        /// Đọc file excel có sheet index từ ô -> hết cột
        /// VD: A2 -> B
        /// </summary>
        /// <param name="p_intSheet_Index"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strCell_End"></param>
        /// <returns></returns>
        public DataTable List_Range_Value_To_End(int p_intSheet_Index, string p_strCell_From, string p_strCell_End)
        {
            DataTable v_dt = new DataTable();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelWorksheet v_excelWorksheet = m_objExcel_Package.Workbook.Worksheets[p_intSheet_Index];
            int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
            int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
            int v_intRowEnd = v_excelWorksheet.Dimension.End.Row;
            p_strCell_End = p_strCell_End + v_intRowEnd.ToString();
            int v_intColumnEnd = v_excelWorksheet.Cells[p_strCell_End].End.Column;

            //doc tiêu đề làm Col name
            for (int v_i = v_intColumnStart; v_i <= v_intColumnEnd; v_i++)
                v_dt.Columns.Add(v_i.ToString());

            for (int i = v_intRowStart; i <= v_intRowEnd; i++)
            {
                var row = v_excelWorksheet.Cells[i, v_intColumnStart, i, v_intColumnEnd];
                DataRow v_Row = v_dt.NewRow();
                int v_intColumn = 0;
                for (int j = v_intColumnStart; j <= v_intColumnEnd; j++)
                {
                    v_Row[v_intColumn] = row[i, j].Value;
                    v_intColumn++;
                }
                v_dt.Rows.Add(v_Row);
            }

            return v_dt;
        }

        /// <summary>
        /// Export excel
        /// </summary>
        /// <param name="p_strPath_File"></param>
        /// <param name="p_intSheet_Index"></param>
        /// <param name="p_iRow_Start"></param>
        /// <param name="p_arrData"></param>
        /// <returns></returns>
        //public static byte[] Export_Excel_Template(string p_strPath_File, int p_intSheet_Index, int p_iRow_Start, List<object[]> p_arrData)
        //{
        //    byte[] v_arrContents;
        //    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        //    using (ExcelPackage v_objPackage = new(p_strPath_File))
        //    {
        //        var v_objWorksheet = v_objPackage.Workbook.Worksheets[p_intSheet_Index];
        //        int v_intRowStart = p_iRow_Start;
        //        //int v_intColumnStart = 1;
        //        object[] v_objData = p_arrData[0];
        //        int v_intColummEnd = v_objData.Length;

        //        foreach (object[] item in p_arrData)
        //        {
        //            v_intRowStart++;
        //            for (int v_i = 0; v_i < v_intColummEnd; v_i++)
        //            {
        //                v_objWorksheet.Cells[v_intRowStart, v_i + 1].Value = item[v_i];
        //            }
        //        }

        //        v_arrContents = v_objPackage.GetAsByteArray();
        //        v_objPackage.Dispose();
        //    }

        //    return v_arrContents;
        //}

        /// <summary>
        /// Lấy giá trị 1 ô
        /// </summary>
        /// <param name="p_strCell"></param>
        /// <returns></returns>
        public string Get_Cell_Value(string p_strCell)
        {
            return Get_Cell_Value(1, p_strCell);
        }

        /// <summary>
        /// Lấy giá trị của 1 ô từ 1 sheet
        /// </summary>
        /// <param name="p_intSheet_Index"></param>
        /// <param name="p_strCell"></param>
        /// <returns></returns>
        public string Get_Cell_Value(int p_intSheet_Index, string p_strCell)
        {
            string v_strCell_Value;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ExcelWorksheet v_excelWorksheet = m_objExcel_Package.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
            v_strCell_Value = CUtility.Convert_To_String(v_excelWorksheet.Cells[p_strCell].Value);

            return v_strCell_Value;
        }

        /// <summary>
        /// Lấy giá trị 1 ô từ 1 sheet
        /// </summary>
        /// <param name="p_strSheet_Name"></param>
        /// <param name="p_strCell"></param>
        /// <returns></returns>
        public string Get_Cell_Value(string p_strSheet_Name, string p_strCell)
        {
            string v_strCell_Value;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            ExcelWorksheet v_excelWorksheet = m_objExcel_Package.Workbook.Worksheets[p_strSheet_Name];
            v_strCell_Value = CUtility.Convert_To_String(v_excelWorksheet.Cells[p_strCell].Value);

            return v_strCell_Value;
        }

    }
}