IEnumerable<int> GenerateNumbers() {
    for (int i = 0; i < 2025; i++) {
        yield return i+1;
    }
}

IEnumerable<int> numbersGenerator = GenerateNumbers();

List<int> filteredList= numbersGenerator.Where(i => (i % 2 == 0) && (Math.Pow( (int)Math.Sqrt(i), 2 ) == i)).ToList();
foreach (int i in filteredList) {
    Console.Write($"{i} ");
}

IEnumerable<int> filteredEnumerable = GenerateNumbers().Where(i => (i % 4 == 0) && (i % 100 == 0));
Console.WriteLine();
foreach (int i in filteredEnumerable) {
    Console.Write($"{i} ");
}

IEnumerator<int> enumerator = filteredEnumerable.Where(i => i % 45 == 0).GetEnumerator();
Console.WriteLine();
while (enumerator.MoveNext()) {
    Console.Write($"{enumerator.Current} ");
}

EnumerableClass enumerableObj = new();
Console.WriteLine();
foreach (int i in enumerableObj.Where(i => i%1000 == 0)) {
    Console.Write($"{i} ");
}
Console.WriteLine();
foreach (char c in enumerableObj.GetChar()) {
    Console.Write($"{c} ");
}

NotEnumerableClass notEnumerableObj = new();
Console.WriteLine();
foreach (int i in notEnumerableObj.EnumerableMethod().Where(i => i%500 == 0)) {
    Console.Write($"{i} ");
}
Console.WriteLine();
foreach (char c in notEnumerableObj.GetChar()) {
    Console.Write($"{c} ");
}

Console.WriteLine();
foreach (int i in notEnumerableObj.Fibonacci(10)) {
    Console.Write($"{i} ");
}