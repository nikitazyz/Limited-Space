using System;
using System.Collections;
using System.Collections.Generic;
using LevelSystem;
using UnityEngine;

public class ReverseEffect : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    void Start()
    {
        LevelReset.LevelRestartStarted += OnLevelResetOnLevelRestartStarted;
        LevelReset.LevelRestarted += OnLevelResetOnLevelRestarted;
    }

    private void OnLevelResetOnLevelRestarted()
    {
        _effect.SetActive(false);
    }

    private void OnLevelResetOnLevelRestartStarted()
    {
        _effect.SetActive(true);
    }

    private void OnDestroy()
    {
        LevelReset.LevelRestartStarted -= OnLevelResetOnLevelRestartStarted;
        LevelReset.LevelRestarted -= OnLevelResetOnLevelRestarted;
    }
}
