using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public enum WaveType
{
    Square,
    Sawtooth,
    Triangle,
    Noise
}

public enum DutyCycle
{
    Eighth = 1,
    Quarter,
    Half
}

public class Synth : MonoBehaviour
{
    private const int SQUARE_CHANNELS = 3;
    private Square[] square = new Square[SQUARE_CHANNELS];

    private const int TRIANGLE_CHANNELS = 1;
    private Triangle[] triangle = new Triangle[TRIANGLE_CHANNELS];

    private const int SAWTOOTH_CHANNELS = 1;
    private Sawtooth[] sawtooth = new Sawtooth[SAWTOOTH_CHANNELS];

    private const int NOISE_CHANNELS = 1;
    private Noise[] noise = new Noise[NOISE_CHANNELS];

    private bool[] usingSquare = new bool[SQUARE_CHANNELS];
    private bool[] usingTriangle = new bool[TRIANGLE_CHANNELS];
    private bool[] usingSawtooth = new bool[SAWTOOTH_CHANNELS];
    private bool[] usingNoise = new bool[NOISE_CHANNELS];

    private const float BaseDutyCycle = 0.125f;
    private const float DEFAULT_FREQ = 0.125f;
    private const float DEFAULT_AMP = 0.125f;

    private float step;

    private void Awake()
    {
        int samplerate = AudioSettings.outputSampleRate;
        step = 1f / samplerate;

        for (int ch = 0; ch < SQUARE_CHANNELS; ch++)
        {
            usingSquare[ch] = false;
            square[ch] = new Square(DEFAULT_FREQ, DEFAULT_AMP, step);
            square[ch].Step = step;
        }

        for (int ch = 0; ch < TRIANGLE_CHANNELS; ch++)
        {
            usingTriangle[ch] = false;
            triangle[ch] = new Triangle(DEFAULT_FREQ, DEFAULT_AMP, step);
            triangle[ch].Step = step;
        }

        for (int ch = 0; ch < SAWTOOTH_CHANNELS; ch++)
        {
            usingSawtooth[ch] = false;
            sawtooth[ch] = new Sawtooth(DEFAULT_FREQ, DEFAULT_AMP, step);
            sawtooth[ch].Step = step;
        }

        for (int ch = 0; ch < NOISE_CHANNELS; ch++)
        {
            usingNoise[ch] = false;
            noise[ch] = new Noise(DEFAULT_FREQ, DEFAULT_AMP, step);
            noise[ch].Step = step;
        }

        Note.Init();
    }

    public void Stop(int channel)
    {
    }

    public int Play(WaveType type, float freq, float amplitude, DutyCycle cycle = DutyCycle.Half)
    {
        int channel = -1;
        Wave wave = null;
        switch (type)
        {
            case WaveType.Square:
                for (int ch = 0; ch < SQUARE_CHANNELS; ch++)
                {
                    if (usingSquare[ch])
                        continue;
                    usingSquare[ch] = true;
                    wave = square[ch];
                    channel = ch;
                    square[ch].DutyCycle = ((int)cycle) * BaseDutyCycle;
                    break;
                }
                break;
            case WaveType.Sawtooth:
                for (int ch = 0; ch < SAWTOOTH_CHANNELS; ch++)
                {
                    if (usingSquare[ch])
                        continue;
                    usingSquare[ch] = true;
                    wave = sawtooth[ch];
                    channel = ch;
                    break;
                }
                break;
            case WaveType.Triangle:
                for (int ch = 0; ch < TRIANGLE_CHANNELS; ch++)
                {
                    if (usingSquare[ch])
                        continue;
                    usingSquare[ch] = true;
                    wave = triangle[ch];
                    channel = ch;
                    break;
                }
                break;
            case WaveType.Noise:
                for (int ch = 0; ch < NOISE_CHANNELS; ch++)
                {
                    if (usingNoise[ch])
                        continue;
                    usingNoise[ch] = true;
                    wave = noise[ch];
                    channel = ch;
                    break;
                }
                break;
            default:
                break;
        }
        if(channel != -1)
        {
            wave.Amplitude = amplitude;
            wave.Frequency = freq;
        }
        return channel;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        for (int ch = 0; ch < SQUARE_CHANNELS; ch++)
            if (usingSquare[ch])
                square[ch].Process(data, channels);

        for (int ch = 0; ch < TRIANGLE_CHANNELS; ch++)
            if (usingTriangle[ch])
                triangle[ch].Process(data, channels);

        for (int ch = 0; ch < SAWTOOTH_CHANNELS; ch++)
            if (usingSawtooth[ch])
                sawtooth[ch].Process(data, channels);

        for (int ch = 0; ch < NOISE_CHANNELS; ch++)
            if (usingNoise[ch])
                noise[ch].Process(data, channels);
    }
}
