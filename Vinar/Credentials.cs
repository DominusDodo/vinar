using Effortless.Net.Encryption;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinar.Properties;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vinar
{
    public class Credentials
    {
        public TranscriptionCredentials Transcription;
        public NarrationCredentials Narration;

        public Credentials ()
        {
            Transcription = new TranscriptionCredentials();
            Narration = new NarrationCredentials();
        }

        public static Credentials FromFile(string path)
        {
            string yaml = File.ReadAllText(path);
            return FromYaml(yaml);
        }

        public void ToFile(string path)
        {
            string yaml = ToYaml();
            File.WriteAllText(path, yaml);
        }

        public static Credentials FromRegistry(string password)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(Resources.RegistryBase))
            {
                byte[] data = (byte[])key.GetValue("Credentials");
                return FromEncryptedYaml(data, password);
            }
        }

        public void ToRegistry(string password)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(Resources.RegistryBase))
            {
                byte[] data = ToEncryptedYaml(password);
                key.SetValue("Credentials", data);
            }
        }

        public static bool InRegistry()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(Resources.RegistryBase))
            {
                return key.GetValueNames().Contains("Credentials");
            }
        }

        public static void DeleteFromRegistry()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(Resources.RegistryBase, true))
            {
                key.DeleteValue("Credentials");
            }
        }

        public static Credentials FromYaml(string yaml)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            Console.WriteLine(yaml);

            return deserializer.Deserialize<Credentials>(yaml);
        }

        public string ToYaml()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return serializer.Serialize(this);
        }

        public static Credentials FromEncryptedYaml(byte[] data, string password)
        {
            byte[] iv = new ArraySegment<byte>(data, 0, 32).ToArray();
            byte[] content = new ArraySegment<byte>(data, 32, data.Length - 32).ToArray();

            byte[] key = Bytes.GenerateKey(password, "vinar", Bytes.KeySize.Default, 1000);

            string encrypted = Encoding.Unicode.GetString(content);
            string yaml = Strings.Decrypt(encrypted, key, iv);

            return FromYaml(yaml);
        }

        public byte[] ToEncryptedYaml(string password)
        {
            byte[] iv = Bytes.GenerateIV();
            byte[] key = Bytes.GenerateKey(password, "vinar", Bytes.KeySize.Default, 1000);

            string yaml = ToYaml();
            string encrypted = Strings.Encrypt(yaml, key, iv);
            byte[] content = Encoding.Unicode.GetBytes(encrypted);

            byte[] data = iv.Concat(content).ToArray();
            return data;
        }

        public class TranscriptionCredentials
        {
            public string Id;
            public string Url;
            public string Location;
            public string Key;

            [YamlIgnore]
            public string UrlBase
            {
                get { return $"{Url}/{Location}/Accounts/{Id}"; }
            }

            [YamlIgnore]
            public string UrlBaseAuth
            {
                get { return $"{Url}/auth/{Location}/Accounts/{Id}"; }
            }
        }

        public class NarrationCredentials
        {
            public string Location = "";
            public string Key = "";
        }
    }
}
