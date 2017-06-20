using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class RewritingnRule<T> : Rule<T>
{
    public T input;
    public T output;

    public RewritingnRule<T> Input(T input)
    {
        this.input = input;
        return this;
    }

    public RewritingnRule<T> Output(T output)
    {
        this.output = output;
        return this;
    }

    public override LinkedList<T> Write(LinkedList<T> list)
    {
        if (list == null || list.Count == 0)
            return list;
        LinkedListNode<T> node = list.First;

        while(node != null)
        {
            if(node.Value.Equals(input) && Apply())
               node.Value = output;
        }
        return list;
    }
}
