using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
namespace FCARDIO.Protocol.Door.Test
{
    public interface INMain
    {
        
        void AddLog(string s);
        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="s">需要显示的日志</param>
        void AddLog(StringBuilder s);

        /// <summary>
        /// 获取一个命令详情，已经装配好通讯目标的所有信息
        /// </summary>
        /// <returns>命令详情</returns>
        INCommandDetail GetCommandDetail();


        /// <summary>
        /// 将命令加入到分配器开始执行
        /// </summary>
        /// <param name="cmd"></param>
        void AddCommand(INCommand cmd);
    }
}
