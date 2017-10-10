using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class MasterUI : MonoBehaviour
{
    public SynthWave wave;
    public GameObject prefab;

    public TabsUI handler;
    public MusicUI musicUI;
    public WaveUI waveUI;

    public Slider slider;
    public Text rules;

    public MinMax amplitudeMinMax;
    public MinMax frequencyMinMax;


    private void Start()
    {
        handler.AddTab(gameObject);
        Displacement();
    }

    public void AddOscilator(Oscilator parent, string parentName)
    {
        Oscilator oscilator;
        oscilator = new Oscilator();

        if (parent == null)
            wave.master.Add(oscilator);
        else
            parent.modulators.Add(oscilator);

        GameObject tab;
        tab = Instantiate(prefab);
        int index;
        if (parent == null)
            index = wave.master.Count;
        else
            index = parent.modulators.Count;
        tab.name = parentName + " < Osc " + index;
        handler.AddTab(tab);
        
        AddOsc add = tab.GetComponentInChildren<AddOsc>();
        RemoveOsc remove = tab.GetComponentInChildren<RemoveOsc>();
        NameUI name = tab.GetComponentInChildren<NameUI>();

        AmplitudeSlider amplitude = tab.GetComponentInChildren<AmplitudeSlider>();
        FrequencySlider frequency = tab.GetComponentInChildren<FrequencySlider>();
        PhaseSlider phase = tab.GetComponentInChildren<PhaseSlider>();

        if (add != null)
            add.Set(tab.name, oscilator, this);
        if(remove != null)
            remove.Set(this, wave, parent, oscilator, tab, handler);
        if (name != null)
            name.SetName(tab.name);

        phase.Set(this, oscilator);
        amplitude.Set(this, oscilator, amplitudeMinMax);
        frequency.Set(this, oscilator, frequencyMinMax);
    }

    public void AddOscilator()
    {
        AddOscilator(null, "Master");
    }

    public void Generate()
    {
        musicUI.Generate();
    }

    public void Displacement()
    {
        wave.displacement = (int)slider.value;
        waveUI.Draw();
        musicUI.Refresh();
    }

    public void Refresh()
    {
        waveUI.Draw();
        musicUI.Refresh();
    }
}
