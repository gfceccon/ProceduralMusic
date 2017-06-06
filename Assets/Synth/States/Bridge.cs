using UnityEngine;
using System.Collections;

public class Bridge : MusicState
{
    public Bridge(StateID type, Song song)
        : base(song)
    {
        this.stateID = type;
    }
    public override void Reason()
    {
    }
    public override void Act()
    {
    }
}

