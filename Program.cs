using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace LINQ_03_Gruppierung_und_Mengenoperationen
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Aufgabe1();
            Aufgabe2();





        }
        public static void Ausgabe<TKey, TElement>(IEnumerable<IGrouping<TKey, TElement>> GroupingListe, string ausgabe)
        {
            Console.WriteLine(ausgabe+":");
            foreach (var group in GroupingListe)
            {
                foreach (var element in group)
                {
                    Console.WriteLine(element);
                }
            }
        }
        public static void Aufgabe1()
        {
            string[] numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen" };
            /* 1.
            Gruppieren Sie die Worte im obigen Array nach dem Anfangsbuchstaben
            2.
            Gruppieren Sie die Worte im obigen Array nach der Länge
            3.
            Gruppieren Sie die Worte im obigen Array nach dem Anfangsbuchstaben und der Länge    */

            var nachAnfangsbuchstaben = numbers.OrderBy(x => x).GroupBy(x => x[0]);
            Ausgabe(nachAnfangsbuchstaben, "Nach Anfangsbuchstaben");

            //foreach (var item in nachAnfangsbuchstaben)
            //{
            //    Console.WriteLine(item.Key);
            //    foreach (var inneresItem in item)
            //    {
            //        Console.WriteLine(inneresItem);
            //    }
            //    Console.WriteLine();
            //}
            var nachLänge = numbers.OrderBy(x => x.Length).GroupBy(x => x.Length);
            Ausgabe(nachLänge, "Nach Länge:");
            //Console.WriteLine("nach Länge:");
            //foreach (var item in nachLänge)
            //{
            //    Console.WriteLine(item.Key);
            //    foreach (var inneresItem in item)
            //    {
            //        Console.WriteLine(inneresItem);
            //    }
            //    Console.WriteLine();
            //}
            var nachAnfangsbuchstabenUndLänge = numbers.GroupBy(x => new { firstLetter = x[0], länge = x.Length });
            Ausgabe(nachAnfangsbuchstabenUndLänge, "Nach Anfangsbuchsaben und Länge:");

            //var step1 = numbers.OrderBy(x => x).GroupBy(x => x[0], x => x);
            //Console.WriteLine("Nach Buchstabe gruppiert, dann nach Länge:");
            //foreach (var item in step1)
            //{
            //    Console.WriteLine(item.Key);
            //    var step2 = item.GroupBy(x => x.Length, x => x);
            //    foreach (var item2 in step2)
            //    {
            //        Console.WriteLine(item2.Key);
            //        foreach (var item3 in item2)
            //        {
            //            Console.WriteLine(item3);
            //        }
            //        Console.WriteLine();
            //    }

            //}
        }
        public static void Aufgabe2()
        {
            /*
             * Die folgenden Gruppierungen sollen auf der Liste der Prozesse auf Ihrem System stattfinden:
1.
Geben Sie die Prozesse auf Ihrem System gruppiert nach der Anzahl der Threads aus
2.
Geben Sie die Prozesse auf Ihrem System gruppiert nach der Anzahl der Module aus
3.
Geben Sie die Prozesse auf Ihrem System gruppiert nach der Anzahl der Module aus, in der Ausgabe sollen die Namen der Prozesse alphabetisch aufsteigend sortiert sein
Hinweis: Das Abfragen der Anzahl der Module eines Prozesses führt ggf. zu einer Exception*/

            var prozesse = Process.GetProcesses().OrderBy(x => x.Threads.Count).GroupBy(x => x.Threads.Count, x=>x.ProcessName);
            try
            {
                foreach (var a in prozesse)
                {
                    Console.WriteLine("Key:" + a.Key + " Threads");
                    foreach (var b in a)
                    {
                        Console.WriteLine(b);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Thread - Fehler:" + e.Message);
            }

            var module = Process.GetProcesses().Select(x =>
            {
                int moduleCount = 0;
                try
                {
                    moduleCount = x.Modules.Count;
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    Console.WriteLine(e);
                }
                return new { Process = x, ModuleCount = moduleCount };
            }).GroupBy(x=>x.ModuleCount);


        //    var nachAnzahlderModule = Process.GetProcesses().GroupBy(x=> x.Modules.Count)    //Unvollständig


            foreach (var proc in module)
            {
                foreach(var entry in proc)
                {
                    Console.WriteLine(entry.Process+" - "+entry.ModuleCount);
                }
            }


        }
    }
}
