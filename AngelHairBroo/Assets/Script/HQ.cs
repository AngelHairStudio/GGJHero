using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : Entity
{

    public Slider healthSlider;
    private bool damaged;



    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        healthSlider.value = health;
    }

}
