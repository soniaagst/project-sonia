public class NumberPairing { //pairs numbers that sum to 10
    public static void Run() {
        List<int> numbers = [1,2,3,4,5,6,7,8,9];
        int numbersSum = 10;
        for (int i = 0; i < numbers.Count; i++) {
            for (int j = i + 1; j < numbers.Count; j++) {
                if (numbers[i] + numbers[j] == numbersSum) {
                    Console.WriteLine(numbers[i] + ", " + numbers[j]);
                }
            }
        }
    }
}