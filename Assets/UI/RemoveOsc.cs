using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class RemoveOsc : MonoBehaviour
{
    public TabsUI handler;
    public GameObject tab;

    public SynthWave wave;
    public MasterUI master;

    public Oscilator parent;
    public Oscilator osc;

    public void Set(MasterUI master, SynthWave wave, Oscilator parent, Oscilator osc, GameObject tab, TabsUI handler)
    {
        this.master = master;
        this.parent = parent;
        this.osc = osc;
        this.tab = tab;
        this.handler = handler;

        Button button;
        button = GetComponent<Button>();
        button.onClick.AddListener(Remove);
    }

    public void Remove()
    {
        if (parent == null)
            wave.master.Remove(osc);
        else
            parent.modulators.Remove(osc);
        handler.RemoveTab(tab);
        master.Refresh();
    }
}
