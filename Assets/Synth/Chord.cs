using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public enum Tone
{
    I,
    II,
    III,
    IV,
    V,
    VI,
    VII
}

public enum Modifier
{
    major,
    minor,
    maj7,
    seventh,
    ninth,
    thirteenth,
    sus2,
    sus4,
    dim,
    aug
}

public class Chord
{
    public Note tone;
    public int nMods;
    private const int MAX_MODIFIER = 4;
    public Modifier[] mods = new Modifier[MAX_MODIFIER];

    private Chord(Note tone)
    {
        this.tone = tone;
        this.nMods = 0;
    }

    public static Chord chord(Note tone)
    {
        return new Chord(tone);
    }

    public Chord mod(Modifier mod)
    {
        if (nMods == MAX_MODIFIER)
            return this;
        bool allow = true;
        for(int i = 0; i < nMods; i++)
        {
            Modifier m = mods[i];
            switch (m)
            {
                case Modifier.major:
                case Modifier.minor:
                case Modifier.sus2:
                case Modifier.sus4:
                case Modifier.dim:
                case Modifier.aug:
                    switch (mod)
                    {
                        case Modifier.major:
                        case Modifier.minor:
                        case Modifier.sus2:
                        case Modifier.sus4:
                        case Modifier.dim:
                        case Modifier.aug:
                            allow = false;
                            break;
                        default:
                            break;
                    }
                    break;
                case Modifier.maj7:
                case Modifier.seventh:
                    switch (mod)
                    {
                        case Modifier.maj7:
                        case Modifier.seventh:
                            allow = false;
                            break;
                        default:
                            break;
                    }
                    break;
                case Modifier.ninth:
                    if (mod == Modifier.ninth)
                        allow = false;
                    break;
                case Modifier.thirteenth:
                    if (mod == Modifier.thirteenth)
                        allow = false;
                    break;
                default:
                    break;
            }
            if (!allow)
                break;
        }
        if (allow)
        {
            this.mods[nMods] = mod;
            nMods++;
        }
        return this;
    }
}
