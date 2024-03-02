using UnityEngine;

public class FireBallSpellManager : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    private PlayerManager _playerManager;
    private SpellsManager _spellsManager;
    private SpellAttributes _spellAttributes;

    void Start()
    {
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER).GetComponent<PlayerManager>();
        _spellsManager = GameObject.FindGameObjectWithTag(Tags.SPELLS_MANAGER).GetComponent<SpellsManager>();
        _spellAttributes = _spellsManager.spellsAttributes[Tags.FIRE_BALL];
    }

    void FixedUpdate()
    {
        float atkSpeed = _playerManager.CalculateAttackSpeedWithBonuses(_spellAttributes.attackSpeed);

        // is constantly moving (atkSpeed) and rotating (rotationSpeed)
        transform.Translate(Time.deltaTime * atkSpeed * direction);
        transform.Rotate(Time.deltaTime * GetRotationSpeed() * Vector3.up);
    }

    private float GetRotationSpeed()
    {
        // rotation speed is what defines the spell's range
        // the slower it rotates, the bigger the area it rotates in
        // example: spellAtkSpd = 8; spellRange = 5;
        // rotationSpeed = 800 / 5 = 160;
        // so it is moving at a speed of 8 (5% ratio compared to the rotation speed of 160)
        // bigger range, bigger ratio between speed / rotation
        // this solution was created with the purpose of being able to increase the spell speed
        // withouth changing the range and vice-versa
        float range = _playerManager.CalculateRangeWithBonuses(_spellAttributes.range);
        return (_spellAttributes.attackSpeed * 100) / range;
    }
}
