using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataFrame
{
    private static DataFrame instance;
    private DataFrame() { }
    public static DataFrame Instance { get { return instance == null ? instance = new DataFrame() : instance; } }

    public const int CATEGORY_BASICS = 1;
    public const int BASIC_TAG_PLAYER_LEVEL = 0;
    public const int BASIC_TAG_PLAYER_GOLD = 1;
    public const int BASIC_TAG_PLAYER_SCORE = 2;
    public const int BASIC_TAG_PLAYER_NAME = 3;
    public const int BASIC_TAG_PLAYER_HP = 4;

    public int PlayerLevel
    {
        get
        {
            return DataSystem.Instance.GetInt(CATEGORY_BASICS, BASIC_TAG_PLAYER_LEVEL);
        }
        set
        {
            DataSystem.Instance.SetValue(CATEGORY_BASICS, BASIC_TAG_PLAYER_LEVEL, value);
        }
    }

    public double PlayerHP
    {
        get
        {
            return DataSystem.Instance.GetDouble(CATEGORY_BASICS, BASIC_TAG_PLAYER_HP);
        }
        set
        {
            DataSystem.Instance.SetValue(CATEGORY_BASICS, BASIC_TAG_PLAYER_HP, value);
        }
    }

    public bool ValueIsChangedPlayerLevel()
    {
        return DataSystem.Instance.ValueIsChanged(CATEGORY_BASICS, BASIC_TAG_PLAYER_LEVEL);
    }

    public void SetPlayerLevelChangedCallback(DataSystem.DataIntChanged callback)
    {
        DataSystem.DataIsChanged changed = (int category, int tag, object from, object to) => 
        {
            callback((int)from, (int)to);
        };

        DataSystem.Instance.SetValueIsChangedCallback(CATEGORY_BASICS, BASIC_TAG_PLAYER_LEVEL, changed);
    }
}
