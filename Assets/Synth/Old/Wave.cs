using UnityEngine;
using System.Collections;
using System;

public abstract class Wave : MonoBehaviour
{
    protected delegate float WaveFunction(float time, float frequency, int channel);
    public virtual float Function(float t, float freq, int channel)
    {
        return SimpleWave.Sine(t, freq, deslocation);
    }

    public float frequency;
    public AnimationCurve gain;
    public float duration;
    public float amplitude = 1.0f;
    public bool play = true;
    public float deslocation = 0.0f;

    protected WaveFunction function;
    protected float sampleRate = 48000f;

    private float increment = 0.0f;
    private float phase = 0.0f;
    private float startTime;
    private float volume;


    void OnAudioFilterRead(float[] data, int channels)
    {
        if(!play)
            return;

        if (function == null)
            return;

        if (duration == 0.0f)
            volume = amplitude;
        else
            volume = amplitude * gain.Evaluate(phase/duration);
        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase += increment;
            for (var channel = 0; channel < channels; channel++)
                 data[i + channel] += volume * function(phase, frequency, channel);
        }
    }

    public void Update()
    {
        if (Time.realtimeSinceStartup - startTime + 0.025f > duration && duration > 0.0f)
            play = false;
    }

    public void Play()
    {
        startTime = Time.realtimeSinceStartup;
        phase = 0.0f;
        play = true;
    }
    public void Start()
    {
        function = Function;
        sampleRate = AudioSettings.outputSampleRate;
        increment = 1 / sampleRate;
        startTime = Time.realtimeSinceStartup;
    }
}