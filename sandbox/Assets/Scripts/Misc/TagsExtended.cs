﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsExtended : MonoBehaviour {
    [Flags]
    public enum Tags : short
    {
        FLOOR = 0X01,
        PLAYER = 0X02,
        WALL = 0X04,
        PLAYERKILLABLE = 0X08,
        ENEMYKILLABLE = 0X16
    };

    public Tags[] tags;

    public bool HasTag(Tags t)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (tags[i] == t)
                return true;
        }
        return false;
    }
}
