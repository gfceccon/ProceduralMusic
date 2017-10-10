using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Oscilator
{
    public float amplitude;
    public float frequency;
    public float phase;
    public List<Oscilator> modulators;

    public Oscilator()
    {
        modulators = new List<Oscilator>();
    }
}