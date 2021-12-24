using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault;
using System;
using Azure.Security.KeyVault.Keys.Cryptography;
using System.Text;

namespace AZKeyVaultTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //fetched value by service priniciple 

            string keystr = "https://keyvaultrk2020.vault.azure.net/"; //give your key vault  url here 
            var client = new SecretClient( new Uri( keystr), new DefaultAzureCredential(true));

            KeyVaultSecret secret = client.GetSecret("dempsecret"); 
            Console.WriteLine(secret.Value);



            //// fetch key for encription decription 
            var keyClient = new KeyClient(new Uri(keystr), new DefaultAzureCredential(true));

            KeyVaultKey keyVaultkey = keyClient.GetKey("rkkey");
                
            var crpcl = new CryptographyClient(keyVaultkey.Id, new DefaultAzureCredential(true));
            byte[] test = Encoding.UTF8.GetBytes("pari is sweet gal");
            //

            EncryptResult res = crpcl.Encrypt(EncryptionAlgorithm.RsaOaep, test);
            //Console.ReadLine(res.Ciphertext.ToString());
            Console.WriteLine(res.Ciphertext);
            DecryptResult decrypt = crpcl.Decrypt(EncryptionAlgorithm.RsaOaep, res.Ciphertext);
            string txt = Encoding.UTF8.GetString(decrypt.Plaintext);
            Console.WriteLine(txt);
            Console.ReadLine();
        }
    }
}
    