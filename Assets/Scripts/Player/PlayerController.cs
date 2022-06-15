using UnityEngine;

namespace Player
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customization.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Max horizontal speed of the player;
        /// </summary>
        [Header("Movement Parameters")] 
        [SerializeField] private float maxSpeed;

        [SerializeField] private float jumpPower;

        [Header("Coyote Time")]
        
        [SerializeField]
        private float coyoteTime;

        private float _coyoteCounter;

        [Header("Multiple Jumps")] 
        public float gravityScale;

        [SerializeField] private int extraJumps;
        private int _jumpCounter;

        [Header("Wall Jumping")] [SerializeField]
        private float wallJumpX; // Horizontal wall jump force

        [SerializeField] private float wallJumpY; // Vertical wall jump force

        [Header("Wall Jumping")] [SerializeField]
        private LayerMask groundLayer;

        [SerializeField] private LayerMask wallLayer;

        // TODO: SEARCH FOR AUDIO ASSETS
        // [Header("Sounds")] [SerializeField] private AudioClip jumpSound;

        private Rigidbody2D _body;
        private Animator _anim;
        private CapsuleCollider2D _collider;
        private float _horizontalInput;
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int Grounded = Animator.StringToHash("isGround");
        
        private void Awake()
        {
            // Grab references for rigidbody and animator from object
            _body = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _collider = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");

            //Flip player when moving left-right
            if (_horizontalInput > 0.01f)
                transform.localScale = new Vector3(0.2f,0.2f,1f);
            else if (_horizontalInput < -0.01f)
                transform.localScale = new Vector3(-0.2f, 0.2f, 1f);

            //Set animator parameters
            _anim.SetBool(Run, _horizontalInput != 0);
            _anim.SetBool(Grounded, IsGrounded());

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            var bodyVelocity = _body.velocity;

            // Adjustable jump height
            if (Input.GetKeyUp(KeyCode.Space) && bodyVelocity.y > 0)
                _body.velocity = new Vector2(bodyVelocity.x, bodyVelocity.y / 2);

            if (OnWall())
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
            {
                _body.gravityScale = gravityScale;
                _body.velocity = new Vector2(_horizontalInput * maxSpeed, _body.velocity.y);

                if (IsGrounded())
                {
                    _coyoteCounter = coyoteTime; // Reset coyote counter when on the ground
                    _jumpCounter = extraJumps; // Reset jump counter to extra jump value
                }
                else
                    _coyoteCounter -= Time.deltaTime; // Start decreasing coyote counter when not on the ground 
            }
        }

        private void Jump()
        {
            // DEBUG MODE
            // print("counter: " + coyoteCounter);
            // If coyote counter is 0 or less an not on the wall and don have any extra jumps don't do anything 
            if (_coyoteCounter <= 0 && !OnWall() && _jumpCounter <= 0) return;

            // TODO: IMPLEMENT SOUND PLAYING
            // SoundManager.instance.PlaySound(jumpSound);

            if (OnWall())
                WallJump();
            else
            {
                if (IsGrounded())
                    _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                else
                {
                    // If not on the ground and coyote counter bigger than 0 do a normal jump
                    if (_coyoteCounter > 0)
                    {
                        _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                    }
                    else
                    {
                        // If we have extra jumps then jump and decrease the jump counter
                        if (_jumpCounter > 0)
                        {
                            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                            _jumpCounter--;
                        }
                    }
                }

                // Reset coyote counter to 0 to avoid double jumps
                _coyoteCounter = 0;
            }
        }

        private void WallJump()
        {
            _body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        }

        private bool IsGrounded()
        {
            var bounds = _collider.bounds;
            RaycastHit2D raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0,
                Vector2.down,
                0.1f, groundLayer);
            return raycastHit.collider != null;
        }

        private bool OnWall()
        {
            var bounds = _collider.bounds;
            RaycastHit2D raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0,
                new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
            return raycastHit.collider != null;
        }

        public bool CanAttack()
        {
            return _horizontalInput == 0 && IsGrounded() && !OnWall();
        }
    }
}