using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour, IDamagable
{
    public float Health;
    public float curHealth { get;  protected set; }
    public bool isDead { get; protected set; }
    public event Action onDeath;

    protected virtual void OnEnable()
    {
        isDead = false;
        curHealth = Health;
    }

    // Start is called before the first frame update
    public virtual void OnDamage(float damage)
    {
        curHealth -= damage;
        if(curHealth <= 0 && !isDead)
        {
            curHealth = 0;
            onDeath.Invoke();
            isDead = true;
        }
    }
}
