using System;
using UnityEngine;

namespace VFX.Turtle
{
    public class TurtleAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _oldPosition;
        private void Update()
        {
            Vector3 direction = transform.position - _oldPosition;
            _oldPosition = transform.position;
            _spriteRenderer.flipX = !(direction.x < 0);
        }
    }
}
