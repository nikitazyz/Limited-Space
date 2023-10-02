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
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Coroutine _coroutine;
        private Color _color;

        private void Awake()
        {
            _color = _spriteRenderer.color;
            LevelReset.LevelRestarted += LevelResetOnLevelRestarted;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= LevelResetOnLevelRestarted;
        }

        private void LevelResetOnLevelRestarted()
        {
            if (_coroutine == null)
            {
                return;
            }
            StopCoroutine(_coroutine);
            _coroutine = null;
            _collider.enabled = true;
            _spriteRenderer.color = _color;
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
            var newColor = _color;
            newColor.a = 0.3f;
            _spriteRenderer.color = newColor;
            yield return new WaitUntil(() => !_playerStandDetector.IsStand);
            yield return new WaitForSeconds(_returnTime);
            _collider.enabled = true;
            _spriteRenderer.color = _color;
            _coroutine = null;
        }
    }
}