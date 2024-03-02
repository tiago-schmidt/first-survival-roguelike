using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAttributes playerAttributes = new()
    {
        damage = 0,
        range = 100,
        attackSpeed = 0,
        movementSpeed = 6.5f,
    };

    private void Update()
    {
        if(playerAttributes.attackSpeed > 90)
        {
            // attack speed is capped at 90
            playerAttributes.attackSpeed = 90;
        }
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
}
