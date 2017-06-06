using UnityEngine;
using System;

public class SineWave : Wave
{

    public override float Function(float t, float freq, int channel)
    {
        return SimpleWave.Sine(t, freq, deslocation);
    }
}
