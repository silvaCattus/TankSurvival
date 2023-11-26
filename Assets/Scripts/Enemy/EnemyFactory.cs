using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : MonoBehaviour
{
    public enum SpawnSide
    {
        Up,
        Bottom,
        Left,
        Right,
    }

    [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
    [SerializeField] private GameObject target = null;

    private List<Enemy> enemies = new List<Enemy>();
    private const int maxEnemies = 10;

    private float minOffset = 1f;
    private float maxOffset = 10f;

    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        var values = Enum.GetValues(typeof(SpawnSide));
        SpawnSide randomSide = (SpawnSide)values.GetValue(Random.Range(0, values.Length));

        float x = 0f;
        float y = 0f;
        var screen = ScreenBorderData.instance;

        switch (randomSide)
        {
            case SpawnSide.Up:
                x = (float)Random.Range(screen.topLeft.x, screen.topRight.x);
                y = (float)Random.Range(screen.topLeft.y + minOffset, screen.topLeft.y + maxOffset);
                break;

            case SpawnSide.Bottom:
                x = (float)Random.Range(screen.topLeft.x, screen.topRight.x);
                y = (float)Random.Range(screen.bottomLeft.y - minOffset, screen.bottomLeft.y - maxOffset);
                break;

            case SpawnSide.Left:
                x = (float)Random.Range(screen.topLeft.x - minOffset, screen.topLeft.x - maxOffset);
                y = (float)Random.Range(screen.bottomLeft.y, screen.topLeft.y);
                break;

            case SpawnSide.Right:
                x = (float)Random.Range(screen.topRight.x + minOffset, screen.topRight.x + maxOffset);
                y = (float)Random.Range(screen.bottomRight.y, screen.topRight.y);
                break;
        }

        var newEnemy = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        newEnemy.SetTarget(target);
        newEnemy.died.AddListener(DestroyEnemy);
        enemies.Add(newEnemy);
    }

    private void DestroyEnemy(Unit unit)
    {
        if(unit is Enemy enemy)
        {
            Destroy(enemy.gameObject);
            enemies.Remove(enemy);

            while(enemies.Count < maxEnemies)
            {
                Spawn();
            }
        }
    }
}
