using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class ProceduralMusic : MonoBehaviour
{
    void Start()
    {
        Grammar<ENote> grammar = Grammar<ENote>.Create(ENote.A).
            AddProduction(new ProductionRule<ENote>().
                Input(ENote.C).
                Output(ENote.D, ENote.A).
                SetCondition(UseRule).
                AddPostProcess(AddProbability)).
            AddRewrite(new RewritingnRule<ENote>().
                Input(ENote.A).Output(ENote.F).
                SetCondition(UseRule).
                AddPostProcess(AddProbability));

        LinkedList<ENote> generated = grammar.Generate();
        StartCoroutine(Play(generated));
    }

    bool UseRule(LinkedListNode<ENote> note)
    {
        // TODO Definir se usa regra ou nao
        return true;
    }

    void AddProbability(params LinkedListNode<ENote>[] notes)
    {
        // TODO Fazer alteracoes na matriz
    }

    float time = 0.25f;

    IEnumerator Play(LinkedList<ENote> notes)
    {
        foreach (var enote in notes)
        {
            Note note = new Note(enote);
            yield return new WaitForSeconds(time);
        }
    }
}
