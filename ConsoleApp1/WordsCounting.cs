namespace SimpleLogics{
    public static class WordsCounting {
        public static void Run (string text) {
            Dictionary<string, int> wordCount = new();
            List<string> Wordss = new(text.Split(' '));
            foreach (string word in Wordss) {
                if (wordCount.ContainsKey(word)) {
                    wordCount[word]++;
                }
                else {
                    wordCount.Add(word, 1);
                }
            }
            Console.WriteLine("Numbers of words in the text");
            foreach (var item in wordCount) {
                Console.WriteLine($"{item.Key} = {item.Value}");
            }
        }
    }
}