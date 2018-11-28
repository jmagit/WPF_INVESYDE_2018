using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Core.Validadores {
    public static class ValidarCadenasExtensions {
        /// <summary>
        /// Método extensor que indica si la cadena esta a nulo, es cadena vacía o solo tiene espacios
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si no contiene nada significativo, en otro caso <c>false</c>.
        /// </returns>
        public static bool EstaVacia(this string cad) {
            return string.IsNullOrWhiteSpace(cad);
        }

        /// <summary>
        /// Método extensor que indica si la cadena esta a nulo, es cadena vacía o solo tiene espacios
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si no contiene nada significativo, en otro caso <c>false</c>.
        /// </returns>
        public static bool EstaVacio(this string cad) {
            return EstaVacia(cad);
        }
        /// <summary>
        /// Método extensor que indica  si la cadena contiene al menos un carácter.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si contiene algo, en otro caso <c>false</c>.
        /// </returns>
        public static bool EstaRellena(this string cad) {
            return !cad.EstaVacia();
        }
        /// <summary>
        /// Método extensor que indica  si la cadena contiene al menos un carácter.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si contiene algo, en otro caso <c>false</c>.
        /// </returns>
        public static bool EstaRelleno(this string cad) {
            return EstaRellena(cad);
        }

        private const string tablaAsignacion = "TRWAGMYFPDXBNJZSQVHLCKET";

        /// <summary>
        /// Determina si es un NIF es valido.
        /// </summary>
        /// <param name="nif">NIF</param>
        /// <returns>
        /// 	<c>true</c> si el NIF es válido; en otro caso, <c>false</c>.
        /// </returns>
        public static bool NIF(string nif) {
            nif = nif.ToUpper();
            if (!Regex.IsMatch(nif, @"^[XYZ0-9]([0-9]{7})[TRWAGMYFPDXBNJZSQVHLCKE]$"))
                return false;
            else if (!Regex.IsMatch(nif, @"^[XYZ]"))
                //Es un NIF de un Español con DNI
                return (tablaAsignacion[(int.Parse(nif.Substring(0, 8)) % 23)] == nif[8]);
            else
            //Es un NIF de extranjero
            {
                int numero = int.Parse(nif.Substring(1, 7));

                switch (nif[0]) {
                    case 'X':
                        return tablaAsignacion[numero % 23] == nif[8];
                    case 'Y':
                        return tablaAsignacion[(10000000 + numero) % 23] == nif[8];
                    case 'Z':
                        return tablaAsignacion[(20000000 + numero) % 23] == nif[8];
                    default:
                        return false;

                }

            }
        }
        /// <summary>
        /// Método extensor que determina si es un NIF es valido.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si el NIF es válido; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNIF(this string cad) {
            return NIF(cad);
        }
        /// <summary>
        /// Método extensor que determina si es un NIF es valido.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> si el NIF no es válido; otherwise, <c>false</c>.
        /// </returns>
        public static bool NotIsNIF(this string cad) {
            return !NIF(cad);
        }

        /// <summary>
        /// Determina si es un CIF es válido.
        /// </summary>
        /// <param name="cif">CIF</param>
        /// <returns>
        /// 	<c>true</c> si el CIF es válido; otherwise, <c>false</c>.
        /// </returns>
        public static bool CIF(string cif) {
            if (string.IsNullOrEmpty(cif)) return false;
            cif = cif.Trim().ToUpper();

            //Debe tener una longitud igual a 9 caracteres;
            if (cif.Length != 9) return false;
            // ... y debe comenzar por una letra, la cual pasamos a mayúscula, ... 
            // 
            string firstChar = cif.Substring(0, 1);
            // ...que necesariamente deberá de estar comprendida en 
            // el siguiente intervalo: ABCDEFGHJNPQRSUVW 
            // 
            string cadena = "ABCDEFGHJNPQRSUVW";
            if (cadena.IndexOf(firstChar) == -1) return false;
            try {
                Int32 sumaPar = default(Int32);
                Int32 sumaImpar = default(Int32);
                // A continuación, la cadena debe tener 7 dígitos + el dígito de control. 
                // 
                string cif_sinControl = cif.Substring(0, 8);
                string digits = cif_sinControl.Substring(1, 7);
                for (Int32 n = 0; n <= digits.Length - 1; n += 2) {
                    if (n < 6) {
                        // Sumo las cifras pares del número que se corresponderá 
                        // con los caracteres 1, 3 y 5 de la variable «digits». 
                        // 
                        sumaPar += Convert.ToInt32(digits[n + 1].ToString());
                    }
                    // Multiplico por dos cada cifra impar (caracteres 0, 2, 4 y 6). 
                    // 
                    Int32 dobleImpar = 2 * Convert.ToInt32(digits[n].ToString());
                    // Acumulo la suma del doble de números impares. 
                    // 
                    sumaImpar += (dobleImpar % 10) + (dobleImpar / 10);
                }
                // Sumo las cifras pares e impares. 
                // 
                Int32 sumaTotal = sumaPar + sumaImpar;
                // Me quedo con la cifra de las unidades y se la resto a 10, siempre 
                // y cuando la cifra de las unidades sea distinta de cero 
                // 
                sumaTotal = (10 - (sumaTotal % 10)) % 10;
                // Devuelvo el Dígito de Control dependiendo del primer carácter 
                // del NIF pasado a la función. 
                //
                string digitoControl = "";
                switch (firstChar) {
                    case "N":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "W":
                        // NIF de entidades cuyo dígito de control se corresponde 
                        // con una letra. 
                        // 
                        // Al estar los índices de los arrays en base cero, el primer 
                        // elemento del array se corresponderá con la unidad del número 
                        // 10, es decir, el número cero. 
                        // 
                        char[] characters = { 'J', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
                        digitoControl = characters[sumaTotal].ToString();
                        break;
                    default:
                        // NIF de las restantes entidades, cuyo dígito de control es un número. 
                        // 
                        digitoControl = sumaTotal.ToString();
                        break;
                }
                return digitoControl.Equals(cif.Substring(8, 1));
            } catch (Exception) {
                // Cualquier excepción producida, devolverá false. 
                return false;
            }
        }
        /// <summary>
        /// Método extensor que determina si es un CIF es válido.
        /// </summary>
        /// <param name="cif">CIF</param>
        /// <returns>
        /// 	<c>true</c> si el CIF es válido; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCIF(this string cad) {
            return CIF(cad);
        }
        /// <summary>
        /// Método extensor que determina si es un CIF es válido.
        /// </summary>
        /// <param name="cif">CIF</param>
        /// <returns>
        /// 	<c>true</c> si el CIF no es válido; otherwise, <c>false</c>.
        /// </returns>
        public static bool NotIsCIF(this string cad) {
            return !CIF(cad);
        }
    }
}
