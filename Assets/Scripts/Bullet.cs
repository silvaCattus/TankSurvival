using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private float damage;
    private float speed;
    private Vector3 direction;
    private bool isMoving;

    public void SetBulletData(BulletBase bulletData, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        damage = bulletData.damage;
        speed = bulletData.speed;
    }

    public void StartMove(Vector2 direction)
    {
        isMoving = true;
        this.direction = direction;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        CheckPosition();
    }

    private void CheckPosition()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if(x < ScreenBorderData.instance.bottomLeft.x ||
           x > ScreenBorderData.instance.bottomRight.x ||
           y < ScreenBorderData.instance.bottomRight.y ||
           y > ScreenBorderData.instance.topRight.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy>())
        {
            collision.collider.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}
