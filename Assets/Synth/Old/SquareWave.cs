using UnityEngine;
using System;

public class SquareWave : Wave
{

    public override float Function(float t, float freq, int channel)
    {
        return SimpleWave.Square(t, freq, deslocation);
    }
}
