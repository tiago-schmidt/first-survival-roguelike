using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private IDictionary<string, EnemyAttributes> _enemiesAttributes;
    private HealthBarManager healthBarManager;
    private readonly float invulnerabilityTime = 0.25f;

    private void Start()
    {
        healthBarManager = gameObject.GetComponentInChildren<HealthBarManager>();

        _enemiesAttributes = GameObject.FindGameObjectWithTag(Tags.ENEMIES_MANAGER)
            .GetComponent<EnemiesManager>().enemiesAttributes;
    }

    private void OnTriggerStay(Collider other)
    {
        var enemyTag = other.tag;

        if (enemyTag.Contains("Enemy"))
        {
            float enemyDamage = _enemiesAttributes[enemyTag].damage;

            healthBarManager.UpdateHealth(-enemyDamage);
            StartCoroutine(GetInvunerability(other));
        }
    }

    IEnumerator GetInvunerability(Collider other)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), other, true);

        yield return new WaitForSeconds(invulnerabilityTime);

        Physics.IgnoreCollision(GetComponent<Collider>(), other, false);
    }
}
