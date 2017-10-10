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
    private ProceduralMusic procedural;
    private List<GameObject> noteObjects;

    private void Start()
    {
        config = GetComponent<Config>();
        wave = GetComponent<SynthWave>();
        procedural = GetComponent<ProceduralMusic>();
        noteObjects = new List<GameObject>();
    }

    public void Generate()
    {
        procedural.Generate();
        List<Tuple<string, int>> music = procedural.Music;
        float time = 0f;
        noteObjects.Clear();
        foreach (Tuple<string, int> note in music)
        {
            string durationStr = note.first;
            int midi = note.second;
            GameObject obj = Instantiate(notePrefab, transform);
            float duration = Tempo.Time(durationStr, config.length, config.bpm);
            obj.transform.localPosition = new Vector3(time, midi);
            obj.transform.localScale = new Vector3(duration, 1f);
            noteObjects.Add(obj);
            time += duration;
        }
    }

    public void Refresh()
    {
        List<Tuple<string, int>>  music = procedural.Music;
        float time = 0f;
        for (int i = 0; i < noteObjects.Count; i++)
        {
            string durationStr = music[i].first;
            int midi = music[i].second;
            float duration = Tempo.Time(durationStr, config.length, config.bpm);
            noteObjects[i].transform.localPosition = new Vector3(time, midi);
            time += duration;
        }
    }
}
