using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Rule<T>
{
    public string production;
    public bool isFinal;

    public List<Product<T>> products;

    public Rule(string production, params Product<T>[] products)
    {
        this.isFinal = false;
        this.production = production;

        this.products = new List<Product<T>>();
        if (products != null)
            this.products.AddRange(products);
    }
}
