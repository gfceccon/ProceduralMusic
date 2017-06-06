using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : MusicState
{
    public int pianoInstrument;

    private int pianoLeadChannel = -1;
    private int[] mode;
    private LinkedListNode<Playable> currentNote;

    public Intro(Song song)
        : base(song)
    {
        this.stateID = StateID.Intro;
        mode = song.currentScale.MixoLydianScale;
        for (int i = 0; i < 5; i++)
        {
            Note note = song.GetPooledNote();
            if (note != null)
            {
                note.instrument = pianoInstrument;
                note.channel = (pianoLeadChannel == -1 ? song.synth.GetAvaiableChannel() : pianoLeadChannel);
                note.lenght = Random.Range(1, 2) * song.tempo.SingleBeatDuration;
                note.note = mode[Random.Range(0, mode.Length - 1)];

                playables.AddLast(note);
            }
        }
        currentNote = playables.Last;
    }
    public override void Reason()
    {
        if (Time.realtimeSinceStartup - startTime > song.tempo.SingleBeatDuration * 20)
        {
            foreach (Playable playable in playables)
            {
                playable.Stop(song.synth);
            }
            song.SetTransition(Transition.BeginVerse);
        }
        if (!currentNote.Value.isPlaying)
        {
            if (currentNote == playables.Last)
                currentNote = playables.First;
            else
                currentNote = currentNote.Next;
            currentNote.Value.Play(song.synth);
        }
    }

    public override void Act()
    {
        currentNote.Value.Update(song.synth);
    }
}
