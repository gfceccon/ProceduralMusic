using UnityEngine;
using System.Collections;

public class Note : Playable
{
	public int note;

    public Note()
    {
        this.type = PlayableType.Note;
    }

	public Note(int note) : this()
	{
		this.note = note;
	}
	
	public override void Play(Synth synth)
	{
		base.Play (synth);
		channel = synth.Play(instrument, note, velocity, false, channel);
    }
    
    public override void Halt(Synth synth)
    {
        base.Halt(synth);
        synth.Stop(channel, note);
    }
	
	public override void Stop(Synth synth)
	{
		if (channel == -1)
			return;
		
		base.Stop (synth);
		synth.Stop(channel, note);
	}
}