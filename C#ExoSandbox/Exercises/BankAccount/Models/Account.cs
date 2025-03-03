using BankAccount.Interfaces;

class Account
{

    public string owner { get; set; }
    public string Bban { get; set; }
    public string Iban { get; set; }

    private readonly IConvertAccount _converter;
    private readonly IValidateAccount _validator;
    public Account(string bban, string owner, IConvertAccount converter, IValidateAccount validator)
    {
        this._converter = converter ?? throw new ArgumentNullException(nameof(converter));
        this._validator = validator ?? throw new ArgumentNullException(nameof(validator));


        if (!this._validator.ValidateBban(bban))
        {
            throw new ArgumentException($"Account \"{bban}\" is invalid.");
        }
        else
        {
            Console.WriteLine("Account is valid.");
        }
        this.Bban = bban;
        this.Iban = this._converter.ConvertToIban(bban);
        this.owner = owner;
    }



}