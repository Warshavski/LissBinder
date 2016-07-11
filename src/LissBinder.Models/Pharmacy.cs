using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models
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

        public Pharmacy(int pharmacyId, string pharmacyName, string pharmacyAddress,
            int drugReservTime, string pharmacyPhoneNumber, string pharmacyWorktime)
        {
            Id = pharmacyId;
            Name = pharmacyName;
            Address = pharmacyAddress;
            ReservTime = drugReservTime;
            PhoneNumber = pharmacyPhoneNumber;
            WorkTime = pharmacyWorktime;
        }
    }
}
