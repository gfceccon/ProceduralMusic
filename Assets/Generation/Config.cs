using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [Header("Beats Per Minute")]
    public float bpm;
    public Tempo.Beat relative;

    [Header("Tempo")]
    public int size;
    public Tempo.Beat length;

    public List<float> scale = new List<float> { 0, 2, 3, 5, 7, 8, 10 };
}
