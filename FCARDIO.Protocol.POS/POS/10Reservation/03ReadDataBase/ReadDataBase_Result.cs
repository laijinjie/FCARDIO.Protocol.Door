using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.Reservation.ReadDataBase
{
    public class ReadDataBase_Result : INCommandResult
    {
        /// <summary>
        /// 读取到的卡片列表
        /// </summary>
        public List<ReservationDetail> ReservationDetailList;

        public ReadDataBase_Result(List<ReservationDetail> reservationDetailList)
        {
            ReservationDetailList = reservationDetailList;
        }
        public void Dispose()
        {
            
        }

        public void SetBytes(IByteBuffer buf)
        {
           

        }
    }
}
