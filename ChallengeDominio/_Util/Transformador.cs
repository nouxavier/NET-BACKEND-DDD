using System.Globalization;
using System.Linq;
using System.Text;

namespace ChallengeDominio._Util
{
    public class Transformador
    {
        /// <summary>
        /// Substitui os acentos por chars comuns e deixa o strring todo em maiúsculas
        /// </summary>
        /// <param name="termo"></param>
        /// <returns></returns>
        public static string Normaliza(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return termo;

            termo = termo.ToUpper().Normalize(NormalizationForm.FormD);
            var chars = termo.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }


    }
}
