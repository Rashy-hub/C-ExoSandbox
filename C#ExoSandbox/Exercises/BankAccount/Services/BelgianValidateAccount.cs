using BankAccount.Interfaces;


class BelgianValidateAccount : IValidateAccount
{

    /*Verification of a BBAN bank account:
   The modulo of the first 10 digits by 97 gives the last 2 digits. However, if the modulo result is 0, the last 2 digits should be 97.*/
    public bool ValidateBban(string bban)
    {

        string cleanBban = bban.Replace("-", "");

        if (!long.TryParse(cleanBban, out long parsedBban) || cleanBban.Length != 12)
        {
            throw new ArgumentException("Invalid BBAN. , must contain twelve numbers (separated with dash)");
        }
        string numbers = cleanBban.Substring(0, 10);
        string checksum = cleanBban.Substring(10, 2);


        int modulo = int.Parse(numbers) % 97;
        if ((modulo == 0 && int.Parse(checksum) == 97) || modulo == int.Parse(checksum))
            return true;

        return false;
    }
}