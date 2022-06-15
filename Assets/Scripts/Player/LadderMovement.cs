using System;
using Player;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(PlayerController), typeof(Animator))]
    public class LadderMovement : MonoBehaviour
    {
        private float _vertical;
        [SerializeField] private float speed = 8f;
        private Animator _anim;
        private bool _isLadder;
        private bool _isClimbing;
        private const String Ladder = "Ladder";
        [SerializeField] private Rigidbody2D rb;
        private static readonly int Climb = Animator.StringToHash("climb");

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            _vertical = Input.GetAxis("Vertical");

            if (_vertical != 0 && _isClimbing && _isLadder)
                _anim.SetBool(Climb, true);
            else
                _anim.SetBool(Climb, false);

            if (_isLadder && Mathf.Abs(_vertical) > 0f)
            {
                _isClimbing = true;
            }
        }

        private void FixedUpdate()
        {
            if (_isClimbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, _vertical * speed);
            }
            else
            {
                rb.gravityScale = GetComponent<PlayerController>().gravityScale;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            // Debug.Log("enter: "+ col.tag);
            if (col.CompareTag(Ladder))
            {
                _isLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Debug.Log("exit: " + other.tag);
            if (other.CompareTag(Ladder))
            {
                _isLadder = false;
                _isClimbing = false;
            }
        }
    }
}