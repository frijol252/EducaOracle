using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.ControlDosage
{
    public static class Conversores
    {
        public static string NumeroALetras(this decimal numberAsString)
        {
            string dec;

            var entero = Convert.ToInt64(Math.Truncate(numberAsString));
            var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
            if (decimales > 0)
            {
                //dec = " PESOS CON " + decimales.ToString() + "/100";
                dec = $"  {decimales:0,0} /100 Bolivians";
            }
            //Código agregado por mí
            else
            {
                //dec = " PESOS CON " + decimales.ToString() + "/100";
                dec = $"  {decimales:0,0} /100 Bolivians";
            }
            var res = NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private static string NumeroALetras(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "ZERO";
            else if (value == 1) num2Text = "ONE";
            else if (value == 2) num2Text = "TWO";
            else if (value == 3) num2Text = "THREE";
            else if (value == 4) num2Text = "FOUR";
            else if (value == 5) num2Text = "FIVE";
            else if (value == 6) num2Text = "SIX";
            else if (value == 7) num2Text = "SEVEN";
            else if (value == 8) num2Text = "EIGHT";
            else if (value == 9) num2Text = "NINE";
            else if (value == 10) num2Text = "TEN";
            else if (value == 11) num2Text = "ELEVEN";
            else if (value == 12) num2Text = "TWELVE";
            else if (value == 13) num2Text = "THIRTEEN";
            else if (value == 14) num2Text = "FOURTEEN";
            else if (value == 15) num2Text = "FIFTEEN";
            else if (value == 16) num2Text = "SIXTEEN";
            else if (value == 17) num2Text = "SEVENTEEN";
            else if (value == 18) num2Text = "EIGHTEEN";
            else if (value == 19) num2Text = "NINETEEN";
            else if (value == 20) num2Text = "TWENTY";
            else if (value < 30) num2Text = "TWENTY " + NumeroALetras(value - 20);
            else if (value == 30) num2Text = "THIRTY";
            else if (value == 40) num2Text = "FOURTY";
            else if (value == 50) num2Text = "FIFTY";
            else if (value == 60) num2Text = "SIXTY";
            else if (value == 70) num2Text = "SEVENTY";
            else if (value == 80) num2Text = "EIGHTY";
            else if (value == 90) num2Text = "NINETY";
            else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " " + NumeroALetras(value % 10);
            else if (value == 100) num2Text = "HUNDRED";
            else if (value < 200) num2Text = "HUNDRED " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800) || (value == 500) || (value == 700) || (value == 900)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + " HUNDRED";

            else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) num2Text = "THOUSAND";
            else if (value < 2000) num2Text = "THOUSAND " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " THOUSAND";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "ONE MILLION";
            }
            else if (value < 2000000)
            {
                num2Text = "ONE MILLION " + NumeroALetras(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLION ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }
    }
}
