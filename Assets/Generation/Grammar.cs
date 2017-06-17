using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

class Grammar<T>
{
    private Dictionary<string, List<Rule<T>>> grammar;
    private GrammarNode<T> head;
    private string initial;

    public delegate void GrammarNodeFunction(ref GrammarNode<T> ndoe);

    public void Start()
    {
        this.grammar = new Dictionary<string, List<Rule<T>>>();
        this.head = new GrammarNode<T>();
    }

    private void DFS(GrammarNodeFunction func)
    {
        TreeNode(head, func);
    }

    private void TreeNode(GrammarNode<T> node, GrammarNodeFunction func)
    {
        foreach (Rule<T> rule in node.children.Keys)
            TreeNode(node.children[rule].next, func);
    }

    public bool SetInitial(string initial)
    {
        if (!grammar.ContainsKey(initial))
            return false;
        this.initial = initial;
        return true;
    }

    public void AddRule(string production, params Product<T>[] products)
    {
        bool isFinal = true;
        for (int index = 0; index < products.Length; index++)
        {
            if (!products[index].isFinal)
            {
                isFinal = false;
                break;
            }
        }

        List<Rule<T>> rules = null;
        Rule<T> rule = new Rule<T>(production, products);
        try
        {
            rules = grammar[production];
        }
        catch (Exception)
        {
            rules = new List<Rule<T>>();
            grammar.Add(production, rules);
        }
        rule.isFinal = isFinal;
        rules.Add(new Rule<T>(production, products));
    }

    //public LinkedList<Product<string>> Generate(int maxDepth)
    //{
    //    LinkedList<Product<string>> result = new LinkedList<Product<string>>();
    //    result.AddFirst(new Product<string>(initial, false));
    //    LinkedListNode<Product<string>> node;
    //    LinkedListNode<Product<string>> next;

    //    node = result.First;
    //    GenerateNext(result, node, head, maxDepth, false);

    //    node = result.First;
    //    next = node.Next;
    //    while (next != null)
    //    {
    //        if (!node.Value.isFinal)
    //            GenerateNext(result, node, head, maxDepth, true);
    //        node = next;
    //        next = next.Next;
    //    }

    //    return result;
    //}

    //public void GenerateNext(LinkedList<Product<T>> list, LinkedListNode<Product<T>> current, 
    //                            GrammarNode<T> treeNode, int depth, bool final)
    //{
    //    if (depth == 0 || current.Value.isFinal)
    //        return;

    //    Rule<T> rule;
    //    T product = current.Value.product;

    //    List<Rule<T>> rules;
    //    grammar.TryGetValue(product, out rules);
    //    if (rules == null)
    //        return;

    //    List<Rule<string>> finals = rules.FindAll(r => r.isFinal);
    //    List<Rule<string>> nonFinals = rules.FindAll(r => !r.isFinal);

    //    if ((final || nonFinals.Count == 0) && finals.Count != 0)
    //        rule = finals[Random.Range(0, finals.Count)];
    //    else
    //        rule = nonFinals[Random.Range(0, nonFinals.Count)];


    //    Tuple<float, GrammarNode<string>> tuple;

    //    LinkedListNode<Product<string>> prev = current.Previous;
    //    LinkedListNode<Product<string>> next = current.Next;

    //    List<LinkedListNode<Product<string>>> products = new List<LinkedListNode<Product<string>>>();

    //    try
    //    {
    //        tuple = treeNode.children[rule];
    //    } catch(Exception)
    //    {
    //        tuple = treeNode.AddRule(rule);
    //    }
    //    tuple.value *= 2;
    //    rule.weight++;

    //    foreach (var p in rule.products)
    //        products.Add(new LinkedListNode<Product<string>>(p));

    //    LinkedListNode<Product<string>> first = products[0];
    //    LinkedListNode<Product<string>> last = products[products.Count - 1];

    //    list.Remove(current);

    //    // First
    //    if (prev == null)
    //    {
    //        products.Reverse();
    //        foreach (var p in products)
    //            list.AddFirst(p);
    //    }
    //    // Last
    //    else if (next == null)
    //    {
    //        foreach (var p in products)
    //            list.AddLast(p);
    //    }
    //    else
    //    {
    //        foreach (var p in products)
    //        {
    //            list.AddAfter(prev, p);
    //            prev = p;
    //        }
    //    }

    //    LinkedListNode<Product<string>> node = first;
    //    do
    //    {
    //        Product<string> p = node.Value;
    //        if (p.isFinal)
    //        {
    //            node = node.Next;
    //            continue;
    //        }

    //        GenerateNext(list, node, tuple.next, depth - 1, final);
    //        if (node == last)
    //            break;
    //        node = node.Next;
    //    } while (node != null);
    //}
}
