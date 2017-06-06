using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Product<T>
{
    public T product;
    [HideInInspector]
    public bool isFinal;

    public Product(T product, bool isFinal)
    {
        this.product = product;
        this.isFinal = isFinal;
    }
}