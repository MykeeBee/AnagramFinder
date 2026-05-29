namespace AnagramFinder
{
    class Program
    {
        static void Main()
        {            
            var wordListFile = File.ReadAllLines(@"..\..\..\4-letter-words-processed.txt");
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
    }
}