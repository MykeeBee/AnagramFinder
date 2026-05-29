namespace AnagramFinder
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("What length anagrams are you interested in?");
            Console.WriteLine("Enter '4', '5', '6', '7' to pick an anagram length or anything else to exit");

            string input = Console.ReadLine();
            bool isValidNumber = int.TryParse(input, out int anagramLength);

            if (isValidNumber && (anagramLength == 4 || anagramLength == 5 || anagramLength == 6 || anagramLength == 7))
            {
                string filePath = $@"..\..\..\{anagramLength}-letter-words.txt";

                if (File.Exists(filePath))
                {
                    var wordListFile = File.ReadAllLines(filePath);
                    var rawWordList = new List<string>(wordListFile);
                    var anagramGroups = new Dictionary<string, List<string>>();

                    foreach (var word in rawWordList)
                    {
                        char[] charArray = word.ToCharArray();
                        Array.Sort(charArray);
                        var sortedKey = new string(word.OrderBy(c => c).ToArray());

                        if (anagramGroups.ContainsKey(sortedKey))
                        {
                            anagramGroups[sortedKey].Add(word);
                        }
                        else
                        {
                            anagramGroups[sortedKey] = new List<string> { word };
                        }
                    }

                    foreach (var anagramGroup in anagramGroups)
                    {
                        if (anagramGroup.Value.Count == 1)
                        {
                            anagramGroups.Remove(anagramGroup.Key);
                        }
                    }

                    var sortedGroups = anagramGroups.OrderByDescending(x => x.Value.Count);

                    using StreamWriter writer = new(@"..\..\..\anagram_results.txt");

                    foreach (var anagramGroup in sortedGroups)
                    {
                        writer.WriteLine($"{anagramGroup.Key}: {string.Join(", ", anagramGroup.Value)}");
                    }
                }

                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            
            else
            {
                Console.WriteLine("Exiting program...");
            }
        }
    }
}