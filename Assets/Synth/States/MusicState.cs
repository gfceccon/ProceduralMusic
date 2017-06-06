using UnityEngine;
using System.Collections.Generic;

public abstract class MusicState : FSMState 
{
    protected Song song;
    protected float startTime;
    protected LinkedList<Playable> playables;

    public MusicState(Song song)
    {
        this.playables = new LinkedList<Playable>();
        this.song = song;
    }

    public override void DoBeforeEntering()
    {
        this.startTime = Time.realtimeSinceStartup;
    }

    public override void DoBeforeLeaving()
    {
        foreach (var playable in playables)
        {
            if (playable.isPlaying == true)
                playable.Stop(song.synth);
        }
        song.ReturnAllToPool(playables);
    }
}
