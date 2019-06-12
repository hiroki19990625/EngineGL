using System;
using System.Diagnostics;
using EngineGL.Serializations;
using MessagePack;
using NUnit.Framework;

namespace EngineGL.Tests.FormatMessage
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void FormatTest()
        {
            Parson bob = new Parson
            {
                Name = "Bob",
                Age = 23,
                JobData = new Job
                {
                    Type = JobType.PG,
                    JobCaria = 1
                }
            };
            Parson alice = new Parson
            {
                Name = "Alice_aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa_1234",
                Age = 35,
                JobData = new Job
                {
                    Type = JobType.PM,
                    JobCaria = 5
                }
            };

            string json;
            Parson p;
            Stopwatch wc = Stopwatch.StartNew();
            Console.WriteLine("Json Deserializable");

            json = bob.ToDeserializableJson();
            Console.WriteLine(json);
            p = json.FromDeserializableJson<Parson>();
            Assert.True(p.Name == bob.Name);
            Assert.False(p.Name == alice.Name);

            wc.Stop();
            Console.WriteLine(wc.Elapsed.ToString());
            Console.WriteLine();
            Console.WriteLine("Json");
            wc.Restart();

            json = bob.ToJson(true);
            Console.WriteLine(json);
            p = json.FromDeserializableJson<Parson>();
            Assert.True(p.Name == bob.Name);
            Assert.False(p.Name == alice.Name);

            wc.Stop();
            Console.WriteLine(wc.Elapsed.ToString());
            Console.WriteLine();
            Console.WriteLine("Yaml");
            wc.Restart();

            string yaml;

            yaml = alice.ToYaml();
            Console.WriteLine(yaml);
            p = yaml.FromYaml<Parson>();
            Assert.True(p.Name == alice.Name);
            Assert.False(p.Name == bob.Name);

            wc.Stop();
            Console.WriteLine(wc.Elapsed.ToString());
            Console.WriteLine();
            Console.WriteLine("Bin");
            wc.Restart();

            byte[] bin;
            bin = alice.ToBinary();
            Console.WriteLine(Convert.ToBase64String(bin));
            p = bin.FromBinary<Parson>();
            Assert.True(p.Name == alice.Name);
            Assert.False(p.Name == bob.Name);

            wc.Stop();
            Console.WriteLine(wc.Elapsed.ToString());
            Console.WriteLine();
            Console.WriteLine("Bin Compress");
            wc.Restart();

            bin = alice.ToCompressBinary();
            Console.WriteLine(Convert.ToBase64String(bin));
            p = bin.FromCompressBinary<Parson>();
            Assert.True(p.Name == alice.Name);
            Assert.False(p.Name == bob.Name);

            wc.Stop();
            Console.WriteLine(wc.Elapsed.ToString());
        }

        [MessagePackObject]
        public class Parson
        {
            [Key(0)] public string Name { get; set; }
            [Key(1)] public int Age { get; set; }

            [Key(2)] public Job JobData { get; set; }
        }

        [MessagePackObject]
        public class Job
        {
            [Key(0)] public JobType Type;

            [Key(1)] public int JobCaria;
        }

        public enum JobType
        {
            PG,
            PM,
            ECO
        }
    }
}