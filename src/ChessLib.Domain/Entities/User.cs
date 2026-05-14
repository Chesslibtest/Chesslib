using ChessLib.Domain.ValueObjects;

namespace ChessLib.Domain.Entities;

public class User
{
    public Guid Id{get; init;}
    public string Name {get; set;} = string.Empty;
    public Email Email {get; private set;} = null!;
    public string PasswordHash {get; private set;} = string.Empty;

    public Profile Profile {get; private set;} = null!;

    public User (){ }

// 
// Конструктор для создания нового пользователя, который автоматически создает профиль для этого пользователя.
// 
    public User (Guid id, string name, Email email, string passwordHash)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Profile = new Profile(Guid.NewGuid(), Id);
    }

}