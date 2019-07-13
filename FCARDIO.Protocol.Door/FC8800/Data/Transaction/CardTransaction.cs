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
    /// 刷卡记录<br/>
    /// TransactionCode 事件代码含义表：<br/>
    /// 1 &emsp; 合法开门                                                    
    /// 2 &emsp; 密码开门------------卡号为密码                              
    /// 3 &emsp; 卡加密码                                                    
    /// 4 &emsp; 手动输入卡加密码                                            
    /// 5 &emsp; 首卡开门                                                    
    /// 6 &emsp; 门常开 --- 常开工作方式中，刷卡进入常开状态                 
    /// 7 &emsp; 多卡开门 -- 多卡验证组合完毕后触发                          
    /// 8 &emsp; 重复读卡                                                    
    /// 9 &emsp; 有效期过期                                                  
    /// 10 &emsp; 开门时段过期                                               
    /// 11 &emsp; 节假日无效                                                 
    /// 12 &emsp; 未注册卡                                                   
    /// 13 &emsp; 巡更卡 -- 不开门                                           
    /// 14 &emsp; 探测锁定                                                   
    /// 15 &emsp; 无有效次数                                                 
    /// 16 &emsp; 防潜回                                                     
    /// 17 &emsp; 密码错误------------卡号为错误密码                         
    /// 18 &emsp; 密码加卡模式密码错误----卡号为卡号。                       
    /// 19 &emsp; 锁定时(读卡)或(读卡加密码)开门                             
    /// 20 &emsp; 锁定时(密码开门)                                           
    /// 21 &emsp; 首卡未开门                                                 
    /// 22 &emsp; 挂失卡                                                     
    /// 23 &emsp; 黑名单卡                                                   
    /// 24 &emsp; 门内上限已满，禁止入门。                                   
    /// 25 &emsp; 开启防盗布防状态(设置卡)                                   
    /// 26 &emsp; 撤销防盗布防状态(设置卡)                                   
    /// 27 &emsp; 开启防盗布防状态(密码)                                     
    /// 28 &emsp; 撤销防盗布防状态(密码)                                     
    /// 29 &emsp; 互锁时(读卡)或(读卡加密码)开门                             
    /// 30 &emsp; 互锁时(密码开门)                                           
    /// 31 &emsp; 全卡开门                                                   
    /// 32 &emsp; 多卡开门--等待下张卡                                       
    /// 33 &emsp; 多卡开门--组合错误                                         
    /// 34 &emsp; 非首卡时段刷卡无效                                         
    /// 35 &emsp; 非首卡时段密码无效                                         
    /// 36 &emsp; 禁止刷卡开门 -- 【开门认证方式】验证模式中禁用了刷卡开门时 
    /// 37 &emsp; 禁止密码开门 -- 【开门认证方式】验证模式中禁用了密码开门时 
    /// 38 &emsp; 门内已刷卡，等待门外刷卡。（门内外刷卡验证）               
    /// 39 &emsp; 门外已刷卡，等待门内刷卡。（门内外刷卡验证）               
    /// 40 &emsp; 请刷管理卡(在开启管理卡功能后提示)(电梯板)                 
    /// 41 &emsp; 请刷普通卡(在开启管理卡功能后提示)(电梯板)                 
    /// 42 &emsp; 首卡未读卡时禁止密码开门。                                 
    /// 43 &emsp; 控制器已过期_刷卡                                          
    /// 44 &emsp; 控制器已过期_密码                                          
    /// 45 &emsp; 合法卡开门—有效期即将过期                                 
    /// 46 &emsp; 拒绝开门--区域反潜回失去主机连接。                         
    /// 47 &emsp; 拒绝开门--区域互锁，失去主机连接                           
    /// 48 &emsp; 区域防潜回--拒绝开门    
    /// 49 &emsp; 区域互锁--有门未关好，拒绝开门
    /// </summary>
    public class CardTransaction : AbstractTransaction
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        public CardTransaction()
        {
            TransactionType = 1;
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public long CardData;

        /// <summary>
        /// 读卡器号
        /// </summary>
        public short Reader;

        /// <summary>
        /// 获取读卡记录格式长度
        /// </summary>
        /// <returns></returns>
        public virtual int GetDataLen()
        {
            return 13;
        }

        /// <summary>
        /// 从buf中读取记录数据
        /// </summary>
        /// <param name="data"></param>
        public override void SetBytes(IByteBuffer data)
        {
            try
            {
                if (data.ReadByte() == 255)
                {
                    IsNull = true;
                    //return;
                }
                if (data.Capacity % 13 == 0)
                {
                    data.ReadByte();

                    CardData = data.ReadInt();
                }
                else
                {
                    data.ReadByte();

                    CardData = data.ReadLong();
                }

                byte[] btTime = new byte[6];
                data.ReadBytes(btTime, 0, 6);
                TransactionDate = TimeUtil.BCDTimeToDate_yyMMddhh(btTime);
                Reader = data.ReadByte();
                TransactionCode = data.ReadByte();
                if (TransactionCode == 0 || Reader == 0 || Reader > 8 || TransactionDate == null)
                {
                    IsNull = true;
                }
            }
            catch (Exception e)
            {
            }

            return;
        }

        /// <summary>
        /// 从buf中读取卡号
        /// </summary>
        /// <param name="data"></param>
        public virtual void ReadCardData(IByteBuffer data)
        {
            data.ReadByte();

            CardData = data.ReadInt();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IByteBuffer GetBytes()
        {
            return null;
        }

        /// <summary>
        /// 获取门号
        /// </summary>
        /// <returns>1-4 代表4个门</returns>
        public short DoorNum()
        {
            switch (Reader)
            {
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                    return 2;
                case 5:
                case 6:
                    return 3;
                case 7:
                case 8:
                    return 4;
                default:
                    return 0;

            }

        }

        /// <summary>
        /// 是否为进门读卡
        /// </summary>
        /// <returns>true 进门读卡，false 出门读卡</returns>
        public bool IsEnter()
        {
            if (Reader == 0 || Reader > 8)
            {
                return false;
            }
            return Reader % 2 == 1;
        }

        /// <summary>
        /// 是否为出门读卡
        /// </summary>
        /// <returns>true 出门读卡，false 进门读卡</returns>
        public bool IsExit()
        {
            return !IsEnter();
        }
    }
}