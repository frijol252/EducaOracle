using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.ControlDosage
{
    public static class AllegedRC4
    {
        /// <summary>
        /// Criptografia simetrica
        /// <see cref="http://jc-mouse.net/"/>
        /// <param name="message">texto a encriptar</param>                
        /// <param name="key">llave para encriptacion</param>                
        /// <param name="unscripted">con guion o sin guion</param>                
        /// <returns>mensaje encriptado</returns>
        /// </summary>       
        public static String encryptMessageRC4(String message, String key, Boolean unscripted)
        {
            int[] state = new int[256];
            int x = 0;
            int y = 0;
            int index1 = 0;
            int index2 = 0;
            int nmen;
            String messageEncryption = "";

            for (int i = 0; i <= 255; i++)
            {
                state[i] = i;
            }

            for (int i = 0; i <= 255; i++)
            {
                index2 = (((int)key[index1]) + state[i] + index2) % 256;
                int aux = state[i];
                state[i] = state[index2];
                state[index2] = aux;
                index1 = (index1 + 1) % key.Length;
            }

            for (int i = 0; i < message.Length; i++)
            {
                x = (x + 1) % 256;
                y = (state[x] + y) % 256;
                int aux = state[x];
                state[x] = state[y];
                state[y] = aux;
                nmen = ((int)message[i]) ^ state[(state[x] + state[y]) % 256];
                String nmenHex = nmen.ToString("X").ToUpper();
                messageEncryption = messageEncryption + ((unscripted) ? "" : "-") + ((nmenHex.Length == 1) ? ("0" + nmenHex) : nmenHex);
            }
            return (unscripted) ? messageEncryption : messageEncryption.Substring(1, messageEncryption.Length - 1);
        }

    }
}
