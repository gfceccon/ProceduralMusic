using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Rule<T>
{
    [HideInInspector]
    public string production;
    [HideInInspector]
    public bool isFinal;
    [HideInInspector]
    public float weight;

    public List<Product<T>> products;

    public Rule(string production, params Product<T>[] products)
    {
        this.isFinal = false;
        this.weight = 1f;
        this.production = production;

        this.products = new List<Product<T>>();
        if (products != null)
            this.products.AddRange(products);
    }
}