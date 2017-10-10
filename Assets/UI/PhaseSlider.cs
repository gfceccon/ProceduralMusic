using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Slider))]
public class PhaseSlider : MonoBehaviour
{
    public MasterUI master;
    public Oscilator oscilator;

    public void Set(MasterUI master, Oscilator oscilator)
    {
        this.master = master;
        this.oscilator = oscilator;
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(Slide);
        slider.minValue = 0f;
        slider.maxValue = 2 * (float)Math.PI;
    }

    public void Slide(float value)
    {
        oscilator.phase = value;
        master.Refresh();
    }
}
