using System.Collections;
using System.Collections.Generic;
using LevelSystem;
using UnityEngine;

public class ReverseEffect : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    void Start()
    {
        LevelReset.LevelRestartStarted += () => _effect.SetActive(true);
        LevelReset.LevelRestarted += () => _effect.SetActive(false);
    }
}
