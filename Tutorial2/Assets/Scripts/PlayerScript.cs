using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    Animator anim;

    public float speed;

    public Text score;

    public Text win;

    public Text lives;

    private int livesValue = 3;

    private int scoreValue = 0;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" + scoreValue.ToString();
        lives.text = "Lives:" + livesValue.ToString();
        anim = GetComponent<Animator>();
        
        if (scoreValue < 8)
        {
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        }        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))

        {
          anim.SetBool("IsRunning", true);
         } else
         {
             anim.SetBool("IsRunning", false);
         }

         if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
        else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }

    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue < 8)
        {
                    win.text= " ";
        }
            if (collision.collider.tag == "Teleport" && scoreValue >= 3)
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            transform.position = new Vector2(91.3f, -0.82f);
            livesValue = 3;
            lives.text = "Lives:" + livesValue.ToString();
        }
        if (scoreValue == 8)
        {
            win.text = "You win! This game was made by Michael Rodriguez.";
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
            Destroy(this);
        }
        if (livesValue == 0)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
            win.text = "You lose!";
            Destroy(this);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         if (scoreValue == 8)
        {
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        musicSource.loop = false;
        }  
        if (collision.collider.tag == "Ground")
        {
         anim.SetBool("IsJumping", false);   
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                anim.SetBool("IsJumping", true);
            }
        }
    }
}