
namespace Escyug.LissBinder.Data.Entities
{
    public class User
    {
        // represents id_user column
        public int Id {get; set;}

        // represents name column
        public string Name { get; set; }

        // represents login column
        public string NameDescription { get; set; }

        // represents hash column
        public string PasswordHash { get; set; }

        // represents salt column
        //public byte[] Salt { get; set; }

        // represents liss_pharmacys.id_pharmacy column
        public int PharmacyId { get; set; }
    }
}
