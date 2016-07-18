using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Models
{
    public class Pharmacy
    {
        #region Properties

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public int ReservTime { get; private set; }

        public string PhoneNumber { get; private set; }

        public string WorkTime { get; private set; }

        #endregion Properties

        public Pharmacy(int id, string name, string address,
            int reservTime, string phoneNumber, string worktime)
        {
            Id = id;
            Name = name;
            Address = address;
            ReservTime = reservTime;
            PhoneNumber = phoneNumber;
            WorkTime = worktime;
        }
    }
}
