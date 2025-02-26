// The birthday paradox (birthday problem) says that if there is a group of 23 people,
// there's at least 50% chance that the group has people with shared birthday.

// This is to show how many people have actually share birthdays,
// and only show the dates that have more than 1 person.

public static class BirthdayParadox{
    private static List<string> GenerateRandomBirthdays(int numberofPeople) {
        List<string> birthdays = [];
        Random random = new();
        for (int n = 0; n < numberofPeople; n++) {
            int daysAgo = random.Next(0,366);
            DateTime daysagoDate = DateTime.Now.AddDays(-daysAgo);
            string birthday = daysagoDate.ToString("dd/MM");
            birthdays.Add(birthday);
        }
        return birthdays;
    }

    private static Dictionary<string, int> FindSameDates(List<string> birthdays) {
        Dictionary<string, int> sameBirthdays = new();
        for (int i = 0; i < birthdays.Count; i++) {
            for (int j = i+1; j < birthdays.Count; j++) {
                if (birthdays[i] == birthdays[j]) {
                    if (sameBirthdays.ContainsKey(birthdays[i])){
                        sameBirthdays[birthdays[i]] += 1;
                    }
                    else {
                        sameBirthdays.Add(birthdays[i], 2);
                    }
                }
            }
        }
        return sameBirthdays;
    }

    public static void DisplaySharedBirthdays(int numberofPeople) {
        List<string> birthdays = GenerateRandomBirthdays(numberofPeople);
        Dictionary<string, int> sameBirthdays = FindSameDates(birthdays);
        Console.WriteLine($"Number of People in a group of {numberofPeople} with Shared Birthdays");
        Console.WriteLine("Date  | People");
        if (sameBirthdays.Count == 0) {
            Console.WriteLine("None");
        }
        else {
            foreach (var item in sameBirthdays) {
                Console.WriteLine($"{item.Key} | {item.Value}");
            }
        }
    }
}