using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [Header("Beats Per Minute")]
    public float bpm;

    [Header("Tempo")]
    public int barLength;
    public Tempo.Beat noteLength;
}
