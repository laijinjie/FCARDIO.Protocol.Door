using DotNetty.Buffers;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.ManageCard
{
    /// <summary>
    /// 管理卡功能
    /// </summary>
    public class WriteManageCard_Parameter : AbstractParameter
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUse;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteManageCard_Parameter() { }

        /// <summary>
        /// 使用主板蜂鸣器初始化实例
        /// </summary>
        /// <param name="isUse">是否启用</param>
        public WriteManageCard_Parameter(bool isUse)
        {
            IsUse = isUse;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 对主板蜂鸣器参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteBoolean(IsUse);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x01;
        }

        /// <summary>
        /// 对主板蜂鸣器参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            IsUse = databuf.ReadBoolean();
        }
    }
}
