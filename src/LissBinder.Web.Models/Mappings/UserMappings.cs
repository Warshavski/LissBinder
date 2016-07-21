
namespace Escyug.LissBinder.Web.Models.Mappings
{
    internal static class UserMappings
    {
        public static Models.User EntityToModel(Data.Entities.User entity)
        {
            var userId = entity.Id.ToString();
            var userName = entity.Name;
            var userLogin = entity.Login;
            var passwordHash = entity.PwdHash;
            var salt = entity.Salt;

            return new Models.User(userId, userName, userLogin, passwordHash, salt);
        }

        public static Data.Entities.User ModelToEntity(Models.User model)
        {
            var userEntity = new Data.Entities.User();
            userEntity.Id = int.Parse(model.Id);
            userEntity.Name = model.UserName;
            userEntity.Login = model.Login;
            userEntity.PwdHash = model.PwdHash;
            userEntity.Salt = model.Salt;

            return userEntity;
        }
    }
}
