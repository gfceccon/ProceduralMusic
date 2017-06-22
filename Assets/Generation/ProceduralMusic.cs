using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Synth))]
public class ProceduralMusic : MonoBehaviour
{
    private Synth synth;
    void Start()
    {
        synth = GetComponent<Synth>();
        Grammar<Note> grammar = Grammar<Note>.Create(new Note(ENote.A)).
            AddProduction(new ProductionRule<Note>().
                Input(new Note(ENote.A)).
                Output(new Note(ENote.A), new Note(ENote.A)).
                SetCondition(UseRule).
                AddPostProcess(AddProbability)).
            AddRewrite(new RewritingnRule<Note>().
                Input(new Note(ENote.A)).
                Output(new Note(ENote.A)).
                SetCondition(UseRule).
                AddPostProcess(AddProbability));

        LinkedList<Note> generated = grammar.Generate();
        foreach (var note in generated)
            print(note.Note_);
    }

    bool UseRule(LinkedListNode<Note> note)
    {
        // TODO Definir se usa regra ou nao
        return true;
    }

    void AddProbability(params LinkedListNode<Note>[] notes)
    {
        // TODO Fazer alteracoes na matriz
    }

    float time = 0.25f;

    IEnumerator Play(LinkedList<IMusicGrammar> list)
    {
        foreach (var note in list)
        {
            note.Play(synth);
            yield return new WaitForSeconds(time);
        }
    }
}
