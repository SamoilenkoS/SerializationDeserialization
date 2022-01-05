using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SerializationDeserialization
{
    class Program
    {
        static void Write()
        {
            string input;
            DateTime current = DateTime.Now;
            string filename = $"data_{current.ToString("dd MM yyyy HH mm ss").Replace(' ', '_')}.txt";
            do
            {
                input = Console.ReadLine();
                using (StreamWriter streamWriter = new StreamWriter(filename, true))
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        streamWriter.WriteLine($"{DateTime.Now:HH:mm:ss} {input}");
                    }
                }
            } while (!string.IsNullOrEmpty(input));
        }

        static void Read()
        {
            string[] filesPathes = Directory.GetFiles(
                Directory.GetCurrentDirectory(),
                "*.txt");
            foreach (var item in filesPathes)
            {
                Console.WriteLine(item);
            }

            string input = Console.ReadLine();
            if (filesPathes.Contains(input))
            {
                using (StreamReader streamReader = new StreamReader(input))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string temp = streamReader.ReadLine();
                        int position = 0;
                        for (int count = 0; count < 3; ++position)
                        {
                            if(temp[position] == ':')
                            {
                                ++count;
                            }
                        }
                        --position;
                        var date = DateTime.Parse(temp.Substring(0, position));
                        var info = temp.Substring(position + 1);
                        if(date.TimeOfDay >= DateTime.Parse("19:49:43").TimeOfDay &&
                            date.TimeOfDay <= DateTime.Parse("19:49:45").TimeOfDay &&
                            info.Length > 1)
                        {
                            Console.WriteLine(date.TimeOfDay + " " + info);
                        }
                    }
                }
            }
        }

        static void WriteJson()
        {
            string testMessage = "Hello world!";
            Message message = new Message
            {
                Date = DateTime.Now,
                Info = testMessage
            };
            using (StreamWriter sw = new StreamWriter("data.txt"))
            {
                string text = JsonSerializer.Serialize(message);
                sw.WriteLine(text);
            }
        }

        static void WriteJsonList()
        {
            string input;
            DateTime current = DateTime.Now;
            string filename = $"data_{current.ToString("dd MM yyyy HH mm ss").Replace(' ', '_')}.txt";
            List<Message> messages = new List<Message>();
            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    messages.Add(new Message
                    {
                        Date = DateTime.Now,
                        Info = input
                    });
                }
            } while (!string.IsNullOrEmpty(input));

            using(StreamWriter streamWriter = new StreamWriter(filename))
            {
                streamWriter.WriteLine(JsonSerializer.Serialize(messages));
            }
        }

        static List<Message> ReadMessages(string filepath)
        {
            List<Message> result;
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                string input = streamReader.ReadToEnd();
                result = JsonSerializer.Deserialize<List<Message>>(input);
            }

            return result;
        }

        static void Main(string[] args)
        {
            List<Message> messages = ReadMessages(@"C:\Users\Sviatoslav_Samoilenk\source\repos\SerializationDeserialization\SerializationDeserialization\bin\Debug\netcoreapp3.1\data_05_01_2022_20_45_49.txt");
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
            //WriteJsonList();
            //WriteJson();
            //using (StreamReader sr = new StreamReader("data.txt"))
            //{
            //    string input = sr.ReadToEnd();
            //    Message message = JsonSerializer.Deserialize<Message>(input);
            //    Console.WriteLine(message.Date.TimeOfDay + " " + message.Info);
            //}
        }
    }
}
