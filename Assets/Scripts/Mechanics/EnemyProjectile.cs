﻿using UnityEngine;

namespace Mechanics
{
    public class EnemyProjectile : EnemyDamage
    {
        [SerializeField] private float speed;
        [SerializeField] private float resetTime;
        private float _lifetime;
        private Animator _anim;
        private BoxCollider2D _coll;
        private bool _hit;
        private static readonly int Explode = Animator.StringToHash("Explode");

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _coll = GetComponent<BoxCollider2D>();
        }

        public void ActivateProjectile()
        {
            _hit = false;
            _lifetime = 0;
            gameObject.SetActive(true);
            _coll.enabled = true;
        }
        private void Update()
        {
            if (_hit) return;
            float movementSpeed =  speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);
            
            _lifetime += Time.deltaTime;
            if (_lifetime > resetTime)
                gameObject.SetActive(false);
        }

        private new void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.tag);
            if (!collision.CompareTag("Untagged") && !collision.CompareTag("Enemy")) _hit = true;
            // base.OnTriggerEnter2D(collision); //Execute logic from parent script first
            _coll.enabled = false;
            
            if (_anim != null)
                _anim.SetTrigger(Explode); //When the object is a fireball explode it
        }
        private void Deactivate()
        {
            
            gameObject.SetActive(false);
        }
    }
}