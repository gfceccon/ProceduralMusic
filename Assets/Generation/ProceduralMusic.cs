using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

using Rand = UnityEngine.Random;
using System.Linq;

[RequireComponent(typeof(Config))]
[RequireComponent(typeof(SynthWave))]
[RequireComponent(typeof(SynthPlayer))]
public class ProceduralMusic : MonoBehaviour
{
    private Config config;
    private SynthWave wave;
    private SynthPlayer player;

    private Grammar<string> grammar;

    private List<float> scale = new List<float> { 0, 2, 3, 5, 7, 8, 10 };


    void Start()
    {
        config = GetComponent<Config>();
        wave = GetComponent<SynthWave>();
        player = GetComponent<SynthPlayer>();

        grammar = new Grammar<string>("4B", "4B").
            AddProduction(new ProductionRule<string>().
                Input("4B").
                Output("2B", "2B").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("4B").
                Output("2B", "1B", "1B").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("4B").
                Output("1B", "2B", "1B").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("2B").
                Output("h", "q.", "h", "e", "e", "e", "q").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("2B").
                Output("h.", "q", "e", "e", "q", "h").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("1B").
                Output("h", "q", "q").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("1B").
                Output("q", "q", "q", "q").
                SetCondition(Coin)).
            AddProduction(new ProductionRule<string>().
                Input("1B").
                Output("h.", "q").
                SetCondition(Coin));
    }

    bool Coin(LinkedListNode<string> bar)
    {
        return Rand.Range(0,2) == 0;
    }

    public List<Tuple<string, int>> Generate()
    {
        LinkedList<string> tempo = grammar.Generate(10);
        List<Tuple<string, int>> music = new List<Tuple<string, int>>();

        float time = 0f;
        foreach (string length in tempo)
        {
            float midi = wave.Synth(time);
            int oct = (int)(midi / 12f);
            float note = midi % 12;
            float closest = scale.OrderBy(item => Math.Abs(note - item)).First();
            midi = oct * 12 + closest;
            time += Tempo.Time(length, config.noteLength, config.bpm);

            music.Add(new Tuple<string, int>(length, (int)midi));
        }

        StartCoroutine(Play(tempo));
        return music;
    }

    IEnumerator Play(LinkedList<string> notes)
    {
        Note.Init();
        float time = 0f;

        
        foreach (string length in notes)
        {
            float midi = wave.Synth(time);
            int oct = (int)(midi / 12f);
            float note = midi % 12;
            float closest = scale.OrderBy(item => Math.Abs(note - item)).First();
            midi = oct * 12 + closest;

            float freq = Note.MidiToFreq((int)midi);
            int channel = player.Play(WaveType.Square, freq, 0.1f);
            float tempo = Tempo.Time(length, config.noteLength, config.bpm);
            yield return new WaitForSeconds(tempo);
            player.Stop(WaveType.Square, channel);
            time += tempo;
        }
    }
}
