using System.Text.RegularExpressions;

namespace IdentityService.Domain.ValueObjects
{
    public record Password
    {
        public string Hash { get; }

        private Password(string hash) => Hash = hash;

        public static bool ValidatePlainText(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            if (!Regex.IsMatch(password, "[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            return true;
        }

        public static Password CreateFromHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Password hash cannot be empty");

            return new Password(hash);
        }

        public override string ToString() => Hash;
    }
}
