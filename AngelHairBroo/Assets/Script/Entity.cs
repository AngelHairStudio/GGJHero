using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{
    public float startingHealth;

    protected float health;
    protected bool dead;   

    public event System.Action onDeath;


    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        damaged = true;
        health -= damage;


        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    protected void Die()
    {
        dead = true;
        if (onDeath != null)
            onDeath();

        GameObject.Destroy(gameObject);
    }

    void Update()
    {

    }

}