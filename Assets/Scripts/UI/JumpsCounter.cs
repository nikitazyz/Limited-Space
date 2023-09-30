using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class JumpsCounter : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private TextMeshProUGUI _counterField;
        [SerializeField] private Color _zeroJumpsTextColor;
        private Color _initialColor;

        private void Awake()
        {
            _initialColor = _counterField.color;
            _player ??= FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            _counterField.text = $"x{_player.Jumps}";
            if (_player.Jumps == 0)
            {
                _counterField.color = _zeroJumpsTextColor;
                if (Input.GetButtonDown("Jump"))
                {
                    _counterField.transform.DOShakePosition(0.3f, Vector3.left * 50, 50, 0);
                }
            }
            else
            {
                _counterField.color = _initialColor;
            }
            
        }
    }
}
