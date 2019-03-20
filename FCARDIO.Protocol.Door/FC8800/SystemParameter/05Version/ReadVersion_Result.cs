using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.Version
{
    /// <summary>
    /// 获取设备版本号_结果
    /// </summary>
    public class ReadVersion_Result : INCommandResult
    {
        public string Version;

        public ReadVersion_Result(string _Version)
        {
            Version = _Version;
        }

        public void Dispose()
        {
            return;
        }
    }
}