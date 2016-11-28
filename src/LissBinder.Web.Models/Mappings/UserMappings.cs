namespace Escyug.LissBinder.Web.Models.Mappings
{
    internal static class UserMappings
    {
        public static Models.User EntityToModel(Data.Entities.User userEntity)
        {
            var userId = userEntity.Id;
            var userName = userEntity.Name;
            var userNameDescription = userEntity.NameDescription;
            var userPasswordHash = userEntity.PasswordHash;
            var pharmacyId = userEntity.PharmacyId;

            return new Models.User(userId.ToString(), userName, 
                userNameDescription, pharmacyId, userPasswordHash);
        }

        //public static Data.Entities.User ModelToEntity(Models.User userModel, byte[] pwdHash, byte[] pwdSalt)
        //{
        //    var userEntity = ModelToEntity(userModel);
        //    userEntity.PwdHash = pwdHash;
        //    userEntity.Salt = pwdSalt;

        //    return userEntity;
        //}

        public static Data.Entities.User ModelToEntity(Models.User userModel)
        {
            var userEntity = new Data.Entities.User();

            userEntity.Id = (userModel.Id == null ? 0 : int.Parse(userModel.Id));
            userEntity.Name = userModel.UserName;
            userEntity.NameDescription = userModel.NameDescription;
            userEntity.PharmacyId = userModel.PharmacyId;
            userEntity.PasswordHash = userModel.PasswordHash;

            return userEntity;
        }
    }
}
