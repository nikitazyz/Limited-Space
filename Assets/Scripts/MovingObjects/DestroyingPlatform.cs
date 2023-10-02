using System;
using System.Collections;
using LevelSystem;
using UnityEngine;

namespace MovingObjects
{
    public class DestroyingPlatform : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private float _returnTime;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private PlayerStandDetector _playerStandDetector;

        private Coroutine _coroutine;

        private void Awake()
        {
            LevelReset.LevelRestarted += LevelResetOnLevelRestarted;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= LevelResetOnLevelRestarted;
        }

        private void LevelResetOnLevelRestarted()
        {
            StopCoroutine(_coroutine);
        }

        private void Update()
        {
            if (_playerStandDetector.IsStand && _coroutine == null)
            {
                _coroutine = StartCoroutine(DestroyTimer());
            }
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_time);
            _collider.enabled = false;
            yield return new WaitUntil(() => !_playerStandDetector.IsStand);
            yield return new WaitForSeconds(_returnTime);
            _collider.enabled = true;
            _coroutine = null;
        }
    }
}