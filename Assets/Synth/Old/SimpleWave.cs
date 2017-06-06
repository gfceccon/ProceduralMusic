using UnityEngine;
using System;

public class SimpleWave
{
    public static float Sine(float t, float freq, float des)
    {
        return (float)Math.Sin(t * 2 * Math.PI * freq + des);
    }

    public static float Saw(float t, float freq, float des)
    {
        return (float)(1 - 2 * ((t + freq) % (1 / freq)) * freq);
    }

    public static float Square(float t, float freq, float des)
    {
        return (Math.Sin(t * 2 * Math.PI * freq + des) > 0.0 ? 1.0f : -1.0f);
    }

    public static float Triangle(float t, float freq, float des)
    {
        return (float)(Math.Asin(Math.Sin(t * 2 * Math.PI * freq + des)) / Math.PI);
    }
}
