using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public enum Wave
{
    Square,
    Sawtooth,
    Triangle
}

public class Synth : MonoBehaviour
{
    private Pool<Wave> pool;
    private void Start()
    {
        pool = new Pool<Wave>(5, (Wave x) =>
        {
            switch (x)  
            {
                default:
                    break;
            }
        });
    }
}
