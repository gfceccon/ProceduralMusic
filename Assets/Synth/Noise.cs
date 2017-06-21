using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Noise : Wave
{
    public Noise(float frequency, float amplitude, float step) : base(frequency, amplitude, step) { }

    public override void Process(float[] data, int channels)
    {
    }
}
