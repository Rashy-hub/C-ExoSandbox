using System;
using BankAccount.Interfaces;

/*
- Verification of a BBAN bank account:
  The modulo of the first 10 digits by 97 gives the last 2 digits. However, if the modulo result is 0, the last 2 digits should be 97.
    
- Convert a Belgian BBAN bank account (xxx-xxxxxxx-xx) into an IBAN (BExx-xxxx-xxxx-xxxx). Find the procedure using a search engine.

- The solution must respect the **SOLID** principles by applying a clear **separation of concerns**:
 
*/

namespace BankAccount
{
    class Program
    {
        static void Main(string[] args)

        {

            IConvertAccount converter = new BelgianConvertAccount();
            IValidateAccount validator = new BelgianValidateAccount();

            try
            {
                var account = new Account(bban: "091-0122401-16", owner: "Bob", converter, validator);
                Console.WriteLine($"{account.owner} IBAN account : {account.Iban}");
            }
            catch (System.Exception ex)
            {

                Console.WriteLine("error " + ex.Message);
            }

        }

    }
}
