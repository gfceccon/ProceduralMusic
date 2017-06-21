using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class ProductionRule<T> : Rule<T>
{
    private T input;
    private List<T> output;

    public ProductionRule<T> Input(T input)
    {
        this.input = input;
        return this;
    }

    public ProductionRule<T> Output(params T[] output)
    {
        this.output = new List<T>();
        if (input != null)
            this.output.AddRange(output);
        return this;
    }

    public override LinkedList<T> Write(LinkedList<T> list)
    {
        if (list == null || list.Count == 0)
            return list;
        LinkedListNode<T> node = list.First;
        List<LinkedListNode<T>> result = new List<LinkedListNode<T>>();

        while (node != null)
        {
            if (node.Value.Equals(input) && Cond(node))
            {
                if (node == list.First)
                    list.AddBefore(node, node.Value);

                LinkedListNode<T> auxiliar = node.Previous;
                list.Remove(node);
                foreach (var val in output)
                {
                    auxiliar = list.AddAfter(auxiliar, val);
                    result.Add(auxiliar);
                }

                if (node == list.First)
                    list.RemoveFirst();
            }
        }
        PostProcess(result.ToArray());
        return list;
    }
}
