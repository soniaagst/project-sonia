// The birthday paradox says that if there is a group of 30 people,
// there's 50% chance that a person shares the same birthday with someone in the group (which apparently not true).

// This is to show how many people have actually share birthdays,
// and only show the dates that have more than 1 person.

public static class BirthdayParadox{
    public static List<string> GenerateRandomBirthdays(int numberofPeople) {
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

    public static Dictionary<string, int> FindSameDates(List<string> birthdays) {
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
        foreach (var item in sameBirthdays) {
            Console.WriteLine($"{item.Key} | {item.Value}");
        }
    }
}