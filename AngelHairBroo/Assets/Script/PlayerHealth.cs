using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Entity {

    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5.0f;
    public Color flashCol = new Color(1f, 0f, 0f, 0.1f);
    private bool damaged;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        damaged = true;
    }

    void Update ()
    {
        healthSlider.value = health;

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
