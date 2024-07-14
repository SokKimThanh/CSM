using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSM_Data_Access.Utility
{
    public class CUtility_Number
    {
        public static bool Is_Valid_Number(object p_objData)
        {
            if (p_objData == null || CUtility.Convert_To_String(p_objData) == CConst.STR_VALUE_NULL)
                return false;

            char[] v_arrNums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < CUtility.Convert_To_String(p_objData).Length; i++)
                //Nếu không thỏa điều kiện kia thì cook
                if (v_arrNums.Contains(v_arrNums[i]) == false)
                    return false;

            return true;
        }
    }
}
