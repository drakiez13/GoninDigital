namespace GoninDigital.Validator.Interfaces
{
    interface IPersonalAccountValidator<T>
    {
        T ValidatePassword(string password);

        T ValidateEmail(string emailstring, string emailSubject, string emailContent);
    }
}
