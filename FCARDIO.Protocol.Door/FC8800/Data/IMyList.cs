using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    public interface IMyList<in T>
    {
        void ChangeT(T t);
    }
}
