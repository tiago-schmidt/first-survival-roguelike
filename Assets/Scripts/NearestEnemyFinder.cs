using UnityEngine;

public class NearestEnemyFinder : MonoBehaviour
{
    private GameObject[] enemies;
    
    [HideInInspector]
    public float range;
    [HideInInspector]
    public bool limitDistance = false;
    [HideInInspector]
    public GameObject nearestEnemy;

    void Start()
    {
        InvokeRepeating(nameof(FindNearest), 0, 0.1f);
    }

    void FindNearest()
    {
        float distance;
        float nearestDistance = 100000;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        if(limitDistance && nearestDistance > range)
        {
            nearestEnemy = null;
        }
    }
}
