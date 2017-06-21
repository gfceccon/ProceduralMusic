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

    private float step;

    private void Start()
    {
        int samplerate = AudioSettings.outputSampleRate;
        step = 1f / samplerate;

        for (int ch = 0; ch < SQUARE_CHANNELS; ch++)
        {
            usingSquare[ch] = false;
            square[ch] = new Square();
            square[ch].Step = step;
        }

        for (int ch = 0; ch < TRIANGLE_CHANNELS; ch++)
        {
            usingTriangle[ch] = false;
            triangle[ch] = new Triangle();
            triangle[ch].Step = step;
        }

        for (int ch = 0; ch < SAWTOOTH_CHANNELS; ch++)
        {
            usingSawtooth[ch] = false;
            sawtooth[ch] = new Sawtooth();
            sawtooth[ch].Step = step;
        }

        for (int ch = 0; ch < NOISE_CHANNELS; ch++)
        {
            usingNoise[ch] = false;
            noise[ch] = new Noise();
            noise[ch].Step = step;
        }
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
