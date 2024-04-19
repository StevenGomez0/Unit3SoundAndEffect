using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 10;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private bool grounded;
    public bool gameOver;
    public bool canDoubleJump;
    public bool sprinting;

    private Animator playerAnim;
    public AudioClip jumpSFX;
    public AudioClip deathSFX;
    private AudioSource playerAudio;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        InvokeRepeating("Score", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSFX, 1.0f);
            playerAnim.SetTrigger("Jump_trig");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSFX, 1.0f);
            playerAnim.SetTrigger("Jump_trig");
            canDoubleJump = false;
        }

        if(Input.GetKey(KeyCode.LeftShift) && grounded && !gameOver)
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            canDoubleJump = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = Vector3.zero;
            playerAnim.SetBool("Death_b", true);
            gameOver = true;
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(deathSFX, 1.0f);
            Debug.Log("Game Over");
        }
    }

    private void Score()
    {
        if (!sprinting && !gameOver)
        {
            score += 1;
            Debug.Log("Score: " + score);
        }
        else if(sprinting && !gameOver)
        {
            score += 2;
            Debug.Log("Score: " + score);
        }
    }
}
