using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    public GameManager gm;
    private GameVariables gv;
    private string sceneName;

    private int masks;
    public int maskTotal = 0;
    [SerializeField] private Text maskCount;
    [SerializeField] private AudioSource maskCollectSound;

    private int vaccines;
    public int vaccineTotal = 0;
    [SerializeField] private Text vaccineCount;
    [SerializeField] private AudioSource vaccineCollectSound;

    public int checkpointReached = 0;
    private Vector2 checkpointPos = new Vector2(-39.4f, 0f);
    [SerializeField] private AudioSource checkpointSound;

    [SerializeField] private AudioSource finishSound;
    [SerializeField] private AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Transform>();
        sceneName = SceneManager.GetActiveScene().name;

        masks = PlayerPrefs.GetInt("masks", 0);
        vaccines = PlayerPrefs.GetInt("vaccines", 0);
        
        if(PlayerPrefs.GetInt("checkpointReached", 0) == 1)
        {
            checkpointPos.x = PlayerPrefs.GetFloat("checkpointPosX", -39.4f);
            checkpointReached = 1;
            player.transform.position = checkpointPos;
        }

        //gv = GameObject.FindGameObjectWithTag("GV").GetComponent<GameVariables>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10.0f)
        {
            enabled = false;
            
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mask"))
        {
            maskCollectSound.Play();
            Destroy(collision.gameObject);
            masks++;
            MaskCountDisplay();
        }

        if (collision.gameObject.CompareTag("Vaccine"))
        {
            vaccineCollectSound.Play();
            Destroy(collision.gameObject);
            vaccines++;
            //VaccineCountDisplay();
        }

        if (collision.gameObject.CompareTag("Virus"))
        {
            if (masks == 0)
            {
                Death();
            }
            else
            {
                masks--;
                MaskCountDisplay();
            }

        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Spikes");
            Death();
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint Reached");
            checkpointReached = 1;
            if (player.transform.position.x > checkpointPos.x)
            {
                checkpointPos = player.transform.position;
            }
        }

        if (collision.gameObject.name == "Finish")
        {
            if (vaccines == vaccineTotal)
            {
                finishSound.Play();
                Invoke("FinishLevel", 1f);
            }
            else
            {
                //Message to go collect vaccine
            }
        }
    }

    private void MaskCountDisplay()
    {
        maskCount.text = "Mask: " + masks + "/" + maskTotal;
    }

    private void VaccineCountDisplay()
    {
        maskCount.text = "Vaccine: " + vaccines + "/" + vaccineTotal;
    }

    private void Death()
    {        
        deathSound.Play();
        //rb.bodyType = RigidbodyType2D.Static;
        if(checkpointReached == 1)
        {
            PlayerPrefs.SetInt("checkpointReached", 1);
            PlayerPrefs.SetFloat("checkpointPosX", checkpointPos.x);
            PlayerPrefs.SetInt("masks", masks);
            PlayerPrefs.SetInt("vaccines", vaccines);
            
        }

        PlayerPrefs.SetString("level", sceneName);
        
        Debug.Log("Before Animation");
        anim.SetTrigger("death");
        Debug.Log("After Animation");
    }

    private void RestartLevel()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("GameOver");
    }

    private void FinishLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
