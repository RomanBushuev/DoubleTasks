using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Console.Out;
            int a_1_1 = 0;
            int a_1_2 = 0;
            int a_2_1 = 0;
            int a_2_2 = 0;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                for (int i = 0; i < 10000; ++i)
                {
                    int a = 1;
                    int b = 1;

                    Task task1 = new Task(() => { b = b + 1; Console.WriteLine(a); });
                    Task task2 = new Task(() => { a = a + 1; Console.WriteLine(b); });

                    task1.Start();
                    task2.Start();
                    //All console outputs goes here
                    Task.WaitAll(new[] { task1, task2 });
                    string consoleOutput = stringWriter.ToString();
                    string[] strings = consoleOutput.Split(new char[] { '\r', '\b', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    strings = strings.Skip(strings.Count() - 2).ToArray();
                    Console.SetCursorPosition(0, 1);

                    if (strings.First() == "1" && strings.Last() == "1")
                        a_1_1++;
                    if (strings.First() == "1" && strings.Last() == "2")
                        a_1_2++;
                    if (strings.First() == "2" && strings.Last() == "1")
                        a_2_1++;
                    if (strings.First() == "2" && strings.Last() == "2")
                        a_2_2++;
                    Console.Clear();
                }

            }

            Console.SetOut(t);
            Console.WriteLine($"1_1:{a_1_1}");
            Console.WriteLine($"1_2:{a_1_2}");
            Console.WriteLine($"2_1:{a_2_1}");
            Console.WriteLine($"2_2:{a_2_2}");
        }
    }
}
