using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Rule<T> where T : IMusicGrammar
{
    public delegate bool Condition(LinkedListNode<T> value);
    public delegate void PostRule(params LinkedListNode<T>[] value);

    protected Condition Cond = Default;
    protected event PostRule Post;

    public Rule<T> SetCondition(Condition applicationRule)
    {
        Cond = applicationRule;
        return this;
    }

    private static bool Default(LinkedListNode<T> value)
    {
        return true;
    }

    public Rule<T> AddPostProcess(PostRule action)
    {
        Post += action;
        return this;
    }

    public void PostProcess(params LinkedListNode<T>[] value)
    {
        if(Post != null)
            Post(value);
    }

    public abstract LinkedList<T> Write(LinkedList<T> list);
}
