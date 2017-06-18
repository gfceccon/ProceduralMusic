using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Rule<T>
{
    public bool Apply()
    {
        return true;
    }

    public abstract LinkedList<T> Write(LinkedList<T> list);
}
