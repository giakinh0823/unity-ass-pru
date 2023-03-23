using UnityEngine;

namespace Constant
{
    public class ColorBackgroundConstant
    {
        public static readonly string[] ColorBackground = new string[]
        {
            "#3F3F3F",
            "#BC2A53",
            "#019D82",
            "#009852",
            "#EC7700",
            "#F500FF",
            "#EF2A34"
        };

        public static Color GetRandomColor()
        {
            // random color
            int rand = Random.Range(0, ColorBackground.Length);
            ColorUtility.TryParseHtmlString(ColorBackground[rand], out var color);
            return color;
        }
    }
}