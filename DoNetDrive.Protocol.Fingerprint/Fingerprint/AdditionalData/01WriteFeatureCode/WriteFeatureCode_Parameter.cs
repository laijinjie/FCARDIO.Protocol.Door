using DotNetty.Buffers;
using DoNetDrive.Protocol.Door.Door8800;

namespace DoNetDrive.Protocol.Fingerprint.AdditionalData
{
    /// <summary>
    /// 准备写文件
    /// </summary>
    public class WriteFeatureCode_Parameter : AbstractParameter
    {
        /// <summary>
        /// 用户号
        /// </summary>
        public int UserCode;

        /// <summary>
        /// 文件类型
        /// 1 - 人员头像照片
        /// 2 - 指纹
        /// 3 - 人脸特征码
        /// 4 - 动态人脸特征码
        /// </summary>
        public int Type;

        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber;



        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Datas;

        /// <summary>
        /// 等待校验的时间，单位毫秒
        /// </summary>
        public int WaitVerifyTime;


        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="userCode">用户号</param>
        /// <param name="type">文件类型</param>
        /// <param name="serialNumber">序号</param>
        public WriteFeatureCode_Parameter(int userCode, int type, int serialNumber, byte[] datas)
        {
            UserCode = userCode;
            Type = type;
            SerialNumber = serialNumber;
            Datas = datas;
            WaitVerifyTime = 6000;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Type < 1 || Type > 3)
            {
                return false;
            }
            if (SerialNumber < 0)
            {
                return false;
            }
            if (UserCode < 0)
            {
                return false;
            }
            if (Datas == null || Datas.Length == 0 || Datas.Length > 150 * 1024)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 6;
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteInt(UserCode);
            databuf.WriteByte(Type);
            databuf.WriteByte(SerialNumber);
            return databuf;
        }


        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {

        }
    }
}
