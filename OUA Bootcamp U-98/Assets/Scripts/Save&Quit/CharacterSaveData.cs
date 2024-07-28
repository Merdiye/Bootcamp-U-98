using System;
using UnityEngine;
[Serializable]
public class CharacterData
{
    public float[] position;
    public int health;

    public CharacterData(Vector3 position, int health)
    {
        this.position = new float[] { position.x, position.y, position.z };
        this.health = health;
    }
}
