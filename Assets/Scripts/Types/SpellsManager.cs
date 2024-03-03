using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    public GameObject fireballSpell;
    public GameObject lightningballSpell;
    public GameObject lightningball;

    public IDictionary<string, SpellAttributes> spellsAttributes = new Dictionary<string, SpellAttributes>()
    {
        { 
            Tags.FIRE_BALL, new SpellAttributes() {
                damage = 10f,
                range = 6f,
                attackSpeed = 8.5f,
                destroyOnContact = false,
                triggerInvunerability = true
            }
        },
        { 
            Tags.LIGHTNING_BALL, new SpellAttributes() {
                damage = 35f,
                range = 8f,
                attackSpeed = 1.25f,
                destroyOnContact = true,
                triggerInvunerability = false,
            }
        }
    };

    public void UpdateSpellDamage(string spell, float damage)
    {
        spellsAttributes[spell].damage = damage;
    }

    public void UpdateSpellRange(string spell, float range)
    {
        spellsAttributes[spell].range = range;
    }

    public void UpdateSpellAttackSpeed(string spell, float attackSpeed)
    {
        spellsAttributes[spell].attackSpeed = attackSpeed;
    }
}
