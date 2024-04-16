namespace Foundation.Records.Exceptions;
public class Situation {
    public static readonly Situation Default = new(0, "Unset");

    public int Code { get; set; }
    public string Display { get; set; }

    public Situation(int situation, string display) {
        Code = situation;
        Display = display;
    }
}
