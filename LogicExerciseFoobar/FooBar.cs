using System.Text;

public class FooBar {
    public static void RunVer1(int maxnumber) { // versi 1: langsung print
        for (int num = 1; num <= maxnumber; num++) {
            if (num % 15 == 0) {
                Console.Write("foobar");
            }
            else if (num % 3 == 0) {
                Console.Write("foo");
            }
            else if (num % 5 == 0) {
                Console.Write("bar");
            }
            else {
                Console.Write(num);
            }
            if (num < maxnumber) {
                Console.Write(", ");
            }
        }
    }
    
    public static void RunVer2(int maxnumber) { // versi 2: disimpan di list dulu baru diprint

        List<object> objectList = [];

        for (int num = 1; num <= maxnumber; num++) {
            if (num % 3 == 0 && num % 5 == 0) {
                objectList.Add("Marcopolo");
            }
            else if (num % 3 == 0) {
                objectList.Add("Marco");
            }
            else if (num % 5 == 0) {
                objectList.Add("Polo");
            }
            else {
                objectList.Add(num);
            }
        }

        for (int i = 0; i < objectList.Count; i++) {
            Console.Write(objectList[i]);
            if (i < objectList.Count-1) {
                Console.Write(", ");
            }
        }
    }

    public static void RunVer3(int maxnumber) { // versi 3: menggunakan dictionary untuk mengaitkan angka dan kata2
        Dictionary<int, string> words = new() {
            {3, "foo"},
            {5, "bar"},
            {15,"foobar"}
        };
        for (int num = 1; num <= maxnumber; num++) {
            bool divisible = false;
            foreach (int key in words.Keys.Reverse()) {
                if (num % key == 0) {
                    Console.Write(words[key]);
                    divisible = true;
                    break;
                }
            }
            if (divisible == false) {
                Console.Write(num);
            }
            if (num < maxnumber) {
                Console.Write(", ");
            }
        }
    }

    public static void Foobarjazz(int maxnumber) {
        Dictionary<int, string> words = new() {
            {3, "foo"},
            {5, "bar"},
            {7,"jazzz"}
        };

        for (int num = 1; num <= maxnumber; num++) {
            bool divisible = false;
            foreach (int key in words.Keys) {
                if (num % key == 0) {
                    Console.Write(words[key]);
                    divisible = true;
                }
            }
            if (divisible == false) {
                Console.Write(num);
            }
            if (num < maxnumber) {
                Console.Write(", ");
            }
        }
    }

    public static IEnumerable<object> Foobarjazz2(int maxnumber) {
        Dictionary<int, string> words = new() {
            {3, "foo"},
            {5, "bar"},
            {7,"jazzz"}
        };

        for(int num = 1; num <= maxnumber; num++) {
            bool divisible = false;
            StringBuilder word = new StringBuilder();
            foreach (int key in words.Keys) {
                if (num % key == 0) {
                    word.Append(words[key]);
                    divisible = true;
                }
            }
            
            if (divisible) yield return word;
            else yield return num;
        }
    }

    public static void Bazhuzz(int maxnumber) {
        Dictionary<int, string> words = new() {
            {3, "foo"},
            {4, "baz"},
            {5, "bar"},
            {7, "jazz"},
            {9, "huzz"}
        };

        for (int num = 1; num <= maxnumber; num++) {
            bool divisible = false;
            foreach (int key in words.Keys) {
                if (num % key == 0) {
                    Console.Write(words[key]);
                    divisible = true;
                }
            }
            if (divisible == false) {
                Console.Write(num);
            }
            if (num < maxnumber) {
                Console.Write(", ");
            }
        }
    }
}