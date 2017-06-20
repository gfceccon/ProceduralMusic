using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;


public enum ENote
{
    C = 0,
    Db,
    D,
    Eb,
    E,
    F,
    Gb,
    G,
    Ab,
    A,
    Bb,
    B
};

public class Note
{
    private int note;
    private ENote enote;
    public int octave;
    public ENote Value
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

    public void Init()
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
            return this == Other;
        }
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
