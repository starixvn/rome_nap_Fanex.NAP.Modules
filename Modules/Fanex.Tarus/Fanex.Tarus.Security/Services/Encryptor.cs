using Fanex.Cryptography.Aes;
using System;

namespace Fanex.Tarus.Security.Services
{
    public class Encryptor
    {
        protected Encryptor() { }
        
        public static string Code { get; } = GetCaptchaString(4);

        public static string GetCaptchaString(int length)
        {
            var varZero = '0';
            var varNine = '9';
            var varA = 'A';
            var varZ = 'Z';
            var varCount = 0;
            var varRandomNumber = 0;
            var strCaptchaString = string.Empty;
            var random = new Random(System.DateTime.Now.Millisecond);

            while (varCount < length)
            {
                varRandomNumber = random.Next(varZero, varZ);
                if ((varRandomNumber >= varZero && varRandomNumber <= varNine) || (varRandomNumber >= varA && varRandomNumber <= varZ))
                {
                    strCaptchaString = strCaptchaString + (char)varRandomNumber;
                    varCount = varCount + 1;
                }
            }

            return strCaptchaString;
        }
        
        public static string Decrypt(string encrypted, string code)
            => AesExtension.Decrypt(encrypted, code);
    }
}
