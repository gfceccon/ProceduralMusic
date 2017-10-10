using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class AddOsc : MonoBehaviour
{
    public string name;
    public Oscilator osc;
    public MasterUI controller;

    public void Set(string name, Oscilator osc, MasterUI controller)
    {
        this.name = name;
        this.osc = osc;
        this.controller = controller;

        Button button;
        button = GetComponent<Button>();
        button.onClick.AddListener(Add);
    }

    public void Add()
    {
        controller.AddOscilator(osc, name);
    }
}
