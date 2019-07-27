﻿using DotNetty.Buffers;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Util;
using System;
using System.Text.RegularExpressions;

namespace FCARDIO.Protocol.USB.CardReader.ICCard.Sector
{
    /// <summary>
    /// 写扇区内容
    /// </summary>
    public class WriteSector_Parameter : AbstractParameter
    {
        /// <summary>
        /// 卡片类型
        /// 1 - MF1 IC卡 S50
        /// 2 - NFC标签卡
        /// 3 - NFC手机
        /// 4 - 身份证
        /// 5 - CPU IC卡 S50
        /// 6 - CPU卡
        /// 7 - MF1 IC卡 S70
        /// 8 - CPU IC卡 S70
        /// 9 - ID卡
        /// </summary>
        public byte Type;

        /// <summary>
        /// 扇区号
        /// S50卡的取值范围是0-15
        /// S70卡的取值范围是0-39
        /// </summary>
        public byte Number;

        /// <summary>
        /// 起始数据块
        /// S50卡每个扇区的块号都是0-3，其中块3是密码块
        /// S70卡0-31块扇区的块号是0-3，其中块3是密码块
        /// 32-39块扇区的块号是0-15，其中块15是密码块
        /// </summary>
        public byte StartBlock;


        /// <summary>
        /// 密钥验证类型
        /// 1--A密钥
        /// 2--B密钥
        /// </summary>
        public byte VerifyMode;

        /// <summary>
        /// 扇区密码
        /// </summary>
        public string Password;

        /// <summary>
        /// 待写入数据内容
        /// </summary>
        public string Content;
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="type">卡片类型</param>
        /// <param name="number">扇区号</param>
        /// <param name="startBlock">起始数据块</param>
        /// <param name="writeCount">写入块数</param>
        /// <param name="verifyMode">密钥验证类型</param>
        /// <param name="password">扇区密码</param>
        /// <param name="content">待写入数据内容</param>
        public WriteSector_Parameter(byte type, byte number, byte startBlock, byte verifyMode, string password,string content)
        {
            Type = type;
            Number = number;
            StartBlock = startBlock;
            VerifyMode = verifyMode;
            Password = password;
            Content = content;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Type == 1 || Type == 5)
            {
                if (Number > 15)
                {
                    throw new ArgumentException("Number Error!");
                }
                if (StartBlock > 3)
                {
                    throw new ArgumentException("StartBlock Error!");
                }
            }
            else if (Type == 7 || Type == 8)
            {
                if (Number > 39)
                {
                    throw new ArgumentException("Number Error!");
                }
                if (Number <= 31 && StartBlock > 3)
                {
                    throw new ArgumentException("StartBlock Error!");
                }
                if (StartBlock > 15)
                {
                    throw new ArgumentException("StartBlock Error!");
                }
            }
            else
            {
                throw new ArgumentException("Type Error!");
            }
            if (Password != null && Password.Length > 12)
            {
                
                throw new ArgumentException("Password Error!");
            }
            if (!Password.IsHex())
            {
                throw new ArgumentException("Password Error!");
            }

            if (VerifyMode != 1 && VerifyMode != 2)
            {
                throw new ArgumentException("VerifyMode Error!");
            }


            if (string.IsNullOrEmpty(Content) || Content.Length > 128)
            {
                throw new ArgumentException("Content Error!");
            }
            else
            {
                Content = Content.Replace("\r\n","");
                if (!Content.IsHex())
                {
                    throw new ArgumentException("Content Error!");
                }
            }
          
            return true;
        }



        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (Content.Length % 32 != 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (Content.Length < i * 32)
                    {
                        Content = StringUtil.FillHexString(Content, i * 32, "0", true);
                        break;
                    }
                }
            }
            int WriteCount = Content.Length / 32;
            databuf.WriteByte(Number);
            databuf.WriteByte(StartBlock);
            databuf.WriteByte(WriteCount);
            databuf.WriteByte(VerifyMode);

            Password = Password ?? "";
            Password = StringUtil.FillHexString(Password, 12, "F", true);
            StringUtil.HextoByteBuf(Password, databuf);

            //Content = StringUtil.FillHexString(Content, Content.Length * 2, "0", true);
            StringUtil.HextoByteBuf(Content, databuf);
            return databuf;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x0A + Content.Length;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {

        }
    }
}
