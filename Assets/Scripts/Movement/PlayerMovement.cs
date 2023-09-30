using System;
using System.Collections;
using System.Linq;
using TimeUtility;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : CharacterMovement
    {
        public event Action Jumped;
        public event Action Dashed;

        [SerializeField] private float _slideDeceleration = 5;
        
        [SerializeField] private Cooldown _jumpRequestCooldown = new Cooldown(0.1f);
        [SerializeField] private Cooldown _coyoteJumpTime = new(0.2f);
        [SerializeField] private Cooldown _bufferJumpTime = new(0.2f);
        [SerializeField] private Cooldown _dashTime = new(0.5f);
        [SerializeField] private float _dashSpeed = 10;
        
        private bool _canCoyoteJump;
        private float _initialDeceleration;
        private bool _canDoubleJump;

        public bool CanJump { get; set; } = true;

        public bool IsCrouch { get; set; }
        public bool IsJump { get; private set; }
        public bool IsDash => _dashTime.Timer > 0;
        
        public bool IsFly { get; set; }


        protected override void Awake()
        {
            base.Awake();
            _initialDeceleration = Deceleration;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_bufferJumpTime.Timer > 0 && Velocity.y <= 0 && OnGround && !IsDash)
            {
                JumpImpulse();
                _bufferJumpTime.Stop();
            }

            if (OnGround)
            {
                _coyoteJumpTime.Stop();
                _canCoyoteJump = true;
            }
            else if(Velocity.y <= 0 && _canCoyoteJump && !IsJump)
            {
                _coyoteJumpTime.Start(this);
            }

            if (IsFly && Velocity.y <= 0 && !OnGround)
            {
                Rigidbody.AddForce(Vector2.up * -Physics2D.gravity * 2f, ForceMode2D.Force);
            }

            if (IsDash)
            {
                Rigidbody.velocity = new Vector2(_dashSpeed * (IsLeft ? -1 : 1), 0);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (IsJump && OnGround && _jumpRequestCooldown.Timer == 0)
            {
                IsJump = false;
            }
            IsCrouch = IsCrouch && !IsDash;
            if (!IsCrouch || !OnGround)
            {
                Deceleration = _initialDeceleration;
            }
        }

        public void Dash()
        {
            IsCrouch = false;
            _dashTime.Start(this);
            Dashed?.Invoke();
        }

        public override void Move(float direction)
        {
            if (IsDash)
            {
                return;
            }
            
            if (IsCrouch && OnGround)
            {
                direction /= 3;
                Deceleration = Mathf.Abs(Velocity.x) > MaxSpeed / 2 && (int)Mathf.Sign(Velocity.x) == (int)Mathf.Sign(direction) ? _slideDeceleration : _initialDeceleration;
            }
            base.Move(direction);
        }

        public override void Jump()
        {
            if (_jumpRequestCooldown.Timer > 0 || IsDash)
            {
                return;
            }

            if (OnGround)
            {
                _canCoyoteJump = false;
                JumpImpulse();
            }
            else if(_coyoteJumpTime.Timer > 0)
            {
                JumpImpulse();
                _coyoteJumpTime.Stop();
            }
            else
            {
                _bufferJumpTime.Stop();
                _bufferJumpTime.Start(this);
            }
            _jumpRequestCooldown.Start(this);
        }

        private void JumpImpulse()
        {
            if (!CanJump)
            {
                return;
            }
            Rigidbody.velocity =new Vector2(Rigidbody.velocity.x, JumpForce);
            IsJump = true;
            Jumped?.Invoke();
        }

        protected override bool CheckGround()
        {
            float radius = 0.1f;
            Collider2D[] contacts = new Collider2D[10];
            Physics2D.OverlapCircle((Vector2)transform.position + Vector2.up * radius / 2, radius, 
                new ContactFilter2D() {layerMask = GroundMask, useLayerMask = true, useNormalAngle = true, minNormalAngle = 90 - 60, maxNormalAngle = 90 + 60}, contacts);
            return contacts.Any(contact => contact && (!contact.attachedRigidbody || contact.attachedRigidbody != Rigidbody));
        }
    }
}