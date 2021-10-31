using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vinar
{
    public class Credentials
    {
        public TranscriptionCredentials Transcription;
        public NarrationCredentials Narration;

        public static Credentials Load(string path)
        {
            string yml = File.ReadAllText(path);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            return deserializer.Deserialize<Credentials>(yml);
        }

        public class TranscriptionCredentials
        {
            public string Id;
            public string Url;
            public string Location;
            public string Key;

            public string UrlBase
            {
                get { return $"{Url}/{Location}/Accounts/{Id}"; }
            }

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
