using UnityEngine;
using System;

public class SawWave : Wave
{

    public override float Function(float t, float freq, int channel)
    {
        return SimpleWave.Saw(t, freq, deslocation);
    }
}
