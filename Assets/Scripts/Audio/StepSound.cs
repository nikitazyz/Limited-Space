using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;
using Random = UnityEngine.Random;

public class StepSound : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private AudioSource _step;
    [SerializeField] private AudioSource _jump;

    [SerializeField] private AudioClip[] _steps;

    [SerializeField] private float _time;

    private Coroutine _stepCoroutine;

    
    
    private void Start()
    {
        _stepCoroutine = StartCoroutine(Step());
        _playerMovement.Jumped += () => _jump.Play();
    }

    IEnumerator Step()
    {
        do
        {
            yield return new WaitForSeconds(_time);
            if (_playerMovement.OnGround && Mathf.Abs(_playerMovement.Velocity.x) > 0.2f)
            {
                List<AudioClip> clips = new List<AudioClip>(_steps);
                if (_step.clip != null)
                {
                    clips.Remove(_step.clip);
                }
                
                AudioClip clip = clips[Random.Range(0, clips.Count)];
                _step.clip = clip;
                _step.Play();
            }
        } while (_stepCoroutine != null);
    }
}
