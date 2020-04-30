using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DoNetDrive.Protocol.POS.Data;
using DoNetDrive.Protocol.POS.TemplateMethod;

namespace DoNetDrive.Protocol.POS.Reservation.AddReservationDetail
{
    public class AddReservationDetail_Parameter : TemplateParameter_Base<ReservationDetail>
    {
        public AddReservationDetail_Parameter()
        {

        }

        public AddReservationDetail_Parameter(List<ReservationDetail> list) : base(list)
        {
        }

        protected override bool CheckedParameterItem(ReservationDetail Menu)
        {
            
            return true;
        }
    }
}
