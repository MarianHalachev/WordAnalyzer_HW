using System.Text;


namespace WordAnalyzer_HW
{
    internal class Program
    {
        private static string[] words;
        private static int wordCount;
        private static string shortestWord;
        private static string longestWord;
        private static double averageLength;
        private static string[] mostCommonWords;
        private static string[] leastCommonWords;
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Enter the path of the text file: ");
            string filePath = Console.ReadLine();



            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                words = GetWords(text);

                
                Thread wordCountThread = new Thread(() => wordCount = GetWordCount(words));
                Thread shortestWordThread = new Thread(() => shortestWord = GetShortestWord(words));
                Thread longestWordThread = new Thread(() => longestWord = GetLongestWord(words));
                Thread averageLengthThread = new Thread(() => averageLength = GetAverageWordLength(words));
                Thread mostCommonWordsThread = new Thread(() => mostCommonWords = GetMostCommonWords(words, 5));
                Thread leastCommonWordsThread = new Thread(() => leastCommonWords = GetLeastCommonWords(words, 5));

                wordCountThread.Start();
                shortestWordThread.Start();
                longestWordThread.Start();
                averageLengthThread.Start();
                mostCommonWordsThread.Start();
                leastCommonWordsThread.Start();

                
                wordCountThread.Join();
                shortestWordThread.Join();
                longestWordThread.Join();
                averageLengthThread.Join();
                mostCommonWordsThread.Join();
                leastCommonWordsThread.Join();

                
                Console.WriteLine($"Number of words: {wordCount}");
                Console.WriteLine($"Shortest word: {shortestWord}");
                Console.WriteLine($"Longest word: {longestWord}");
                Console.WriteLine($"Average word length: {averageLength:F2}");
                Console.WriteLine("Five most common words:");
                foreach (var word in mostCommonWords)
                {
                    Console.WriteLine($"   {word}");
                }
                Console.WriteLine("Six least common words:");
                foreach (var word in leastCommonWords)
                {
                    Console.WriteLine($"   {word}");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }


        static string[] GetWords(string text)
        {
            
            char[] punctuationMark = { ' ', ',', '.', '!', '?', '-', ';', '/', ':', '„', '“', '\t', '\n', '\r' };
            string[] words = text.Split(punctuationMark, StringSplitOptions.RemoveEmptyEntries);

       
            List<string> validWords = new List<string>();
            foreach (var word in words)
            {
                if (word.Length >= 3)
                {
                    validWords.Add(word);
                }
            }

            return validWords.ToArray();
        }

        static int GetWordCount(string[] words)
        {
            return words.Length;
        }

        static string GetShortestWord(string[] words)
        {
            string? shortestWord = null;
            foreach (var word in words)
            {
                if (shortestWord == null || word.Length < shortestWord.Length)
                {
                    shortestWord = word;
                }
            }
            return shortestWord;
        }

        static string GetLongestWord(string[] words)
        {
            string? longestWord = null;
            foreach (var word in words)
            {
                if (longestWord == null || word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
            return longestWord;
        }

        static double GetAverageWordLength(string[] words)
        {
            if (words.Length == 0)
                return 0.0;

            int totalLength = 0;
            foreach (var word in words)
            {
                totalLength += word.Length;
            }

            return (double)totalLength / words.Length;
        }

        static string[] GetMostCommonWords(string[] words, int count)
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }

            var mostCommonWords = new string[count];
            for (int i = 0; i < count; i++)
            {
                int maxCount = 0;
                string? mostCommonWord = null;
                foreach (var kvp in wordFrequency)
                {
                    if (kvp.Value > maxCount)
                    {
                        maxCount = kvp.Value;
                        mostCommonWord = kvp.Key;
                    }
                }
                if (mostCommonWord != null)
                {
                    mostCommonWords[i] = mostCommonWord;
                    wordFrequency.Remove(mostCommonWord);
                }
            }

            return mostCommonWords;
        }

        static string[] GetLeastCommonWords(string[] words, int count)
        {
            var wordFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }

            var leastCommonWords = new string[count];
            for (int i = 0; i < count; i++)
            {
                int minCount = int.MaxValue;
                string? leastCommonWord = null;
                foreach (var kvp in wordFrequency)
                {
                    if (kvp.Value < minCount)
                    {
                        minCount = kvp.Value;
                        leastCommonWord = kvp.Key;
                    }
                }
                if (leastCommonWord != null)
                {
                    leastCommonWords[i] = leastCommonWord;
                    wordFrequency.Remove(leastCommonWord);
                }
            }

            return leastCommonWords;
        }
    }
}