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
    List<ReductionRule<T>> reductions;
    List<RewritingnRule<T>> rewritings;

    List<T> initial;

    public Grammar(params T[] initial)
    {
        this.initial = new List<T>();
        if (initial != null)
            this.initial.AddRange(initial);
    }

    public LinkedList<T> Generate()
    {
        LinkedList<T> list = new LinkedList<T>();
        return list;
    }
}
