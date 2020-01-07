using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Reservation.ReadDataBase
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
