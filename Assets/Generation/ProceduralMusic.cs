using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

using Rand = UnityEngine.Random;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Synthesizer))]
public class ProceduralMusic : MonoBehaviour
{
    public float bpm;
    private Player player;
    private Synthesizer midi;
    void Start()
    {
        player = GetComponent<Player>();
        midi = GetComponent<Synthesizer>();

        Grammar<string> grammar = new Grammar<string>("4B").
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

        LinkedList<string> tempo = grammar.Generate(10);
        string values = "";
        foreach (string note in tempo)
            values += note + " ";
        print(values);
        StartCoroutine(Play(tempo));
    }

    bool Coin(LinkedListNode<string> bar)
    {
        return Rand.Range(0,2) == 0;
    }

    IEnumerator Play(LinkedList<string> notes)
    {
        float time = 0f;
        foreach (string note in notes)
        {
            int channel = player.Play(WaveType.Triangle, 220f, 0.1f);
            float tempo = Tempo.Time(note, Tempo.Beat.Quarter, bpm);
            yield return new WaitForSeconds(tempo);
            player.Stop(WaveType.Triangle, channel);
            time += tempo;
        }
    }
}
