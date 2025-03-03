namespace BankAccount.Interfaces
{
    public interface IValidateAccount
    {
        bool ValidateBban(string account);
    }
}