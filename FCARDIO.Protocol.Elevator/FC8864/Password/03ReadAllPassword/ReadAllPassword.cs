using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Password;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 从控制器读取所有密码
    /// </summary>
    public class ReadAllPassword : ReadAllPassword_Base<PasswordDetail>
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadAllPassword(INCommandDetail cd) : base(cd)
        {

        }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList">控制器返回的密码集合</param>
        protected override ReadAllPassword_Result_Base<PasswordDetail> CreateResult(List<PasswordDetail> passwordList)
        {
            ReadAllPassword_Result result = new ReadAllPassword_Result(passwordList);
            return result;
        }
    }
}
