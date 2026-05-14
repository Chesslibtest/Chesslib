namespace ChessLib.Domain.Entities
{
    public class Profile 
    {
        public Guid Id {get; init;}
        public Guid UserId {get; private set;}
        
// 
// Ссылка на пользователя к которому принадлежит профиль!
// 
        public User User {get; private set;} = null!;

        public Profile (){}
        public Profile(Guid id , Guid UserId)
        {
            Id = id;
            this.UserId = UserId;
        }

    }
}