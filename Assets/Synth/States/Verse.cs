using UnityEngine;
using System.Collections.Generic;

public class Verse : MusicState
{
    public int pianoInstrument;

    private int pianoLeadChannel = -1;
    private int[] mode;
    private LinkedListNode<Playable> currentNote;

    private const int MIN_CHORDS_GROUP = 1;
    private const int MAX_CHORDS_GROUP = 3;
    private const int CHORDS_MULTIPLIER = 2;

    public Verse(Song song)
        : base(song)
    {
        this.stateID = StateID.Verse;
        mode = song.currentScale.GetScaleInRange(Mode.Locrian, 0, 3);
        int[][] chords = GenerateChords();
        for (int i = 0; i < chords.Length; i++)
        {
            Chord chord = song.GetPooledChord();
            if (chord != null)
            {
                chord.Notes = chords[i];
                chord.lenght = Random.Range(1, 2) * song.tempo.SingleBeatDuration;
                chord.instrument = pianoInstrument;
                chord.channel = (pianoLeadChannel == -1 ? song.synth.GetAvaiableChannel() : pianoLeadChannel);

                playables.AddLast(chord);
            }
        }
        currentNote = playables.Last;
    }

    public int[][] GenerateChords()
    {
        int numberOfChords = Random.Range(MIN_CHORDS_GROUP, MAX_CHORDS_GROUP) * CHORDS_MULTIPLIER;
        int[][] chords = new int[numberOfChords][];
        int scaleIndex = 0;
        Scale auxiliarScale = new Scale(song.currentScale.fundamentalNote);
        for (int i = 0; i < chords.Length; i++)
        {
            chords[i] = auxiliarScale.MinorChord;
            scaleIndex = Random.Range(0, 6);
        }
        return chords;
    }

    public override void Reason()
    {
        if(!currentNote.Value.isPlaying)
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
        foreach (Playable playable in playables)
        {
            playable.Update(song.synth);
        }
    }
}
