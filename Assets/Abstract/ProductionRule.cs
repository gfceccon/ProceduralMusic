using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class ProductionRule<T> : Rule<T> where T : ICloneable
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
            bool first = false;
            if (node.Value.Equals(input) && Cond(node))
            {
                if (node == list.First)
                {
                    first = true;
                    list.AddBefore(node, node.Value);
                }

                LinkedListNode<T> auxiliar = node.Previous;
                list.Remove(node);
                foreach (var val in output)
                {
                    auxiliar = list.AddAfter(auxiliar, (T)val.Clone());
                    result.Add(auxiliar);
                }

                if (first)
                    list.RemoveFirst();
                node = auxiliar;
            }
            node = node.Next;
        }
        PostProcess(result.ToArray());
        return list;
    }
}
