using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.Data.Linq;
using System.Drawing;

namespace ECOPETROL_COMMON.UTILOBJECTS
{

    public enum KeySizes
    {
        SIZE_512 = 512,
        SIZE_1024 = 1024,
        SIZE_2048 = 2048,
        SIZE_952 = 952,
        SIZE_1369 = 1369
    };
    public enum CryptoOperation
    {
        ENCRYPT,
        DECRYPT
    };

    public class RsaFileDemo
    {
        public static Byte[] Encriptar(String mensaje, String id_detalle)
        {
            string message = mensaje;

            string strRutaDefinitiva = string.Empty;
            string ruta = "";
            string ruta2 = "";

            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaCertificadosFactura"];

            String carpeta = "Publica";
            String carpeta2 = "Privada";

            ruta = strRutaDefinitiva + "\\" + carpeta + "\\" + id_detalle;
            ruta2 = strRutaDefinitiva + "\\" + carpeta2 + "\\" + id_detalle;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            if (!Directory.Exists(ruta2))
                Directory.CreateDirectory(ruta2);

            string publicKey = ruta + "/pub.cert";
            string privateKey = ruta2 + "/priv.cert";

            GenerateKeys(publicKey, privateKey);
            byte[] encrypted = Encrypt(publicKey, Encoding.UTF8.GetBytes(message));
            

            return encrypted;
        }

        public static Byte[] Desencriptar(byte[] mensaje, String id_detalle)
        {
            string strRutaDefinitiva = string.Empty;
            string ruta2 = "";
            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaCertificadosFactura"];
            String carpeta2 = "Privada";

            ruta2 = strRutaDefinitiva + "\\" + carpeta2 + "\\" + id_detalle;

            string privateKey = ruta2 + "/priv.cert";

            byte[] decrypted = Decrypt(privateKey, mensaje);

            return decrypted;
        }

        private static void GenerateKeys(string publicKeyFile, string privateKeyFile)
        {
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;

                if (File.Exists(privateKeyFile))
                    File.Delete(privateKeyFile);

                if (File.Exists(publicKeyFile))
                    File.Delete(publicKeyFile);

                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(publicKeyFile, publicKey);
                string privateKey = rsa.ToXmlString(true);
                File.WriteAllText(privateKeyFile, privateKey);
            }
        }

        private static byte[] Encrypt(string publicKeyFile, byte[] plain)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;
                string publicKey = File.ReadAllText(publicKeyFile);
                rsa.FromXmlString(publicKey);
                encrypted = rsa.Encrypt(plain, true);
            }

            return encrypted;
        }

        private static byte[] Decrypt(string privateKeyFile, byte[] encrypted)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;
                string privateKey = File.ReadAllText(privateKeyFile);
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            return decrypted;
        }




        public static Byte[] Launch(String mensaje)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                aes.GenerateKey();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] encrypted = AESCrypto(CryptoOperation.ENCRYPT, aes, Encoding.UTF8.GetBytes(mensaje));


                return encrypted;
            }


        }

        public static Byte[] LaunchDes(byte[] mensaje)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                aes.GenerateKey();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] decrypted = AESCrypto(CryptoOperation.DECRYPT, aes, mensaje);

                return decrypted;
            }

        }
        public static byte[] AESCrypto(CryptoOperation cryptoOperation, AesCryptoServiceProvider aes, byte[] message)
        {

            string strRutaDefinitiva = string.Empty;
            string ruta = "";


            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaCertificadosFactura"];

            String carpeta = "Llave";


            ruta = strRutaDefinitiva + "\\" + carpeta + "\\" + 1;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            FileStream stream = new FileStream("D:\\test.txt",
            FileMode.OpenOrCreate, FileAccess.Write);

            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes("ASALUD93"),
                IV = ASCIIEncoding.ASCII.GetBytes("ASALUD93")
            };

            CryptoStream crStream = new CryptoStream(stream,
               cryptic.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] data = ASCIIEncoding.ASCII.GetBytes("Hello World!");

            crStream.Write(data, 0, data.Length);


            return data;

        }

        public static Byte[] LaunchDemo(String mensaje, String Id_datalle)
        {
            byte[] IV = GenerateIv();
            byte[] key = GenerateKey();

            byte[] encrypted = DESCrypto(CryptoOperation.ENCRYPT, IV, key, Encoding.UTF8.GetBytes(mensaje), Id_datalle);

            return encrypted;

        }
        public static string LaunchDemoDes(Byte[] mensaje, String Id_datalle)
        {

           string decrypted = DESCryptoDes(mensaje, Id_datalle);

            return decrypted;

        }


        public static byte[] GenerateRandomByteArray(int size)
        {
            var random = new Random();
            byte[] byteArray = new byte[size];
            random.NextBytes(byteArray);
            return byteArray;
        }

        public static byte[] GenerateIv()
        {
            byte[] IV = GenerateRandomByteArray(8);
            return IV;
        }

        public static byte[] GenerateKey()
        {
            byte[] key = GenerateRandomByteArray(8);
            return key;

        }

        public static byte[] DESCrypto(CryptoOperation cryptoOperation, byte[] IV, byte[] key, byte[] message, String id_detalle)
        {
            string strRutaDefinitiva = string.Empty;
            string ruta = "";

            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaCertificadosFactura"];
            String carpeta = "LlaveFact";
            ruta = strRutaDefinitiva + "\\" + carpeta + "\\" + id_detalle;
            
            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);
          
            string publicKey = ruta + "/.txt";
           

            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            FileStream stream = new FileStream(publicKey, FileMode.OpenOrCreate, FileAccess.Write);

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes("IJKLMNOP");

            CryptoStream crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);

            crStream.Write(message, 0, message.Length);

            crStream.Close();
            stream.Close();

            return message;

        }

        public static string DESCryptoDes(byte[] message, String id_detalle)
        {

            string strRutaDefinitiva = string.Empty;
            string ruta2 = "";
            strRutaDefinitiva = ConfigurationManager.AppSettings["rutaCertificadosFactura"];
            String carpeta2 = "LlaveFact";

            ruta2 = strRutaDefinitiva + "\\" + carpeta2 + "\\" + id_detalle;

            string privateKey = ruta2 + "/.txt";

            FileStream stream = new FileStream(privateKey, FileMode.Open, FileAccess.Read);

            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

            cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
            cryptic.IV = ASCIIEncoding.ASCII.GetBytes("IJKLMNOP");

            CryptoStream crStream = new CryptoStream(stream,cryptic.CreateDecryptor(), CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(crStream);

            string data = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return data;

        }



    }
}
