using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{
    public float startingHealth;

    float timer;
    float lol = 2;
    protected float health;
    protected bool dead;

    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5.0f;
    public Color flashCol = new Color(1f, 0f, 0f, 0.1f);

    public event System.Action onDeath;

    private bool damaged;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        damaged = true;
        health -= damage;

        healthSlider.value = health;

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
        timer += Time.deltaTime;

        if (timer > lol)
        {
            TakeDamage(10);
            timer = 0;
        }
        if (damaged)
        {
            damageImage.color = flashCol;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

}