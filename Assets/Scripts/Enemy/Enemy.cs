using UnityEngine;

public abstract class Enemy : Unit
{
    [SerializeField] protected float rechargingTimer;
    [SerializeField] protected float damage;

    protected GameObject target;

    protected bool hasTarget;
    protected bool isRecharging;
    protected float rechargingTime;

    public void SetTarget(GameObject target)
    {
        this.target = target;
        hasTarget = true;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime);
    }

    private void Update()
    {
        if (hasTarget)
        {
            Move();
        }

        if (isRecharging)
        {
            rechargingTimer -= Time.deltaTime;
            if (rechargingTimer <= 0)
            {
                rechargingTimer = rechargingTime;
                isRecharging = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRecharging)
        {
            return;
        }

        if (collision.collider.GetComponent<TankBody>())
        {
            collision.collider.GetComponent<TankBody>().GetDamage(damage);
        }
    }

}