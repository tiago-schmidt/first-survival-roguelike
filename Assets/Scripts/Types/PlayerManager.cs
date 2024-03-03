using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public PlayerAttributes playerAttributes = new()
    {
        damage = 0f,
        range = 0f,
        attackSpeed = 0f,
        movementSpeed = 6.5f,
        defense = 0f,
        gold = 0f,
    };

    private void Update()
    {
        if (playerAttributes.attackSpeed > 90)
        {
            // attack speed is capped at 90
            playerAttributes.attackSpeed = 90;
        }
    }

    // defense breakpoints
    const int DEF_FIRST_BREAKPOINT = 50;
    const int DEF_SECOND_BREAKPOINT = 70;
    const int DEF_THIRD_BREAKPOINT = 100;
    const int DEF_LAST_BREAKPOINT = 101;

    private readonly IDictionary<int, float> defenseRatios = new Dictionary<int, float>()
    {
        { DEF_FIRST_BREAKPOINT, 0.9f },
        { DEF_SECOND_BREAKPOINT, 0.75f },
        { DEF_THIRD_BREAKPOINT, 0.5f },
        { DEF_LAST_BREAKPOINT, 0.35f },
    };

    public float CalculateDefenseReduction(float enemyDamage)
    {
        // defense calculation:
        // up to 50 defense, it scales at 1:0.9 ratio = 50*.9 = 45% reduction
        // from 51 to 70 defense, it scales at 1:0.75 ratio = (70 - 50) * .75 = 15% + 45% = 60% reduction
        // from 71 to 100 defense, it scales at 1:0.5 ratio = (100 - 70) * .5 = 15% + 60% = 75% reduction
        // above 100 defense, it scales at 1:0.35 ratio and is capped at 90%
        // for example 146 = (146 - 100) * .35 = would be 16% + 75% = 91% reduction, but will be 90% instead

        bool lessThanOrEqual1stBreakpoint = playerAttributes.defense <= DEF_FIRST_BREAKPOINT;
        bool greaterThan1stBreakpoint = playerAttributes.defense > DEF_FIRST_BREAKPOINT;
        bool lessThanOrEqual2ndBreakpoint = playerAttributes.defense <= DEF_SECOND_BREAKPOINT;
        bool greaterThan2ndBreakpoint = playerAttributes.defense > DEF_SECOND_BREAKPOINT;
        bool lessThanOrEqual3rdBreakpoint = playerAttributes.defense <= DEF_THIRD_BREAKPOINT;
        bool greaterThan3rdBreakpoint = playerAttributes.defense > DEF_THIRD_BREAKPOINT;

        float damageReduction = 0;

        if (lessThanOrEqual1stBreakpoint)
        {
            damageReduction = playerAttributes.defense * defenseRatios[DEF_FIRST_BREAKPOINT];
        }
        if (greaterThan1stBreakpoint)
        {
            damageReduction = DEF_FIRST_BREAKPOINT * defenseRatios[DEF_FIRST_BREAKPOINT];
        }
        if (greaterThan1stBreakpoint && lessThanOrEqual2ndBreakpoint)
        {
            float defenseInsideBreakpoint = playerAttributes.defense - DEF_FIRST_BREAKPOINT;
            damageReduction += defenseInsideBreakpoint * defenseRatios[DEF_SECOND_BREAKPOINT];
        }
        if (greaterThan2ndBreakpoint)
        {
            float defenseInsideBreakpoint = DEF_SECOND_BREAKPOINT - DEF_FIRST_BREAKPOINT;
            damageReduction += defenseInsideBreakpoint * defenseRatios[DEF_SECOND_BREAKPOINT];
        }
        if (greaterThan2ndBreakpoint && lessThanOrEqual3rdBreakpoint)
        {
            float defenseInsideBreakpoint = playerAttributes.defense - DEF_SECOND_BREAKPOINT;
            damageReduction += defenseInsideBreakpoint * defenseRatios[DEF_THIRD_BREAKPOINT];
        }
        if (greaterThan3rdBreakpoint)
        {
            float defenseInsideBreakpoint = DEF_THIRD_BREAKPOINT - DEF_SECOND_BREAKPOINT;
            damageReduction += defenseInsideBreakpoint * defenseRatios[DEF_THIRD_BREAKPOINT];

            float defenseAboveLastBreakpoint = playerAttributes.defense - DEF_THIRD_BREAKPOINT;
            damageReduction += defenseAboveLastBreakpoint * defenseRatios[DEF_LAST_BREAKPOINT];
        }

        if (damageReduction > 90)
        {
            // reduction is capped at 90%
            damageReduction = 90;
        }

        // dmg = 100, reduction 72 = 100 * 0.72 = 28 dmg received;
        float percentReduction = 1 - (damageReduction / 100);
        return enemyDamage * percentReduction;
    }

    public float CalculateRangeWithBonuses(float spellRange)
    {
        // example: player range = 125; spellRange = 8;
        // bonus range will be: 1 + 1.25 = 2.25;        
        float bonusRange = 1 + (playerAttributes.range / 100);

        // totalRange = 8 * 2.25 = 18;
        return spellRange * bonusRange;
    }

    public float CalculateAttackSpeedWithBonuses(float spellAtkSpeed)
    {
        // example: playerAtkSpd = 65; bonus = 1 - 0.65 = 0.35;
        float bonusAtkSpeed = 1 - (playerAttributes.attackSpeed / 100);

        // multiply the spellAtkSpeed with 0.35 and you get 65% bonus atkSpd
        return spellAtkSpeed * bonusAtkSpeed;
    }

    public void UpdateDamage(float damage)
    {
        playerAttributes.damage = damage;
    }

    public void UpdateRange(float range)
    {
        playerAttributes.range = range;
    }

    public void UpdateAttackSpeed(float attackSpeed)
    {
        playerAttributes.attackSpeed = attackSpeed;
    }

    public void UpdateMovementSpeed(float movementSpeed)
    {
        playerAttributes.movementSpeed = movementSpeed;
    }

    public void LoadData(GameData data)
    {
        playerAttributes.gold = data.gold;
    }

    public void SaveData(ref GameData data)
    {
        data.gold = playerAttributes.gold;
    }
}
