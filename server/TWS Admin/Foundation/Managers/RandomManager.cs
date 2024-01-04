namespace Foundation.Managers;
public static class RandomManager {
    private static readonly Random Randomizer = new();
    
    public static string String(int Size) {
        char[] letters = new char[Size];
        for(int Pointing = 0; Pointing < Size; Pointing++) {
            int Code = Randomizer.Next(65, 91);
            letters[Pointing] = (char)Code;
        }

        string GeneratedString = new(letters);
        return GeneratedString;
    }
}
