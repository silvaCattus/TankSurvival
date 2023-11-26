using UnityEngine;

public abstract class Weapon
{
    public BulletBase bulletPrototype { get; protected set; }

    public int id { get; protected set; }
    public float rechargingTime { get; protected set; }
}

