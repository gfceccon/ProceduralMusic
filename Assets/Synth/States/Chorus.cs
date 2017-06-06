using UnityEngine;
using System.Collections;

public class Chorus : MusicState
{
    public Chorus(Song song)
        : base(song)
    {
        this.stateID = StateID.Chorus;
    }
    public override void Reason()
    {
    }
    public override void Act()
    {
    }
}

