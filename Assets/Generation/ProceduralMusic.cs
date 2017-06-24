using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

using Rand = UnityEngine.Random;

[RequireComponent(typeof(Synth))]
public class ProceduralMusic : MonoBehaviour
{
    private Synth synth;
    void Start()
    {
        synth = GetComponent<Synth>();
        Grammar<Note> grammar = new Grammar<Note>(new Note(ENote.A), new Note(ENote.C), new Note(ENote.E), new Note(ENote.G)).
            AddProduction(new ProductionRule<Note>().
                Input(new Note(ENote.A)).
                Output(new Note(ENote.A), new Note(ENote.D)).
                SetCondition(UseRule).
                AddPostProcess(AddProbability)).
            AddProduction(new ProductionRule<Note>().
                Input(new Note(ENote.E)).
                Output(new Note(ENote.E), new Note(ENote.A)).
                SetCondition(UseRule).
                AddPostProcess(AddProbability)).
            AddRewrite(new RewritingnRule<Note>().
                Input(new Note(ENote.A)).
                Output(new Note(ENote.G)).
                SetCondition(UseRule).
                AddPostProcess(AddProbability));

        LinkedList<Note> generated = grammar.Generate(10);
        foreach (var note in generated)
            print(note.Note_);
        StartCoroutine(Play(generated));
    }

    bool UseRule(LinkedListNode<Note> note)
    {
        // TODO Definir se usa regra ou nao
        return Rand.Range(0,2) == 0;
    }

    void AddProbability(params LinkedListNode<Note>[] notes)
    {
        // TODO Fazer alteracoes na matriz
    }

    float time = 0.25f;

    IEnumerator Play(LinkedList<Note> notes)
    {
        foreach (var note in notes)
        {
            print("Play " + note.Note_);
            note.Play(synth, WaveType.Square, 0.2f, DutyCycle.Eighth);
            yield return new WaitForSeconds(time);
            note.Stop(synth);
            print("Stop " + note.Note_);
        }
    }
}
