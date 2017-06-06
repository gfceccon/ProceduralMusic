using UnityEngine;
using System.Collections;

public class Chord : Playable
{
    public int[] Notes
    {
        set
        {
            if (value != null)
            {
                notes = null;
                notes = value;
            }
        }
    }

	private int[] notes;

    public Chord()
    {
        this.type = PlayableType.Chord;
    }

    public Chord(params int[] notes) : this()
    {
        this.notes = notes;
    }

    public override void Play(Synth synth)
    {
		base.Play (synth);
        for (int i = 0; i < notes.Length; i++)
            channel = synth.Play(instrument, notes[i], velocity, false, channel);
    }

    public override void Halt(Synth synth)
    {
        base.Halt(synth);
        for (int i = 0; i < notes.Length; i++)
            synth.Stop(channel, notes[i]);
    }

    public override void Stop(Synth synth)
	{
		base.Stop (synth);
		if (channel == -1)
            return;
        for (int i = 0; i < notes.Length; i++)
            synth.Stop(channel, notes[i]);
    }
}