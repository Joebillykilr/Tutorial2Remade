using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text scoreText;
    private int score;
    private int lives;
    public Text winText;
    public Text LivesText;
    public GameObject player;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        SetScoreText ();
        SetLivesText ();
        winText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        musicSource.clip = musicClipOne;
        musicSource.Play();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            score = score + 1;
            Destroy(collision.collider.gameObject);
            SetScoreText ();
        }
        else if (collision.collider.tag == ("Enemy"))
        {
            Destroy(collision.collider.gameObject);
            lives = lives - 1;  
            SetLivesText();
        }
    }

    void SetScoreText ()
    {
        scoreText.text = "Coins Collected: " + score.ToString ();
        if (score == 4) 
        {
            transform.position = new Vector3(50.0f, 0.5f, 0.0f);
            lives = 3;
            SetLivesText();
        }

        if (score >= 8)
        {
            Destroy(this.gameObject);
            winText.text = "You Win! Game created by Alexander Shopovick";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    void SetLivesText ()
    {
        LivesText.text = "Lives: " + lives.ToString ();
        if (lives == 0)
        {
            Destroy(this.gameObject);
            winText.text = "You Lose...";

            if (Input.GetKey("escape"))
            {
            Application.Quit();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
