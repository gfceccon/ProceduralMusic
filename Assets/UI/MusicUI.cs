using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Config))]
[RequireComponent(typeof(SynthWave))]
public class MusicUI : MonoBehaviour
{
    public GameObject notePrefab;

    private Config config;
    private SynthWave wave;

    void Start()
    {
        config = GetComponent<Config>();
        wave = GetComponent<SynthWave>();

        List<Tuple<string, int>> music = GetComponent<ProceduralMusic>().Generate();
        float time = 0f;
        float duration;
        GameObject obj;
        foreach (Tuple<string, int> note in music)
        {
            obj = Instantiate<GameObject>(notePrefab, transform);
            duration = Tempo.Time(note.first, config.noteLength, config.bpm);
            obj.transform.localPosition = new Vector3(time, note.second);
            obj.transform.localScale = new Vector3(duration, 1f);
            time += duration;
        }
    }
}
