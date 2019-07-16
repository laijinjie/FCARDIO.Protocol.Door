using FCARDIO.Protocol.Door.FC8800.Password;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
    /// <summary>
    /// 读取所有密码 结果
    /// </summary>
    public class ReadAllPassword_Result : ReadAllPassword_Result_Base<PasswordDetail>
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="passwordList"></param>
        public ReadAllPassword_Result(List<PasswordDetail> passwordList) : base(passwordList)
        {

        }
    }
}
