
public static class Utils
{
    public static string FormatPrice(int price) {
        return "$" + price.ToString("N0");
    }

    public static string HighlightText(string text, string color = "08FF00") {
        return "<color=#" + color + ">" + text + "</color>";
    }
}
