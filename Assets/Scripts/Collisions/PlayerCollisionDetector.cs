using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private PlayerManager _playerManager;
    private IDictionary<string, EnemyAttributes> _enemiesAttributes;
    private HealthBarManager _healthBarManager;
    private readonly float _invulnerabilityTime = 0.25f;

    private void Start()
    {
        _healthBarManager = gameObject.GetComponentInChildren<HealthBarManager>();

        _enemiesAttributes = GameObject.FindGameObjectWithTag(Tags.ENEMIES_MANAGER)
            .GetComponent<EnemiesManager>().enemiesAttributes;

        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER)
            .GetComponent<PlayerManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        var enemyTag = other.tag;

        if (enemyTag.Contains("Enemy"))
        {
            float enemyDamage = _enemiesAttributes[enemyTag].damage;
            float damageReceived = _playerManager.CalculateDefenseReduction(enemyDamage);

            _healthBarManager.UpdateHealth(-damageReceived);
            StartCoroutine(GetInvunerability(other));
        }

        if(_healthBarManager.currentHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<LifeCycleManager>().ShowGameOverScreen();
        }
    }

    IEnumerator GetInvunerability(Collider other)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), other, true);

        yield return new WaitForSeconds(_invulnerabilityTime);

        Physics.IgnoreCollision(GetComponent<Collider>(), other, false);
    }
}
