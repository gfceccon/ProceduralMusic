﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public interface IMusicGrammar : ICloneable
{
    void Play(Player synth, WaveType wave, float amplitude, DutyCycle dutyCycle = DutyCycle.Half);
    void Stop(Player synth);
}
