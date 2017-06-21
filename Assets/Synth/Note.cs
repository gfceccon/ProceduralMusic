using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;


public class Note : IMusicGrammar
{
    int channel;
    private int note;
    private ENote enote;
    public int octave;
    public int Octave
    {
        set { octave = value; }
        get { return octave; }
    }
    public ENote Note_
    {
        set
        {
            note = ((int)value);
            enote = value;
        }
        get
        {
            return enote;
        }
    }
    public const int OCTAVE_NOTES = 12;
    public const int MIDI_NOTES = 10;
    private static float[] frequencies = new float[OCTAVE_NOTES * MIDI_NOTES];

    public Note(ENote note = ENote.C, int octave = 4)
    {
        this.note = ((int)note) + octave * OCTAVE_NOTES;
        this.enote = note;
        this.octave = octave;
    }

    public static void Init()
    {
        for(int i = 0; i < frequencies.Length; i++)
            frequencies[i] =  440f * Mathf.Pow(2f, ((i - 69) / 12f));

    }

    public float ToFreq()
    {
        return frequencies[note];
    }

    public static bool operator ==(Note This, Note Other)
    {
        return This.note == Other.note;
    }

    public static bool operator !=(Note This, Note Other)
    {
        return This.note != Other.note;
    }

    public override bool Equals(object other)
    {
        if (other.GetType() == typeof(Note))
        {
            Note Other = ((Note)other);
            return this.enote == Other.enote;
        }
        else
            return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void Play(Synth synth)
    {
        channel = synth.Play(WaveType.Square, ToFreq(), 0.2f);
    }

    public void Stop(Synth synth)
    {
        synth.Stop(channel);
    }

    public object Clone()
    {
        return new Note(enote, octave);
    }
}
