using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class FortalezaClave
    {
        public enum PasswordStrength
        {
            Blanco = 0,
            MuyDebil = 1,
            Debil = 2,
            Media = 3,
            Fuerte = 4,
            MuyFuerte = 5
        }

        public PasswordStrength Calificacion { get; set; }

        public FortalezaClave()
        {

        }

        public PasswordStrength GetPasswordStrength(string password)
        {
            int score = 0;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(password.Trim())) return PasswordStrength.Blanco;
            if (HasMinimumLength(password, 5)) score++;
            if (HasMinimumLength(password, 8)) score++;
            if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password)) score++;
            if (HasDigit(password)) score++;
            if (HasSpecialChar(password)) score++;
            return (PasswordStrength)score;
        }

        public bool IsStrongPassword(string password)
        {
            return HasMinimumLength(password, 8)
                && HasUpperCaseLetter(password)
                && HasLowerCaseLetter(password)
                && (HasDigit(password) || HasSpecialChar(password));
        }

        /*public static bool IsValidPassword(string password, PasswordOptions opts)
        {
            return IsValidPassword(
                password,
                opts.RequiredLength,
                opts.RequiredUniqueChars,
                opts.RequireNonAlphanumeric,
                opts.RequireLowercase,
                opts.RequireUppercase,
                opts.RequireDigit);
        }*/

        public bool IsValidPassword(string password, int requiredLength, int requiredUniqueChars, bool requireNonAlphanumeric,
            bool requireLowercase, bool requireUppercase, bool requireDigit)
        {
            if (!HasMinimumLength(password, requiredLength)) return false;
            if (!HasMinimumUniqueChars(password, requiredUniqueChars)) return false;
            if (requireNonAlphanumeric && !HasSpecialChar(password)) return false;
            if (requireLowercase && !HasLowerCaseLetter(password)) return false;
            if (requireUppercase && !HasUpperCaseLetter(password)) return false;
            if (requireDigit && !HasDigit(password)) return false;
            return true;
        }

        public bool HasMinimumLength(string password, int minLength)
        {
            return password.Length >= minLength;
        }

        public bool HasMinimumUniqueChars(string password, int minUniqueChars)
        {
            return password.Distinct().Count() >= minUniqueChars;
        }

        public bool HasDigit(string password)
        {
            return password.Any(c => char.IsDigit(c));
        }

        public bool HasSpecialChar(string password)
        {
            return password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;
        }
        public bool HasUpperCaseLetter(string password)
        {
            return password.Any(c => char.IsUpper(c));
        }

        public bool HasLowerCaseLetter(string password)
        {
            return password.Any(c => char.IsLower(c));
        }




    }
}
