using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Config))]
[RequireComponent(typeof(SynthWave))]
[RequireComponent(typeof(LineRenderer))]
public class WaveUI : MonoBehaviour {
    public int bars;
    public int subStep;

    private Config config;
    private SynthWave wave;
    private LineRenderer line;
	
	void Start ()
    {
        bars = Mathf.Max(1, bars);
        subStep = Mathf.Max(1, subStep);

        config = GetComponent<Config>();
        wave = GetComponent<SynthWave>();
        line = GetComponent<LineRenderer>();

        Draw();
	}

    public void Draw()
    {
        int coef = (int)Tempo.Beat.Sixteenth - (int)config.length;
        int points = (1 << coef) * config.size * bars * subStep;
        float time = Tempo.Time("s", config.relative, config.bpm);
        float iSubStep = 1f / subStep;
        Vector3[] positions = new Vector3[points];
        for (int i = 0; i < points; i++)
            positions[i] = new Vector3(i * time * iSubStep, wave.Synth(i * time * iSubStep));
        line.positionCount = points;
        line.SetPositions(positions);
    }
}
