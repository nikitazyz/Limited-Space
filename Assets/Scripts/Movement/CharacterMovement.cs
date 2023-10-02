using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public event Action<bool> Flipped; 

        [SerializeField, HideInInspector] protected Rigidbody2D Rigidbody;

        [SerializeField] protected float MaxSpeed = 5;
        [SerializeField] protected float Acceleration = 15;
        [SerializeField] protected float Deceleration = 20;
        [SerializeField] protected float JumpForce = 7;
        [SerializeField] protected LayerMask GroundMask = 0x1;
        private Vector3 _initialScale;
        
        private Vector2 _actualVelocityStamp;
        
        public Vector2 ActualVelocity { get; private set; }
        
        public bool OnGround { get; protected set; }
        public bool IsLeft { get; protected set; }

        public Vector2 Velocity => Rigidbody.velocity;

        protected virtual void Awake()
        {
            _initialScale = transform.localScale;
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        { 
            OnGround = CheckGround();
            ActualVelocity = ((Vector2)transform.position - _actualVelocityStamp) / Time.fixedDeltaTime;
            _actualVelocityStamp = transform.position;
        }

        protected virtual bool CheckGround()
        {
            var hitsContainer = new RaycastHit2D[10];
            Rigidbody.Cast(Vector2.down, new ContactFilter2D() {layerMask = GroundMask}, hitsContainer, 0.1f);
            return hitsContainer.Any(hit2D => hit2D.rigidbody != Rigidbody && hit2D.normal.y > 0.5f);
        }
        
        protected virtual void OnEnable()
        {
            SetupRigidbody();
        }
        
        protected virtual void OnValidate()
        {
            SetupRigidbody();
        }

        private void SetupRigidbody()
        {
            Rigidbody ??= GetComponent<Rigidbody2D>();
        }

        public virtual void Jump()
        {
            if (OnGround)
            {
                Rigidbody.velocity += Vector2.up * JumpForce;
            }
        }

        public virtual void Move(float direction)
        {
            direction = Mathf.Clamp(direction, -1, 1);
            float speed = CalculateSpeed(Rigidbody.velocity.x, direction * MaxSpeed, Acceleration, Deceleration);
            Rigidbody.velocity = new Vector2(speed, Rigidbody.velocity.y);
            if (direction < -0.03f && !IsLeft)
            {
                transform.localScale = new Vector3(-_initialScale.x, _initialScale.y, _initialScale.z);
                IsLeft = true;
                Flipped?.Invoke(IsLeft);
            }
            else if (direction > 0.03f && IsLeft)
            {
                transform.localScale = _initialScale;
                IsLeft = false;
                Flipped?.Invoke(IsLeft);
            }
        }

        private float CalculateSpeed(float currentSpeed, float targetSpeed, float acceleration, float deceleration)
        {
            if (Mathf.Abs(targetSpeed - currentSpeed) < float.Epsilon)
            {
                return currentSpeed;
            }
            float changeDirection = Mathf.Sign(targetSpeed - currentSpeed);
            float changeSpeed = Mathf.Sign(currentSpeed) * changeDirection > 0 ? acceleration : deceleration;
            return Mathf.MoveTowards(currentSpeed, targetSpeed, changeSpeed * Time.fixedDeltaTime);
        }
    }
}
