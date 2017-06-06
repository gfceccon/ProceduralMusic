using UnityEngine;
using System;

public class TriangleWave : Wave
{

    public override float Function(float t, float freq, int channel)
    {
        return SimpleWave.Triangle(t, freq, deslocation);
    }

}
