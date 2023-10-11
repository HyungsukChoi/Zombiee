using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieData : ScriptableObject
{
    public float health = 10f;
    public float damage = 1f;
    public float speed = 1f;
    public int score = 10;
    public Color skinColor = Color.red;
}
