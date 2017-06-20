using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class ReductionRule<T> : Rule<T>
{
    public List<T> input;
    public T output;

    public ReductionRule<T> Input(params T[] input)
    {
        this.input = new List<T>();
        if (input != null)
            this.input.AddRange(input);
        return this;
    }

    public ReductionRule<T> Output(T output)
    {
        this.output = output;
        return this;
    }

    public override LinkedList<T> Write(LinkedList<T> list)
    {
        if (list == null || list.Count == 0)
            return list;
        LinkedListNode<T> node = list.First;

        while (node != null)
        {
            LinkedListNode<T> listNode = node;
            int counter = input.Count;
            bool replace = true;
            foreach (T val in input)
            {
                if (listNode == null)
                    return list;
                if (!listNode.Value.Equals(val))
                {
                    replace = false;
                    break;
                }
                listNode = listNode.Next;
            }
            if (replace && Apply())
            {
                listNode = node;
                bool first = true;
                foreach (T val in input)
                {
                    if(first)
                    {
                        listNode.Value = output;
                        first = false;
                        listNode = listNode.Next;
                    }
                    else
                    {
                        LinkedListNode<T> auxiliar = listNode.Next;
                        list.Remove(listNode);
                        listNode = auxiliar;
                    }
                }
                node = listNode;
            }
        }
        return list;
    }
}
