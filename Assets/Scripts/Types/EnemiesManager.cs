using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject eliteEnemy1;
    public GameObject bossEnemy1;

    public IDictionary<string, EnemyAttributes> enemiesAttributes = new Dictionary<string, EnemyAttributes>()
    {
        { 
            Tags.ENEMY_1, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 3.5f,
            }
        },
        { 
            Tags.ENEMY_2, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 3.5f,
            }
        },
        { 
            Tags.ENEMY_3, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 3.5f,
            }
        },
        { 
            Tags.ENEMY_4, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 3.5f,
            }
        },
        { 
            Tags.ELITE_ENEMY_1, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 4.5f,
            }
        },
        { 
            Tags.BOSS_ENEMY_1, new EnemyAttributes() {
                damage = 5,
                movementSpeed = 5,
            }
        },
    };
}
