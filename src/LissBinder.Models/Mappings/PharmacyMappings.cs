
namespace Escyug.LissBinder.Models.Mappings
{
    internal static class PharmacyMappings
    {
        public static Models.Pharmacy EntityToModel(Data.Entities.Pharmacy entity)
        {
            var pharmacyId = entity.Id;
            var pharmacyName = entity.Name;
            var pharmacyAddress = entity.Address;
            var drugReservTime = entity.ReservTime;
            var pharmacyPhoneNumber = entity.PhoneNumber;
            var pharmacyWorktime = entity.WorkTime;

            return new Models.Pharmacy(pharmacyId, pharmacyName, pharmacyAddress,
                drugReservTime, pharmacyPhoneNumber, pharmacyWorktime);
        }
    }
}
