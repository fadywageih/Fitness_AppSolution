using System.Text.RegularExpressions;

namespace IdentityService.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        private static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled);

        private Email(string value) => Value = value;

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            if (!EmailRegex.IsMatch(email))
                throw new ArgumentException("Invalid email format");

            return new Email(email.ToLowerInvariant());
        }

        public override string ToString() => Value;
    }
}
