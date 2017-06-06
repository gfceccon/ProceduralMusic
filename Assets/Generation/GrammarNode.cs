using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class GrammarNode<T>
{
    public Dictionary<Rule<T>, Tuple<float, GrammarNode<T>>> children;

    public GrammarNode()
    {
        this.children = new Dictionary<Rule<T>, Tuple<float, GrammarNode<T>>>();
    }

    public Tuple<float, GrammarNode<T>> AddRule(Rule<T> rule)
    {
        Tuple<float, GrammarNode<T>> tuple = new Tuple<float, GrammarNode<T>>(0.0f, new GrammarNode<T>());
        this.children.Add(rule, tuple);
        tuple.value = 1f;
        return tuple;
    }

}
