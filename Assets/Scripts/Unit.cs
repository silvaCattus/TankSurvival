using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour
{
    public class  UnitUnityEvent : UnityEvent<Unit>{}

    [Range(0f, 1f)]
    [SerializeField] protected float armor;
    [SerializeField] protected float hp;
    [SerializeField] protected float speed = 5f;

    [HideInInspector] public FloatUnityEvent hpChanged = new FloatUnityEvent();
    [HideInInspector] public UnitUnityEvent died = new UnitUnityEvent();

    public void GetDamage(float damage)
    {
        if(armor != 0)
        {
            damage /= armor;
        }
        hp -= damage;

        Math.Round(hp, 2);

        if (hp <= 0f)
        {
            hp = 0f;
            died?.Invoke(this);
        }

        hpChanged?.Invoke(hp);
    }
}
