
namespace Escyug.LissBinder.Models
{
    public class User
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public User(int userId, string userName)
        {
            Id = userId;
            Name = userName;
        }
    }
}
