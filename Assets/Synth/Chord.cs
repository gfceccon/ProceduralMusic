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

public class Chord : ICloneable
{
    public Note tone;
    public int nMods;
    private const int MAX_MODIFIER = 4;
    public Modifier[] mods = new Modifier[MAX_MODIFIER];

    public Chord(Note tone)
    {
        this.tone = tone;
        this.nMods = 0;
    }

    public Chord Mod(Modifier mod)
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

    public static bool operator ==(Chord This, Chord Other)
    {
        return This.tone == Other.tone;
    }

    public static bool operator !=(Chord This, Chord Other)
    {
        return !(This == Other);
    }

    public override bool Equals(object other)
    {
        if (other.GetType() == typeof(Chord))
        {
            Chord Other = ((Chord)other);
            return this == Other;
        }
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void Play(Player synth, WaveType wave, float amplitude, DutyCycle dutyCycle = DutyCycle.Half)
    {
        //channel = synth.Play(WaveType.Square, ToFreq(), 0.2f);
    }

    public void Stop(Player synth)
    {
        //synth.Stop(channel);
    }

    public object Clone()
    {
        Chord clone = new Chord(this.tone);
        for (int mod = 0; mod < nMods; mod++)
        {
            clone.Mod(this.mods[mod]);
        };
        return clone;
    }
}
