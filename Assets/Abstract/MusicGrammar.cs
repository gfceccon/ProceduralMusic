﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public interface IMusicGrammar : ICloneable
{
    void Play(Synth synth);
    void Stop(Synth synth);
}