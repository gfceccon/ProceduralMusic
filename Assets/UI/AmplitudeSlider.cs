using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Slider))]
public class AmplitudeSlider : MonoBehaviour
{
    public MasterUI master;
    public Oscilator oscilator;

    public void Set(MasterUI master, Oscilator oscilator, MinMax minMax)
    {
        this.master = master;
        this.oscilator = oscilator;
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(Slide);
        slider.minValue = minMax.min;
        slider.maxValue = minMax.max;
    }

    public void Slide(float value)
    {
        oscilator.amplitude = value;
        master.Refresh();
    }
}
