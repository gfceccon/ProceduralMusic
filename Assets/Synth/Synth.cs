using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

[RequireComponent(typeof(AudioListener))]
public class Synth : MonoBehaviour
{
    public string bankFilePath = "CSharpSynth/GM Bank/skirim";
    public float gain = 1.0f;

    private float[] sampleBuffer;
    private int sampleRate = 44100;
    private int bufferSize;
	private StreamSynthesizer midiStreamSynthesizer;
    private bool[] usedChannels;
    private const int MAX_POLY = 40;
	void Awake ()
	{
        int numberOfBuffers;
        AudioSettings.GetDSPBufferSize(out bufferSize, out numberOfBuffers);
        AudioSettings.outputSampleRate = sampleRate;
        midiStreamSynthesizer = new StreamSynthesizer(sampleRate, 2, bufferSize, MAX_POLY);
		sampleBuffer = new float[midiStreamSynthesizer.BufferSize];
		usedChannels = new bool[MAX_POLY];
		midiStreamSynthesizer.LoadBank (bankFilePath);
	}
    private void OnAudioFilterRead(float[] data, int channels)
    {
        midiStreamSynthesizer.GetNext(sampleBuffer);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = sampleBuffer[i] * gain;
        }
    }


    public int GetFirstInstrumentIndex(string name)
    {
        int index = 0;
        foreach (var instrument in midiStreamSynthesizer.SoundBank.getInstruments(false))
        {
            if (instrument.Name.Equals(name))
                return index;
            index++;
        }
        return 0;
    }

    public int GetAvaiableChannel()
    {
        for (int i = 0; i < usedChannels.Length; i++)
        {
            if (usedChannels[i] == false)
            {
                usedChannels[i] = true;
                return i;
            }
        }
        return -1;
    }

    public void ReleaseChannel(int channel)
    {
        if(channel > 0 && channel < MAX_POLY)
            usedChannels[channel] = false;
    }

    public int Play(int instrumentIndex, int note, int velocity, bool drums, int channel = -1)
    {
        if(channel == -1)
            channel = GetAvaiableChannel();
        midiStreamSynthesizer.NoteOn(channel, note, velocity, instrumentIndex, drums);
        return channel;
    }
    public void Stop(int channel, int note)
    {
        midiStreamSynthesizer.NoteOff(channel, note);
    }
}
