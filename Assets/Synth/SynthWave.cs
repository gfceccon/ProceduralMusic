using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthWave : MonoBehaviour
{
    public int displacement;
    public Oscilator[] master;

    [System.Serializable]
    public class Oscilator
    {
        public float amplitude;
        public float frequency;
        public float phase;
        public Oscilator[] modulators;
    }

    public float Synth(float time)
    {
        float value = displacement;
        foreach (Oscilator osc in master)
            value += FmSytnh(osc, time);
        return value;
    }

    private float FmSytnh(Oscilator oscilator, float time)
    {
        float frequency = oscilator.frequency;
        if(oscilator.modulators != null)
            foreach (Oscilator modulator in oscilator.modulators)
                frequency += FmSytnh(modulator, time);
        return oscilator.amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time + oscilator.phase);
    }
}
