using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DoNetDrive.Protocol.POS.Data;

namespace DoNetDrive.Protocol.POS.Reservation.AddReservationDetail
{
    public class AddReservationDetail_Parameter : AbstractParameter
    {
        /// <summary>
        /// 需要写入的订餐列表
        /// </summary>
        public List<ReservationDetail> ReservationDetailList;

        public AddReservationDetail_Parameter(List<ReservationDetail> reservationDetailList)
        {
            ReservationDetailList = reservationDetailList;
        }

        public override bool checkedParameter()
        {
            if (ReservationDetailList == null || ReservationDetailList.Count == 0)
            {
                return false;
            }
            int iOut = 0;
            foreach (var item in ReservationDetailList)
            {
                if (item.CardData == 0)
                {
                    throw new ArgumentException("Data Error!");
                }
                if (item.ReservationDate.Year < 2000 || item.ReservationDate.Year > 2099)
                {
                    throw new ArgumentException("ReservationDate Error!");
                }
                //item.TimeGroup
            }
            return true;
        }

        public override void Dispose()
        {
            ReservationDetailList = null;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }

        public override int GetDataLen()
        {
            return 0;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }
    }
}
