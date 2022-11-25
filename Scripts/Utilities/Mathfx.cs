using UnityEngine;

// I built a little math library when I needed a more customized linear interpolation, an ease
// Lots of things can be added, it's very helpful for handling complex formulas

namespace Mathfx
{

    // The following link is very helpful for visualizing each ease, the formulas can be found online
    // https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/easing-functions?view=netframeworkdesktop-4.8
    public static class Ease
    {
        public static float ExponentialInOut(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
            value--;
            return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
        }

        public static float CubicIn(float start, float end, float value)
        {
            end -= start;
            return end * value * value * value + start;
        }

        public static float CircleInOut(float start, float end, float value)
        {
            value /= .5f;
            end -= start;
            if (value < 1) return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
            value -= 2;
            return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
        }
    }
}
