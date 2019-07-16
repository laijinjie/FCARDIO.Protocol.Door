using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 系统记录
    /// TransactionCode 事件代码含义表：
    /// 1   系统加电                       
    /// 2   系统错误复位（看门狗）         
    /// 3   设备格式化记录                 
    /// 4   系统高温记录，温度大于75      
    /// 5   系统UPS供电记录                
    /// 6   温度传感器损坏，温度大于100   
    /// 7   电压过低，小于09V             
    /// 8   电压过高，大于14V             
    /// 9   读卡器接反。                   
    /// 10  读卡器线路未接好。            
    /// 11  无法识别的读卡器              
    /// 12  电压恢复正常，小于14V，大于9V 
    /// 13  网线已断开                    
    /// 14  网线已插入   
    /// </summary>
    public class SystemTransaction : AbstractTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemTransaction()
        {
            TransactionType = 6;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetDataLen()
        {
            return 8;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void SetBytes(IByteBuffer data)
        {
            try
            {
                short code = data.ReadByte();
                if (code == 255)
                {
                    IsNull = true;
                    //return;
                }
                TransactionCode = code;
                byte[] btTime = new byte[6];
                data.ReadBytes(btTime, 0, 6);
                if (btTime[0] == 255)
                {
                    IsNull = true;
                    //return;
                }
                TransactionDate = TimeUtil.BCDTimeToDate_yyMMddhh(btTime);
                data.ReadByte();//占位

                if (TransactionCode == 0)
                {
                    IsNull = true;
                }
            }
            catch (Exception e)
            {
            }

            return;
        }
    }
}
