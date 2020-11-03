using System;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Util
{
    public static class FuncUtil
    {
        private const string REGEX_EMAIL = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private const string REGEX_CPF = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$";

        public static bool RegexMatch(string regexMatch, string valueMatch)
        {
            if (String.IsNullOrWhiteSpace(valueMatch)) return false;

            Regex regex = new Regex(regexMatch);

            Match match = regex.Match(valueMatch);

            return match.Success;
        }

        public static bool ValidarEmail(string email) => RegexMatch(REGEX_EMAIL, email);
        public static bool ValidarCPF(string cpf) => RegexMatch(REGEX_CPF, cpf);
    }
}
