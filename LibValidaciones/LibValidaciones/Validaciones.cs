using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LibValidaciones
{
    public class Validaciones
    {
        static Regex regex;
        /// <summary>
        /// Verifica si el nombre cumple con el patrón de validación (Nombre o apellido)
        /// </summary>
        /// <param name="nombre">La cadena a validar</param>
        /// <returns>True si cumple con la validación, false si no cumple</returns>

        public static bool Palabras (string palabras)
        {
            Regex regEx = new Regex(@"^[a-zA-ZñÑ]");
            return regEx.IsMatch(palabras);
        }

        public static bool Caracter (string caracter)
        {
            Regex regEx = new Regex(@"^[a-zA-ZñÑ0-9]{0,1}$");
            return regEx.IsMatch(caracter);
        }

        public static bool numeros(string Num)
        {
            Regex regEx = new Regex(@"^[0-9]$");
            return regEx.IsMatch(Num);
        }

        public static bool Deecimal(string Dec)
        {
            Regex regEx = new Regex(@"^[0 - 9]([.,][0 - 9]{ 1, 3 })?$");
            return regEx.IsMatch(Dec);
        }

        public static bool dooble(String Dob)
        {
            Regex regEx = new Regex(@"^[0-9](?:\.[0-9])?$");
            return regEx.IsMatch(Dob);
        }
    }
}
