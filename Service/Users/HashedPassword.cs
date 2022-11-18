namespace Service.Users;

internal class HashedPassword
{
    public string HashedPasswordText { get; set; }

    public string Salt { get; set; }
}