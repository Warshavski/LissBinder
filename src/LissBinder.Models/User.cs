
namespace Escyug.LissBinder.Models
{
    public class User
    {
        public int UserId { get; private set; }

        public string UserName { get; private set; }

        public int PharmacyId { get; private set; }
        
        public User(int userId, string userName, int pharmacyId)
        {
            UserId = userId;
            UserName = userName;
            PharmacyId = pharmacyId;
        }
    }
}
