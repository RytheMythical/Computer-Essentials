using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Base64
    {
        public static async Task<string> EncodeDecodeToBase64String(string input, bool Encode)
        {
            string Return = "";
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt", input);
            if (Encode == true)
            {
                await Command.RunCommandHidden("certutil -encode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode.txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded.txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
            }
            else
            {
                await Command.RunCommandHidden("certutil -decode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode.txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded.txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
            }

            return Return;
        }
        // Usage Example: string Jerjer = await EncodeDecodeToBase64String(input, [true or false])//
        //true = encode, false = decode//

        public static void EncodeFile(string Input, string Output)
        {
            Command.RunCommand("certutil -encode \"" + Input + "\" \"" + Output + "\"");
        }

        public static void Decode(string Input, string Output)
        {
            Command.RunCommand("certutil -decode \"" + Input + "\" \"" + Output + "\"");
        }
    }
}
