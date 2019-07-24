using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHealthBar : MonoBehaviour
{

    Image healthBarImage;
    Enemy enemy;

    // Use this for initialization
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        healthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = enemy.HealthPercent;
    }
}
