using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Wave
{
    public float Amplitude;
    public float Frequency { set { _frequency = value; Length = 1.0f / value; } get { return _frequency; } }

    protected float Counter = 0f;
    protected float Length = 0f;
    public float Step = 1f /4800f;

    private float _frequency;

    public abstract void Process(float[] data, int channels);

    public Wave(float frequency, float amplitude, float step)
    {
        Frequency = frequency;
        Amplitude = amplitude;
        Step = step;
    }

    protected void Next()
    {
        Counter += Step;
        if (Counter >= Length)
            Counter -= Length;
    }
}
