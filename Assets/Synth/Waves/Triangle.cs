using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Triangle : Wave
{
    public Triangle(float frequency, float amplitude, float step) : base(frequency, amplitude, step) { }

    public override void Process(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (Counter < Length * 0.5f)
                data[i] += 4 * Counter * Amplitude * Frequency - Amplitude;
            else
                data[i] += -4 * (Counter - Length) * Amplitude * Frequency - Amplitude;
            Next();
        }
    }
}
