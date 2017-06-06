using UnityEngine;
using System;

public class Piano : Wave
{
    public float attackLenght = 0.02f;
    public double attackPower = 0.5;
    public float decay = 1.0f;

    private double dampen;
    private float output;

    public override float Function(float t, float freq, int channel)
    {
        dampen = Math.Pow(0.5 * Math.Log((frequency * amplitude) / sampleRate), 2);
        dampen = Math.Pow(attackPower * Math.Log((frequency * amplitude) / sampleRate), 2);
        deslocation = (float)(Math.Pow(SimpleWave.Sine(t, freq, 0.0f), 2) +
            0.75 * SimpleWave.Sine(t, freq, 0.25f) +
            0.1 * SimpleWave.Sine(t, freq, 0.5f));

        output = (float)SimpleWave.Sine(t, freq, deslocation);

        if (t < attackLenght)
            return output * t / attackLenght;
        if (t < attackLenght)
            return output * t / attackLenght;
        else
            return output * ((float)Math.Pow((1 - decay * ((t - attackLenght) / (duration - attackLenght))), dampen));
    }
}
