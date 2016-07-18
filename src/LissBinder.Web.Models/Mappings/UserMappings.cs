
namespace Escyug.LissBinder.Web.Models.Mappings
{
    internal static class UserMappings
    {
        public static Models.User EntityToModel(Data.Entities.User entity)
        {
            var userId = entity.Id;
            var userName = entity.Name;

            return new Models.User(userId, userName);
        }
    }
}
