using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    //½¥±ä
    public Gradient gradient;
    public Image Fill;

    private int curHealth = 10;

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int i)
    {
        curHealth -= i;
        slider.value = curHealth;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
