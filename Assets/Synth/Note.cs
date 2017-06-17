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
            return ((ENote)note);
        }
    }
    public const int OCTAVE_NOTES = 12;
    public const int MIDI_NOTES = 10;
    private static float[] frequencies = new float[OCTAVE_NOTES * MIDI_NOTES];
    private static bool init = false;

    public Note(ENote note = ENote.C, int octave = 4)
    {
        this.note = ((int)note);
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
        return frequencies[(int)note + octave * OCTAVE_NOTES];
    }
}
