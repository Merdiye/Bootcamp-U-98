using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

     [Serializable]
public class SerializableVector2
{
    public float x, y;

    public SerializableVector2(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}

