using UnityEngine;
using System;
using System.Collections;

public class FullIndieAudioUtil
{
    private static float twelfthRootOfTwo = Mathf.Pow(2, 1.0f / 12);
    public static float St2pitch(float st)
    {
        return Mathf.Pow(twelfthRootOfTwo, st);
    }
    public static float Pitch2st(float pitch)
    {
        return Mathf.Log(pitch, twelfthRootOfTwo);
    }

    public static float DecibelToLinear(float dB)
    {
        float linear;
        if (dB > -80)
        {
            linear = Mathf.Pow(10.0f, dB / 20.0f);
        }
        else
        {
            linear = 0;
        }
        return linear;
    }
    public static float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -80.0f;

        return dB;
    }

    public static readonly DateTime _1970_01_01 = new DateTime(1970, 1, 1);
    /// <summary>
    /// Converts a given DateTime into a Unix 
    /// timestamp, i.e., number of seconds since 
    /// 1970-01-01.
    /// </summary>
    public static uint ToUnixTimestamp(DateTime a)
    {
        return (uint)Math.Truncate(a.ToUniversalTime().Subtract(_1970_01_01).TotalSeconds);
    }
}
