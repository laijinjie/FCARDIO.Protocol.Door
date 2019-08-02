using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Protocol.Fingerprint.Data;

namespace FCARDIO.Protocol.Fingerprint.Person.AddPerson
{
    public class AddPerson_Parameter : WritePerson_ParameterBase
    {
        public AddPerson_Parameter(List<Data.Person> personList) : base(personList)
        {

        }

        /// <summary>
        /// 检查每个人员
        /// </summary>
        /// <param name="PersonList"></param>
        /// <returns></returns>
        protected override bool CheckedParameterItem(List<Data.Person> PersonList)
        {
            foreach (var p in PersonList)
            {
                if (p == null) return false;
                if (p.UserCode == 0) return false;
                if (p.CardData == 0) return false;
                if (p.TimeGroup > 64 || p.TimeGroup < 0) return false;
                if (p.EnterStatus > 3 || p.EnterStatus < 0) return false;
                if (p.Expiry.Year > 2099 || p.Expiry.Year < 2000) return false;
                if (p.FingerprintFeatureCodeList == null || p.FingerprintFeatureCodeList.Length > 10) return false;
                foreach (var item in p.FingerprintFeatureCodeList)
                {
                    if (item > 1) return false;
                }
                if (p.Holiday == null || p.Holiday.Length > 4) return false;
                if (p.Identity > 1 || p.Identity < 0) return false;
                if (string.IsNullOrEmpty(p.PName)) return false;
                if (string.IsNullOrEmpty(p.PCode)) return false;
                //if (string.IsNullOrEmpty(p.))
                if (p.CardType > 1 || p.CardType < 0) return false;
                if (p.CardStatus > 1 || p.CardStatus < 0) return false;
            }

            return true;
        }
    }
}
