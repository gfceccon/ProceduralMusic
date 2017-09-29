using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

using Rand = UnityEngine.Random;

public class Tempo
{
    public enum Beat
    {
        Whole,
        Half,
        Quarter,
        Eighth,
        Sixteenth,
        Min
    }

    private Tempo() { }

    public static float Time(string value, Beat relative, float bpm)
    {
        float length;
        char note = value[0];
        switch (note)
        {
            case 'w':
                length = 1f;
                break;

            case 'h':
                length = 2f;
                break;

            default:
            case 'q':
                length = 4f;
                break;

            case 'e':
                length = 8f;
                break;

            case 's':
                length = 16f;
                break;
        }

        if (value.Contains("."))
            length += length / 2f;

        int beat = (int)relative;
        beat = 1 << beat;

        return (beat / length) * (60f / bpm);
    }
}