using DualTextReader.SentenceExtractors;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace DualTextReader
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("cs-CZ");
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length == 0)
            {
                Console.WriteLine("Expected a path to the file");
                return;
            }

            var pathToFile = args[0];

            if (!File.Exists(pathToFile))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            if (Path.GetExtension(pathToFile) != ".pdf")
            {
                Console.WriteLine("File should be in pdf format");
                return;
            }

            var sentenceExtractor = new PdfSentenceExtractor(pathToFile);

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        sentenceExtractor.GoToPrevious();
                        SetSentance(sentenceExtractor);
                        break;
                    case ConsoleKey.RightArrow:
                        sentenceExtractor.GoToNext();
                        SetSentance(sentenceExtractor);
                        break;
                    case ConsoleKey.UpArrow:
                        SetSentance(sentenceExtractor);
                        break;
                }
            }
        }

        private static void SetSentance(PdfSentenceExtractor sentenceExtractor)
        {
            Console.Clear();
            Console.WriteLine(sentenceExtractor.Sentence);
        }
    }
}
