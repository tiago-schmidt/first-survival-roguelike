using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TimerManager _timerManager;
    private PlayerManager _playerManager;
    
    const float GOLD_PER_MINUTE = 17;
    const float ONE_MINUTE = 60;

    private void Start()
    {
        _timerManager = GameObject.FindGameObjectWithTag(Tags.TIMER).GetComponent<TimerManager>();
        _playerManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_MANAGER).GetComponent<PlayerManager>();
    }

    public void AddGoldToPlayerFromScore()
    {
        float gold = GOLD_PER_MINUTE * _timerManager.minutes;
        gold += GOLD_PER_MINUTE * (_timerManager.seconds / ONE_MINUTE);

        _playerManager.playerAttributes.gold += gold;
    }
}
