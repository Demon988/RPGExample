using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float MaxHealth = 100f;
    float CurrentHealth = 100f;

    public float HealthPercent
    {
        get
        {
            return CurrentHealth / MaxHealth;
        }
    }
}
