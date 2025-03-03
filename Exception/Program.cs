void Print (string text) {
    if (text == null) {
        throw new ArgumentNullException("text is null.");
    }
    else if (text.Length < 5) {
        throw new Exception("text is too short.");
    }
    else {
        Console.WriteLine(text);
    }
}

Dictionary<string, string> dict = new() {
    {"spider","arachnid"},
    {"human", "mammal"},
    {"spiderman", "arachmal"}
};

void DictTryGet(string key) {
    if (dict.TryGetValue(key, out string? value)) {
        Print(value);
    }
    else {
        Print("Key not found.");
    }
}

DictTryGet("antman");
DictTryGet("spiderman");

void TryCatchStr(string text) {
    try {
        Print(text);
    }
    catch (ArgumentNullException err) {
        Print(err.Message);
    }
    catch (Exception err) {
        Print(err.Message);
    }
    finally {
        Print("End of trying.");
    }
}

string text = "hi";
string? nullText = null;

TryCatchStr(text);
TryCatchStr(nullText);

using (StreamReader reader = File.OpenText(@"AText.txt")) {
    string content = reader.ReadToEnd();
    Print(content);
}

void CheckAge(int age) {
    if (age < 19) {
        throw new UnderAgeException("You're a minor.");
    }
    else {
        Print("You're an adult.");
    }
}

try {
    CheckAge(17);
}
catch (UnderAgeException err) {
    Print(err.Message);
}



class UnderAgeException : Exception {
    public UnderAgeException(string message) : base(message) { }
}
