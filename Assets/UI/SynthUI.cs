using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Config))]
[RequireComponent(typeof(SynthWave))]
[RequireComponent(typeof(LineRenderer))]
public class SynthUI : MonoBehaviour {
    public int bars;

    private Config config;
    private SynthWave wave;
    private LineRenderer line;
	
	void Start ()
    {
        bars = Mathf.Max(0, bars);

        config = GetComponent<Config>();
        wave = GetComponent<SynthWave>();
        line = GetComponent<LineRenderer>();

        int coef = ((int)Tempo.Beat.Min - (int)config.noteLength - 1);
        int points = (1 << coef) * config.barLength * bars;
        float time = Tempo.Time("s", config.noteLength, config.bpm);
        Vector3[] positions = new Vector3[points];
        for (int i = 0; i < points; i++)
            positions[i] = new Vector3(i * time, wave.Synth(i * time));
        line.positionCount = points;
        line.SetPositions(positions);
	}
}
