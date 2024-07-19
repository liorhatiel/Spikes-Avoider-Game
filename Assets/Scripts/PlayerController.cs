using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Sprites of the player
    [SerializeField] Sprite deadSprite;


    // Those are for player movement.
    [SerializeField] float verticalJumpForce;      // For VERTICAL movement.
    [SerializeField] float moveSpeed;              // For HORIZONTAL movement.
    [SerializeField] float gravity;                // For the downfall of the player.
    float playerScale;

    // Those are for player death.
    [SerializeField] float deleyTime;
    bool playerIsAlive = true;

    // Those are refrencess for other scripts.
    RightSpikeSpawner rightSpikeSpawner;
    LeftSpikeSpawner leftSpikeSpawner;
    GameManager gameManager;


    // Those are refrencess for the player.
    SoundManager soundManager;
    SpriteRenderer sr;
    Rigidbody2D rb;
    ParticleSystem explosionEffect;
    Animator animator;



    void Start()
    {
        Time.timeScale = 0;           
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        explosionEffect = FindObjectOfType<ParticleSystem>();
        gameManager = FindObjectOfType<GameManager>();
        rightSpikeSpawner = FindObjectOfType<RightSpikeSpawner>();
        leftSpikeSpawner = FindObjectOfType<LeftSpikeSpawner>();
        soundManager = FindObjectOfType<SoundManager>();
        playerScale = transform.localScale.x;                          // localScale.x = localScale.y. So it doesnt matter witch axis we choose.
    }

    void Update()
    {
        Fly();
    }

    private void FixedUpdate()
    {
        RealisticPhysics();
    }




    // This function will flip the player when he hit a wall.
    void Flip()
    {
        moveSpeed *= -1;
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed) , 1f) * playerScale;
    }


    // This funcion will fly the player when press SPACE.
    void Fly()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive)
        {
            soundManager.jumpingSound.Play();
            animator.SetBool("isFlying", true);                 // Play the bird fly animation.
            Time.timeScale = 1;                                 // For the restart funcion.
            gameManager.PlayerStartPlay();                      // The player is start to play and stop been static.
            rb.velocity = Vector2.up * verticalJumpForce;       // Read the information every time the player press SPACE
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isFlying", false);               // Turn off the bird fly animation when the user doesnt press SPACE anymore.
        }

    }

    // This funcion will give movement for the player.
    // Movement as falling and constant movement on the X axis.
    void RealisticPhysics()
    {
        rb.velocity += Vector2.down * gravity;                  // Using the velocity from the update.
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);    // Create a constant speed horizontal.  
    }


   // This funcion will kill the player when he hit a spike.
    void Die()
    {
        if (playerIsAlive)
        {
            soundManager.gameOverSound.Play();
            explosionEffect.Play();
        }
        playerIsAlive = false;
        sr.color = Color.black;
        //sr.sprite = null;
       // sr.sprite = deadSprite;
        Invoke("VanishPlayer", deleyTime);

    }


    void VanishPlayer()
    {
        Destroy(gameObject);
        gameManager.ShowGameOverCanvas();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (playerIsAlive)
        {
            if (other.gameObject.CompareTag("RightWall"))
            {
                gameManager.AddScore();                         // Add score for the player.
                rightSpikeSpawner.Clear();                      // Clear the spikes from the right wall.
                leftSpikeSpawner.SpawnRandomly();               // Create new spikes on the left wall.
                Flip();                                         // Flip the player to go to the other wall.
            }

            else if (other.gameObject.CompareTag("LeftWall"))
            {
                gameManager.AddScore();                         // Add score for the player.
                leftSpikeSpawner.Clear();                       // Clear the spikes from the left wall.
                rightSpikeSpawner.SpawnRandomly();              // Create new spikes on the left wall.
                Flip();                                         // Flip the player to go to the other wall.
            }
            else if (other.gameObject.CompareTag("Spike"))
            {
                Die();                                          // The player go KABOOM!
            }
        }

    }


}
