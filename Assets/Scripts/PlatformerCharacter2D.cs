using System;
using UnityEngine;

    public class PlatformerCharacter2D : MonoBehaviour
    {
       
        [SerializeField] private float jumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool airControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask whatIsGround;                  // A mask determining what is ground to the character

        [SerializeField]
        string landingSoundName = "LandingFootsteps";

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool facingRight = true;  // For determining which way the player is currently facing.
        private bool inAir = false;

        Transform playerGFX;                // Ref to GFX to change direction

        AudioManager audioManager;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            //playerGFX = transform.Find("GFX");

            //if (playerGFX == null)
            //{
            //    Debug.LogError("AAAAAHHHHHHHHHHHHHH NO GFX OBJECT AS A CHILD OF THE PLAYER!!!!");

            //}
        }

        private void Start()
        {
            audioManager = AudioManager.instance;
            if (audioManager == null)
            {
                Debug.LogError("NO AUDIOMANAGER FOUND SKIPPY!!!");
            }
        }

        private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;

            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Fix for double sound
            if (!m_Grounded && !inAir)
            {
                inAir = true;
            }

            // Play Sound if we weren't grounded and are not grounded. BRACKEYS
            if (m_Grounded && inAir)
            {
                inAir = false;
                audioManager.PlaySound(landingSoundName);
            }




            //if (wasGrounded !=m_Grounded && m_Grounded == true)
            //{
            //    audioManager.PlaySound(landingSoundName);
            //}

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            // Old deprecated code
            playerGFX = transform.Find("GFX");

            if (playerGFX == null)
            {
                Debug.LogError("AAAAAHHHHHHHHHHHHHH NO GFX OBJECT AS A CHILD OF THE PLAYER!!!!");

            }
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, whatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || airControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*crouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move* PlayerStats.instance.movementSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGFX.localScale;
            theScale.x *= -1;
            playerGFX.localScale = theScale;
        }
    }

