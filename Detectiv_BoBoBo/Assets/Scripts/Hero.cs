using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detectiv
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;


        private Vector2 _direction;
        private Collider2D[] _interactionResalt = new Collider2D[1];
        private Rigidbody2D _rigibody;
        private Animator _animator;
        private SpriteRenderer _sprite;


        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Awake()
        {
            _rigibody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var xVelocity = +_direction.x * _speed;
            var yVelocity = +_direction.y * _speed;
            _rigibody.velocity = new Vector2(xVelocity, yVelocity);


        }
    }
}
