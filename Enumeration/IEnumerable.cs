using System.Collections;

public class EnumerableClass : IEnumerable<int> {
    public IEnumerator<int> GetEnumerator() {
        for (int i = 0; i < 2025; i++) {
            yield return i+1;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public IEnumerable<char> GetChar () {
        string text = "TOUCHSOMEGRASS!";
        foreach (char c in text) {
            yield return c;
        }
    }
}

public class NotEnumerableClass {
    public IEnumerable<int> EnumerableMethod() {
        for (int i = 0; i < 2025; i++) {
            yield return i+1;
        }
    }

    public IEnumerable<char> GetChar () {
        string text = "GETSOMESUN!";
        foreach (char c in text) {
            yield return c;
        }
    }

    public IEnumerable<int> Fibonacci(int n) {
        int a = 0; int b = 1;
        for (int i = 0; i < n; i++) {
            yield return a;
            int next = a + b;
            a = b;
            b = next;
        }
    }
}
