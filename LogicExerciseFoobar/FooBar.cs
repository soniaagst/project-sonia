public class FooBar
{
    private SortedDictionary<int, string> _words = new() {
        {3, "foo"},
        {4, "baz"},
        {5, "bar"},
        {7, "jazz"},
        {9, "huzz"}
    };

    public void AddRule(int number, string word)
    {
        _words.Add(number, word);
    }

    public void EditRule(int number, string newWord)
    {
        _words[number] = newWord;
    }

    public void RemoveRule(int number)
    {
        _words.Remove(number);
    }

    public List<string> ShowRules()
    {
        List<string> rules = [];
        foreach (var item in _words)
        {
            rules.Add($"{item.Key} : {item.Value}");
        }
        return rules;
    }

    public void Run(int maxNumber = 132)
    {
        for (int num = 1; num <= maxNumber; num++)
        {
            bool divisible = false;
            foreach (int key in _words.Keys)
            {
                if (num % key == 0)
                {
                    Console.Write(_words[key]);
                    divisible = true;
                }
            }
            if (divisible == false)
            {
                Console.Write(num);
            }
            if (num < maxNumber)
            {
                Console.Write(", ");
            }
        }
    }
}