using CSM_Data_Access.Controllers.Sys;
using CSM_Data_Access.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM_Data_Access.Cache.Sys
{
    public class CCache_Sys_Thanh_Vien
    {
        //Bắt buộc phải có list.
        private static List<CSys_Thanh_Vien> m_arrThanh_Vien = new();

        //Có thể tạo các biến thuận tiện cho việc truy xuất
        //VD: Tạo dic theo id và mã đăng nhập

        private static Dictionary<long, CSys_Thanh_Vien> m_dicID = new();

        private static Dictionary<string, CSys_Thanh_Vien> m_dicCode = new();


        /// <summary>
        /// Hàm này gọi trong Cache_Timer_Service
        /// </summary>
        public static void Load_Thanh_Vien()
        {
            CSys_Thanh_Vien_Controller v_objCtrlData = new();
            List<CSys_Thanh_Vien> v_arrTemp = v_objCtrlData.FSys_001_Cache_Thanh_Vien();

            foreach (CSys_Thanh_Vien v_objItem in v_arrTemp)
                Add_Data(v_objItem);
        }

        /// <summary>
        /// Hàm này dùng để add 1 obj vào cache
        /// </summary>
        /// <param name="p_objData"></param>
        public static void Add_Data(CSys_Thanh_Vien p_objData)
        {
            //Kiểm tra nếu dữ liệu thêm vào nó null hoặc Auto_ID nó = 0 thì không thêm vào
            if (p_objData == null || p_objData.Auto_ID == CConst.INT_VALUE_NULL)
                return;

            //Kiểm tra trong dic auto id: nếu tồn tại rồi thì không thêm vào
            if (m_dicID.ContainsKey(p_objData.Auto_ID) == true)
                return;
            m_dicID.Add(p_objData.Auto_ID, p_objData);

            //Kiểm tra trong dic mã đăng nhập: nếu tồn tại rồi thì không thêm vào

            //Lưu ý vì để nó quy về 1 chuẩn cho toàn bộ type web thì sử dụng hàm tạo key trong CUtility đối với dic có key là kiểu string
            //Nguyên tắt key là duy nhất và không được trùng cd như auto_id, mã đăng nhập, hoặc key là cả 2

            //string v_strKeyCode = CUtility.Tao_Key(p_objData.Auto_ID);
            //string v_strKeyCode = CUtility.Tao_Key(p_objData.Auto_ID, p_objData.Ma_Dang_Nhap);

            string v_strKeyCode = CUtility.Tao_Key(p_objData.Ma_Dang_Nhap);

            if (m_dicCode.ContainsKey(v_strKeyCode) == true)
                return;
            m_dicCode.Add(v_strKeyCode, p_objData);

            m_arrThanh_Vien.Add(p_objData); //Add vào list
        }

        /// <summary>
        /// Hàm này dùng để sửa 1 obj
        /// </summary>
        /// <param name="p_objData"></param>
        public static void Update_Data(CSys_Thanh_Vien p_objData)
        {
            //Kiểm tra nếu dữ liệu thêm vào nó null hoặc Auto_ID nó = 0 thì không thêm vào
            if (p_objData == null || p_objData.Auto_ID == CConst.INT_VALUE_NULL)
                return;

            //Xóa đi, add lại thôi cho đỡ quằng
            Delete_Data_By_ID(p_objData.Auto_ID);

            Add_Data(p_objData);
        }

        /// <summary>
        /// Hàm này dùng để xóa obj theo Auto_ID
        /// </summary>
        /// <param name="p_lngAuto_ID"></param>
        public static void Delete_Data_By_ID(long p_lngAuto_ID)
        {
            //Kiểm tra nếu id rỗng thì thoát hàm đỡ tốn bộ nhớ
            if (p_lngAuto_ID == CConst.INT_VALUE_NULL)
                return;

            //Lấy obj đó ra
            CSys_Thanh_Vien v_objDelete = m_arrThanh_Vien.FirstOrDefault(it => it.Auto_ID == p_lngAuto_ID);

            //Check null = cook
            if (v_objDelete == null)
                return;

            //Check trong các biến khác
            string v_strKey_Code = CUtility.Tao_Key(v_objDelete.Ma_Dang_Nhap); //Nhớ tạo key
            if (m_dicCode.ContainsKey(v_strKey_Code) == false)
                return;
            m_dicCode.Remove(v_strKey_Code);

            if (m_dicID.ContainsKey(v_objDelete.Auto_ID) == false)
                return;
            m_dicID.Remove(v_objDelete.Auto_ID);

            //Tiến hành xóa obj khỏi list
            m_arrThanh_Vien.Remove(v_objDelete);
        }

        /// <summary>
        /// Hàm này dùng để xóa obj theo mã đăng nhập
        /// </summary>
        /// <param name="p_strMa_Dang_Nhap"></param>
        public static void Delete_Data_By_Ma_Dang_Nhap(string p_strMa_Dang_Nhap)
        {
            //Kiểm tra nếu id rỗng thì thoát hàm đỡ tốn bộ nhớ
            if (p_strMa_Dang_Nhap == CConst.STR_VALUE_NULL)
                return;

            //Lấy obj đó ra
            CSys_Thanh_Vien v_objDelete = m_arrThanh_Vien.FirstOrDefault(it => it.Ma_Dang_Nhap == p_strMa_Dang_Nhap);

            //Check null = cook
            if (v_objDelete == null)
                return;

            //Check trong các biến khác
            string v_strKey_Code = CUtility.Tao_Key(v_objDelete.Ma_Dang_Nhap); //Nhớ tạo key
            if (m_dicCode.ContainsKey(v_strKey_Code) == false)
                return;
            m_dicCode.Remove(v_strKey_Code);

            if (m_dicID.ContainsKey(v_objDelete.Auto_ID) == false)
                return;
            m_dicID.Remove(v_objDelete.Auto_ID);

            //Tiến hành xóa obj khỏi list
            m_arrThanh_Vien.Remove(v_objDelete);
        }

        public List<CSys_Thanh_Vien> List_Data()
        {
            return m_arrThanh_Vien;
        }

        public CSys_Thanh_Vien Get_Data_By_ID(long p_lngAuto_ID)
        {
            CSys_Thanh_Vien v_objRes = null;
            if (m_dicID.ContainsKey(p_lngAuto_ID) == true)
                v_objRes = m_dicID[p_lngAuto_ID];

            return v_objRes;
        }

        public CSys_Thanh_Vien Get_Data_By_Ma_Ddang_Nhap(string p_strMa_Dang_Nhap)
        {
            CSys_Thanh_Vien v_objRes = null;
            if (m_dicCode.ContainsKey(p_strMa_Dang_Nhap) == true)
                v_objRes = m_dicCode[p_strMa_Dang_Nhap];

            return v_objRes;
        }

    }
}