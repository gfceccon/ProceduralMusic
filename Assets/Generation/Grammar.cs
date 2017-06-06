using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

class Grammar : MonoBehaviour
{
    private Dictionary<string, List<Rule<string>>> grammar;
    private GrammarNode<string> head;
    private GameObject tree;
    private string initial;

    public void Start()
    {
        this.grammar = new Dictionary<string, List<Rule<string>>>();
        this.head = new GrammarNode<string>();

        AddRule("S", new Product<string>("A", false), new Product<string>("C1", true), new Product<string>("B", false));

        AddRule("A", new Product<string>("D1", true), new Product<string>("A", false));
        AddRule("A", new Product<string>("E1", true), new Product<string>("A", false));
        AddRule("A", new Product<string>("G1", true));

        AddRule("B", new Product<string>("A1", true), new Product<string>("B", false));
        AddRule("B", new Product<string>("F1", true), new Product<string>("A", false));

        SetInitial("S");

        LinkedList<Product<string>> generatedList;

        Print();

        for (int i = 0; i < 10; i++)
        {
            generatedList = Generate(15);
            string output = "";
            foreach (var p in generatedList)
                output += p.product + " ";
            Debug.Log(output);
        }
        GenerateTree();
    }

    private void GenerateTree()
    {
        if (tree)
            DestroyObject(tree);
        tree = new GameObject("Grammar Tree");
        TreeNode(tree.transform, head);
    }

    private void TreeNode(Transform parent, GrammarNode<string> node)
    {
        foreach (Rule<string> rule in node.children.Keys)
        {
            GameObject nodeObj = new GameObject();
            string ruleString = rule.production + " ->";

            foreach (Product<string> product in rule.products)
                ruleString += " " + product.product;
            ruleString += "(" + node.children[rule].value + ")";

            nodeObj.name = ruleString;
            nodeObj.transform.SetParent(parent);

            TreeNode(nodeObj.transform, node.children[rule].next);
        }
    }

    public void Print()
    {
        foreach (string nonFinal in grammar.Keys)
        {
            string ruleString = nonFinal + " ->";
            bool first = true;
            foreach (Rule<string> rule in grammar[nonFinal])
            {
                if (!first)
                    ruleString += " |";
                else
                    first = false;

                foreach (Product<string> product in rule.products)
                    ruleString += " " + product.product;
                ruleString += "(" + rule.weight + ")";
            }
            Debug.Log(ruleString);
        }
    }

    public bool SetInitial(string initial)
    {
        if (!grammar.ContainsKey(initial))
            return false;
        this.initial = initial;
        return true;
    }

    public void AddRule(string production, params Product<string>[] products)
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

        List<Rule<string>> rules = null;
        Rule<string> rule = new Rule<string>(production, products);
        try
        {
            rules = grammar[production];
        }
        catch (Exception)
        {
            rules = new List<Rule<string>>();
            grammar.Add(production, rules);
        }
        rule.isFinal = isFinal;
        rules.Add(new Rule<string>(production, products));
    }

    public LinkedList<Product<string>> Generate(int maxDepth)
    {
        LinkedList<Product<string>> result = new LinkedList<Product<string>>();
        result.AddFirst(new Product<string>(initial, false));
        LinkedListNode<Product<string>> node;
        LinkedListNode<Product<string>> next;

        node = result.First;
        GenerateNext(result, node, head, maxDepth, false);

        node = result.First;
        next = node.Next;
        while (next != null)
        {
            if (!node.Value.isFinal)
                GenerateNext(result, node, head, maxDepth, true);
            node = next;
            next = next.Next;
        }

        return result;
    }

    public void GenerateNext(LinkedList<Product<string>> list, LinkedListNode<Product<string>> current, 
                                GrammarNode<string> treeNode, int depth, bool final)
    {
        if (depth == 0 || current.Value.isFinal)
            return;

        Rule<string> rule;
        string product = current.Value.product;

        List<Rule<string>> rules;
        grammar.TryGetValue(product, out rules);
        if (rules == null)
            return;

        List<Rule<string>> finals = rules.FindAll(r => r.isFinal);
        List<Rule<string>> nonFinals = rules.FindAll(r => !r.isFinal);

        if ((final || nonFinals.Count == 0) && finals.Count != 0)
            rule = finals[Random.Range(0, finals.Count)];
        else
            rule = nonFinals[Random.Range(0, nonFinals.Count)];


        Tuple<float, GrammarNode<string>> tuple;

        LinkedListNode<Product<string>> prev = current.Previous;
        LinkedListNode<Product<string>> next = current.Next;

        List<LinkedListNode<Product<string>>> products = new List<LinkedListNode<Product<string>>>();

        try
        {
            tuple = treeNode.children[rule];
        } catch(Exception)
        {
            tuple = treeNode.AddRule(rule);
        }
        tuple.value *= 2;
        rule.weight++;

        foreach (var p in rule.products)
            products.Add(new LinkedListNode<Product<string>>(p));

        LinkedListNode<Product<string>> first = products[0];
        LinkedListNode<Product<string>> last = products[products.Count - 1];

        list.Remove(current);

        // First
        if (prev == null)
        {
            products.Reverse();
            foreach (var p in products)
                list.AddFirst(p);
        }
        // Last
        else if (next == null)
        {
            foreach (var p in products)
                list.AddLast(p);
        }
        else
        {
            foreach (var p in products)
            {
                list.AddAfter(prev, p);
                prev = p;
            }
        }

        LinkedListNode<Product<string>> node = first;
        do
        {
            Product<string> p = node.Value;
            if (p.isFinal)
            {
                node = node.Next;
                continue;
            }

            GenerateNext(list, node, tuple.next, depth - 1, final);
            if (node == last)
                break;
            node = node.Next;
        } while (node != null);
    }
}