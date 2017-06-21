using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

class Grammar<T>
{
    List<ProductionRule<T>> productions;
    List<RewritingnRule<T>> rewritings;

    List<T> initial;

    private Grammar() { }

    public static Grammar<T> Create(params T[] initial)
    {
        Grammar<T> grammar = new Grammar<T>();
        grammar.initial = new List<T>();
        if (initial != null)
            grammar.initial.AddRange(initial);
        return grammar;
    }

    public Grammar<T> AddProduction(Rule<T> prod)
    {
        productions.Add((ProductionRule<T>)prod);
        return this;
    }

    public Grammar<T> AddRewrite(Rule<T> prod)
    {
        rewritings.Add((RewritingnRule<T>)prod);
        return this;
    }

    public LinkedList<T> Generate()
    {
        LinkedList<T> list = new LinkedList<T>();
        foreach (var rule in productions)
            rule.Write(list);
        foreach (var rule in rewritings)
            rule.Write(list);
        return list;
    }
}
