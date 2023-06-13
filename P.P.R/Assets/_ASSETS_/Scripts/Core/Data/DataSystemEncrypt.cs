using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class DataSystem
{
    /// <summary>
    /// Encrypt Value
    /// </summary>
    private double Encrypt(double value)
    {
        return value;
    }

    private float Encrypt(float value)
    {
        return value;
    }

    private long Encrypt(long value)
    {
        return value;
    }

    private int Encrypt(int value)
    {
        return value;
    }

    private string Encrypt(string value)
    {
        return value;
    }

    private bool Encrypt(bool value)
    {
        return value;
    }

    /// <summary>
    /// Decrypt Value
    /// </summary>
    private double DecryptDouble(object value)
    {
        if (value == null) return 0.0;
        return (double)value;
    }
    private float DecryptFloat(object value)
    {
        if (value == null) return 0.0f;
        return Convert.ToSingle(value);
    }

    private long DecryptLong(object value)
    {
        if (value == null) return 0;
        return (long)value;
    }

    private int DecryptInt(object value)
    {
        if (value == null) return 0;
        return Convert.ToInt32(value);
    }

    private string DecryptString(object value)
    {
        if (value == null) return "";
        return (string)value;
    }

    private bool DecryptBool(object value)
    {
        if (value == null) return false;
        return (bool)value;
    }
}
