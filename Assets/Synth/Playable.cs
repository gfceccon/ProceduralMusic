using UnityEngine;
using System;
using System.Collections;

public enum PlayableType
{
    Undefined,
    Note,
    Chord
}

public abstract class Playable
{
    public PlayableType type = PlayableType.Undefined;
    public int instrument = 0;
    public int velocity = 100;
    public float lenght = 0.0f;
    public int channel = -1;
    public bool isPlaying = false;
    public bool isOnHalt = false;

    protected float startTime;

    private float halt;

    public virtual void Play(Synth synth)
    {
        if (isOnHalt)
        {
            startTime += (Time.realtimeSinceStartup - halt);
            isOnHalt = false;
        } else
        {
            startTime = Time.realtimeSinceStartup;
            isPlaying = true;
        }
    }

    public virtual void Halt(Synth synth)
    {
        halt = Time.realtimeSinceStartup;
        isOnHalt = true;
    }

    public virtual void Stop(Synth synth)
    {
        isPlaying = false;
    }

    public virtual void Update(Synth synth)
    {
        if (lenght == 0.0f)
            return;
        if (Time.realtimeSinceStartup - startTime > lenght)
            this.Stop(synth);
    }
}
