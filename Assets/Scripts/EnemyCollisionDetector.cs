using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetector : MonoBehaviour
{
    private PlayerAttributes _playerAttributes;
    private IDictionary<string, SpellAttributes> _spellsAttributes;
    private HealthBarManager _healthBarManager;

    private Renderer _enemyRenderer;
    private Color _enemyColor;

    private readonly float _invulnerabilityTime = 0.15f;

    private void Start()
    {
        _enemyRenderer = GetComponent<Renderer>();
        _enemyColor = _enemyRenderer.material.color;

        _healthBarManager = gameObject.GetComponentInChildren<HealthBarManager>();

        _spellsAttributes = GameObject.FindGameObjectWithTag(Tags.SPELLS_MANAGER)
            .GetComponent<SpellsManager>().spellsAttributes;

        _playerAttributes = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER)
            .GetComponent<PlayerManager>().playerAttributes;
    }

    private void OnTriggerStay(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag.Contains("Spell"))
        {
            var spell = _spellsAttributes[tag];

            float damage = CalculateDamage(spell.damage);

            DamageEnemy(spell.damage);

            if (spell.destroyOnContact)
            {
                Destroy(other.gameObject);
            }
            if (spell.triggerInvunerability)
            {
                StartCoroutine(GetInvunerability(other));
            }
        }
    }

    private float CalculateDamage(float spellDamage)
    {
        // example: player damage = 115; spellDamage = 10;
        // bonus damage will be: 1 + 1.15 = 2.15;
        float playerDamageBonus = 1 + (_playerAttributes.damage / 100);
        float totalDamage = playerDamageBonus * spellDamage;
        // totalDamage = 21.5;
        return totalDamage;
    }

    private void DamageEnemy(float damage)
    {
        _healthBarManager.UpdateHealth(-damage);

        if (_healthBarManager.currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(ChangeEnemyColor());
    }

    IEnumerator GetInvunerability(Collider other)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), other, true);

        yield return new WaitForSeconds(_invulnerabilityTime);

        Physics.IgnoreCollision(GetComponent<Collider>(), other, false);
    }

    IEnumerator ChangeEnemyColor()
    {
        float darkerRed = _enemyRenderer.material.color.r - 0.3f;
        _enemyRenderer.material.color = new Color(darkerRed, 0, 0);

        yield return new WaitForSeconds(0.075f);

        _enemyRenderer.material.color = _enemyColor;
    }
}
