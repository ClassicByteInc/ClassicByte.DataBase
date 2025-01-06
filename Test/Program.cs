using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicByte.DataBase;
using YamlDotNet.Core;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase db = new DataBase("Db1");

            var key = db.Root.CreateKey("1/2/3/4/5/6/7");
            key.SetValue("value","wdnmd");




            var serializer = new SerializerBuilder()
           .WithNamingConvention(CamelCaseNamingConvention.Instance)
           .Build();

            // 序列化对象为YAML
            string yaml = serializer.Serialize(db);

            Console.WriteLine(yaml);
            Console.ReadLine();
        }
    }
}
