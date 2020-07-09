﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Person
{
    /// <summary>
    /// 人员识别信息
    /// </summary>
    public class IdentificationData
    {
        /// <summary>
        /// 数据类型,取值范围：1-4
        /// 1、人员照片
        /// 2、指纹特征码
        /// 3、红外人脸特征码
        /// 4、动态人脸特征码
        /// </summary>
        public int DataType;

        /// <summary>
        /// 数据序号， 数据类型为2时，取值范围：0-9
        /// </summary>
        public int DataNum;

        /// <summary>
        /// 数据缓冲区
        /// </summary>
        public byte[] DataBuf;

        /// <summary>
        /// 缓冲区偏移量
        /// </summary>
        public int DataBufOffset = 0;
        /// <summary>
        /// 缓冲区长度
        /// </summary>
        public int DataBufLen;

        /// <summary>
        /// 创建人员识别信息
        /// </summary>
        /// <param name="iType">数据类型,取值范围：1-4</param>
        /// <param name="bData">数据缓冲区</param>
        public IdentificationData(int iType,byte[] bData) : this(iType, 1, bData, 0, bData.Length)
        {

        }

        /// <summary>
        /// 创建人员识别信息
        /// </summary>
        /// <param name="iType">数据类型,取值范围：1-4</param>
        /// <param name="iNum">数据序号， 数据类型为2时，取值范围：0-9</param>
        /// <param name="bData">数据缓冲区</param>
        public IdentificationData(int iType, int iNum, byte[] bData):this(iType, iNum, bData,0, bData.Length)
        {
        }
        /// <summary>
        /// 创建人员识别信息
        /// </summary>
        /// <param name="iType">数据类型,取值范围：1-4</param>
        /// <param name="iNum">数据序号， 数据类型为2时，取值范围：0-9</param>
        /// <param name="bData">数据缓冲区</param>
        /// <param name="ioffset">缓冲区偏移量</param>
        /// <param name="len">缓冲区长度</param>
        public IdentificationData(int iType,int iNum, byte[] bData,int ioffset,int len)
        {
            DataType = iType;
            DataNum = iNum;
            DataBuf = bData;
            DataBufOffset = ioffset;
            DataBufLen = len;
        }

    }
}
