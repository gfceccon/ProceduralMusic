using UnityEngine;
using System.Collections;

public enum Mode
{
    Major,
    Minor,
    Locrian,
    MixoLydian,
    Persian
}

public class Scale
{
    public int fundamentalNote;

    public Scale()
    {
    }

    public Scale(int note)
    {
        this.fundamentalNote = note;
    }

    #region Notes
    public int MinorSecond      { get { return fundamentalNote + 1; } }
    public int MajorSecond      { get { return fundamentalNote + 2; } }
    public int MinorThird       { get { return fundamentalNote + 3; } }
    public int MajorThird       { get { return fundamentalNote + 4; } }
    public int Fourth           { get { return fundamentalNote + 5; } }
    public int DiminishedFifth  { get { return fundamentalNote + 6; } }
    public int Fifth            { get { return fundamentalNote + 7; } }
    public int MinorSixth       { get { return fundamentalNote + 8; } }
    public int MajorSixth       { get { return fundamentalNote + 9; } }
    public int MinorSeventh     { get { return fundamentalNote + 10; } }
    public int MajorSeventh     { get { return fundamentalNote + 11; } }
    #endregion

    #region Scales
    public int[] MajorScale
    {
        get 
        {
            int[] scale = new int[7];
            scale[0] = this.fundamentalNote;
            scale[1] = this.MajorSecond;
            scale[2] = this.MajorThird;
            scale[3] = this.Fourth;
            scale[4] = this.Fifth;
            scale[5] = this.MajorSixth;
            scale[6] = this.MajorSixth;

            return scale;
        }
    }

    public int[] MinorScale
    {
        get
        {
            int[] scale = new int[7];
            scale[0] = this.fundamentalNote;
            scale[1] = this.MajorSecond;
            scale[2] = this.MinorThird;
            scale[3] = this.Fourth;
            scale[4] = this.Fifth;
            scale[5] = this.MinorSixth;
            scale[6] = this.MinorSeventh;

            return scale;
        }
    }

    public int[] LocrianScale
    {
        get
        {
            int[] scale = new int[7];
            scale[0] = this.fundamentalNote;
            scale[1] = this.MinorSecond;
            scale[2] = this.MinorThird;
            scale[3] = this.Fourth;
            scale[4] = this.DiminishedFifth;
            scale[5] = this.MinorSixth;
            scale[6] = this.MinorSeventh;

            return scale;
        }
    }

    public int[] MixoLydianScale
    {
        get
        {
            int[] scale = new int[7];
            scale[0] = this.fundamentalNote;
            scale[1] = this.MajorSecond;
            scale[2] = this.MajorThird;
            scale[3] = this.Fourth;
            scale[4] = this.Fifth;
            scale[5] = this.MajorSixth;
            scale[6] = this.MinorSeventh;

            return scale;
        }
    }

    public int[] PersianScale
    {
        get
        {
            int[] scale = new int[7];
            scale[0] = this.fundamentalNote;
            scale[1] = this.MinorSecond;
            scale[2] = this.MajorThird;
            scale[3] = this.Fourth;
            scale[4] = this.DiminishedFifth;
            scale[5] = this.MinorSixth;
            scale[6] = this.MajorSeventh;

            return scale;
        }
    }
    #endregion

    #region Chords
    public int[] MajorChord
    {
        get
        {
            int[] chord = new int[3];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;

            return chord;
        }
    }

    public int[] MajorSixthChord
    {
        get
        {
            int[] chord = new int[4];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MajorSixth;

            return chord;
        }
    }

    public int[] MajorSeventhChord
    {
        get
        {
            int[] chord = new int[5];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MajorSeventh;
            chord[4] = this.MajorSeventh;

            return chord;
        }
    }

    public int[] MajorNinethChord
    {
        get
        {
            int[] chord = new int[5];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MajorSeventh;
            chord[4] = this.MajorSecond + 12;

            return chord;
        }
    }

    public int[] MinorChord
    {
        get
        {
            int[] chord = new int[3];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MinorThird;
            chord[2] = this.Fifth;

            return chord;
        }
    }

    public int[] MinorSixthChord
    {
        get
        {
            int[] chord = new int[4];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MinorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MajorSixth;

            return chord;
        }
    }

    public int[] MinorSeventhChord
    {
        get
        {
            int[] chord = new int[4];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MinorSeventh;

            return chord;
        }
    }

    public int[] MinorNinethChord
    {
        get
        {
            int[] chord = new int[5];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorThird;
            chord[2] = this.Fifth;
            chord[3] = this.MinorSeventh;
            chord[4] = this.MajorSecond + 12;

            return chord;
        }
    }

    public int[] Sus2Chord
    {
        get
        {
            int[] chord = new int[3];
            chord[0] = this.fundamentalNote;
            chord[1] = this.MajorSecond;
            chord[2] = this.Fifth;

            return chord;
        }
    }

    public int[] Sus4Chord
    {
        get
        {
            int[] chord = new int[3];
            chord[0] = this.fundamentalNote;
            chord[1] = this.Fourth;
            chord[2] = this.Fifth;

            return chord;
        }
    }

    #endregion

    /// <summary>
    /// Return a new Scale object determined by the octave
    /// </summary>
    /// <param name="octave"> The new octave (based on current fundamentalNote)</param>
    /// <returns> Scale object</returns>
    public Scale Octave(int octave)
    {
        if (octave < 0 && -octave * 12 > fundamentalNote)
            return null;
        return new Scale(fundamentalNote + octave * 12);
    }


    /// <summary>
    /// Return a scale in a range (the max range is 0 to 2 * fundamentalNote).
    /// Both param are unsigned
    /// </summary>
    /// <param name="mode"> Scale mode</param>
    /// <param name="down"> How deep the scale come from</param>
    /// <param name="up"> How far the scale goes to</param>
    /// <returns> Scale integer array</returns>
    public int[] GetScaleInRange(Mode mode, int down, int up)
    {
        if (down < 0)
            down *= -1;
        if (up < 0)
            up *= -1;
        if (down * 12 > fundamentalNote || up * 12 > fundamentalNote * 2)
            return null;

        int[] scale = null;
        int[] auxiliarArray = null;
        Scale auxiliarScale = new Scale();

        for (int i = -down; i < up; i++)
        {
            auxiliarScale.fundamentalNote = fundamentalNote + 12 * i;
            switch (mode)
            {
                case Mode.Major:
                    auxiliarArray = auxiliarScale.MajorScale;
                    break;
                case Mode.Minor:
                    auxiliarArray = auxiliarScale.MinorScale;
                    break;
                case Mode.Locrian:
                    auxiliarArray = auxiliarScale.LocrianScale;
                    break;
                case Mode.MixoLydian:
                    auxiliarArray = auxiliarScale.MixoLydianScale;
                    break;
                case Mode.Persian:
                    auxiliarArray = auxiliarScale.PersianScale;
                    break;
                default:
                    break;
            }
            if (scale == null)
                scale = new int[(down + up) * auxiliarArray.Length];
            for (int j = 0; j < auxiliarArray.Length; j++)
            {
                scale[(i + down) * auxiliarArray.Length + j] = auxiliarArray[j];
            }
            auxiliarArray = null;
        }
        auxiliarScale = null;

        return scale;
    }


}
