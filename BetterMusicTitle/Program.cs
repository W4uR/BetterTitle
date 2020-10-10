using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace BetterMusicTitle
{
    class Program
    {
        static List<string> FW = new List<string>() {
        "official",
        "lyrics",
        "music video",
        "video",
        "hd",
        "original",
        "live in",
        "live at"
        };

        static string newPath = "BetterMusicTitles";
        static void Main(string[] args)
        {
            Console.Title = "Better title generator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Better Title Generator!\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Looking for files...");
            List<string> files = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Files read successfully!");
            string Dest = Directory.GetCurrentDirectory().Replace(Directory.GetCurrentDirectory().Split('\\').Last(), newPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating Destination directory by the name \"BetterMusicTitles\"...");
            Directory.CreateDirectory(Dest);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Dest + " successfully created!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Copying and renameing files...");
            foreach (string m in files)
            {
                string fileName = m.Split('\\').Last();
                if (!File.Exists(Dest + "\\" + MakeItBetter(fileName) + ".mp3"))
                {
                    string newFile = MakeItBetter(fileName);
                    if (newFile != "@")
                    {
                        Console.WriteLine("Creating: {0}...", MakeItBetter(fileName));
                        File.Copy(m, Dest + "\\" + MakeItBetter(fileName) + ".mp3");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All Done! Press any key to exit...");
            Console.ReadKey();
        }


        static string MakeItBetter(string original)
        {
            if (!original.Contains(".mp3"))
            {
                return "@";
            }

            string updated = original.ToLower().Replace(".mp3","");
            if (updated.Contains('('))
            {
                updated = updated.Split('(')[0];
            }
            if (updated.Contains('['))
            {
                updated = updated.Split('[')[0];
            }
            foreach (string w in FW)
            {
                updated = updated.Replace(w,"");
            }
            if (updated.Contains('-'))
            {
                while (updated.Split('-').Count() > 2)
                {
                    updated = updated.Remove(updated.LastIndexOf('-'));
                }
            }
            if (updated.Contains(' '))
            {
                string[] tmp = updated.Split(' ');
                updated = "";
                for (int i = 0; i < tmp.Length; i++)
                {
                    if(tmp[i] == "w")
                    tmp[i] = " ";
                    updated += tmp[i] + " ";
                }
            }
            updated = updated.Replace("-", " - ");
            while(updated.Contains("  "))
            {
                updated = updated.Replace("  ", " ");
            }
            updated = Regex.Replace(updated, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


            return updated;
        }
    }
}
