public class NumberPairing {
    public static void Run(List<int> numbers, int numbersSum) {
        Console.WriteLine($"Pairs of numbers that sum to {numbersSum}");
        for (int i = 0; i < numbers.Count; i++) {
            for (int j = i + 1; j < numbers.Count; j++) {
                if (numbers[i] + numbers[j] == numbersSum) {
                    Console.WriteLine(numbers[i] + ", " + numbers[j]);
                }
            }
        }
    }
}