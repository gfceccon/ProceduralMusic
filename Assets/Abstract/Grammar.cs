using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

class Grammar<T> where T : ICloneable
{
    private const int MIN_PASSES = 1;
    private const int MAX_PASSES = 5;

    List<ProductionRule<T>> productions;
    List<RewritingnRule<T>> rewritings;

    List<T> initial;

    private Grammar() { }

    public Grammar(params T[] initial)
    {
        this.initial = new List<T>();
        this.productions = new List<ProductionRule<T>>();
        this.rewritings = new List<RewritingnRule<T>>();
        if (initial != null)
            this.initial.AddRange(initial);
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

    public LinkedList<T> Generate(int passes = 1)
    {
        LinkedList<T> list = new LinkedList<T>(initial);
        passes = Mathf.Clamp(passes, MIN_PASSES, MAX_PASSES);
        while (passes > 0)
        {
            foreach (var rule in productions)
                rule.Write(list);
            foreach (var rule in rewritings)
                rule.Write(list);
            passes--;
        }
        return list;
    }
}
