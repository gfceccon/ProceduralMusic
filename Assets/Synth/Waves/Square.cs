using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Square : Wave
{
    public float DutyCycle = 0.5f;

    public Square(float frequency, float amplitude, float step) : base(frequency, amplitude, step) { }

    public override void Process(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (Counter < DutyCycle * Length)
                data[i] += Amplitude * 0.5f;
            else
                data[i] -= Amplitude * 0.5f;
            Next();
        }
    }
}
