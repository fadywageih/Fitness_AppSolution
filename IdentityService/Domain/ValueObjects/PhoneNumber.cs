using System.Text.RegularExpressions;

namespace IdentityService.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; }

        // Egyptian phone format: 01xxxxxxxxx
        private static readonly Regex PhoneRegex = new(
            @"^(01)[0-9]{9}$",
            RegexOptions.Compiled);

        private PhoneNumber(string value) => Value = value;

        public static PhoneNumber Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number cannot be empty");

            if (!PhoneRegex.IsMatch(phone))
                throw new ArgumentException("Invalid Egyptian phone number format");

            return new PhoneNumber(phone);
        }

        public override string ToString() => Value;
    }
}
