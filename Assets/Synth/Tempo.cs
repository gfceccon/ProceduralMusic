using UnityEngine;
using System.Collections;

public class Tempo
{
    public int beatSize = 4;
    public float beatLength = 0.25f;
    public int bpm = 120;

    public float SingleBeatDuration { get { return 60f / bpm; } }
    public float HalfBeatDuration { get { return 30f / bpm; } }
    public float QuarterBeatDuration { get { return 15f / bpm; } }
}
