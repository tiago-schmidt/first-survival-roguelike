using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LightningSpellManager : MonoBehaviour
{
    private SpellsManager _spellsManager;
    private PlayerManager _playerManager;

    private GameObject _spell;
    private SpellAttributes _spellAttributes;

    private NearestEnemyFinder _enemyFinder;

    void Start()
    {
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER).GetComponent<PlayerManager>();
        _spellsManager = GameObject.FindGameObjectWithTag(Tags.SPELLS_MANAGER).GetComponent<SpellsManager>();
        _spell = _spellsManager.lightningball;
        _spellAttributes = _spellsManager.spellsAttributes[_spell.tag];

        _enemyFinder = GetComponent<NearestEnemyFinder>();
        _enemyFinder.limitDistance = true;
        _enemyFinder.range = _spellAttributes.range;

        StartCoroutine(nameof(Shoot));
    }

    IEnumerator Shoot()
    {
        var lightningballSpell = GameObject.FindGameObjectWithTag(Tags.LIGHTNINGBALL_SPELL);
        var instantiatedSpell = Instantiate(_spell, lightningballSpell.transform);

        instantiatedSpell.tag = Tags.LIGHTNING_BALL;
        instantiatedSpell.AddComponent<FollowTarget>();
        var spellTargetFollower = instantiatedSpell.GetComponent<FollowTarget>();

        spellTargetFollower.agent = instantiatedSpell.GetComponent<NavMeshAgent>();
        spellTargetFollower.target = _enemyFinder.nearestEnemy != null 
            ? _enemyFinder.nearestEnemy.transform
            : null;

        _enemyFinder.range = _playerManager.CalculateRangeWithBonuses(_spellAttributes.range);

        if(spellTargetFollower.target == null)
        {
            Destroy(instantiatedSpell);
        }

        // wait for interval in X seconds (based on attack speed)
        float atkInterval = _playerManager.CalculateAttackSpeedWithBonuses(_spellAttributes.attackSpeed);
        yield return new WaitForSeconds(atkInterval);

        // keep shooting
        StartCoroutine(nameof(Shoot));
    }
}
