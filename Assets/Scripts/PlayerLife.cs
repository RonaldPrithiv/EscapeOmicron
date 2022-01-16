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
    [SerializeField] public GameVariables gv;
    public Canvas dialogue;
    public Vector2 startingPos;
    private string sceneName;

    private int masks = 0;
    public int maskTotal = 0;
    [SerializeField] private Text maskCount;
    [SerializeField] private AudioSource maskCollectSound;

    private int vaccines = 0;
    public int vaccineTotal = 0;
    [SerializeField] private Text vaccineCount;
    [SerializeField] private AudioSource vaccineCollectSound;

    public bool checkpointReached = false;
    private Vector2 checkpointPos;
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
        
        gv = GameObject.FindGameObjectWithTag("GV").GetComponent<GameVariables>();

        checkpointReached = gv.checkpointReached;

        Debug.Log("CHECKPOINT: " + checkpointReached);

        if(checkpointReached)
        {
            masks = gv.masks;
            player.transform.position = gv.checkpointPos;
        }
        else
        {   
            player.transform.position = startingPos;
        }

        dialogue.enabled = false;
        
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
            checkpointReached = true;
            gv.checkpointReached = true;
            gv.masks = masks;
            if (player.transform.position.x > checkpointPos.x)
            {
                checkpointPos = player.transform.position;
                gv.checkpointPos = checkpointPos;
            }
        }

        if (collision.gameObject.name == "Finish")
        {
            if (vaccines == vaccineTotal)
            {
                finishSound.Play();
                dialogue.enabled = true;
                //Invoke("FinishLevel", 1f);
            }
            else
            {
                //Message to go collect vaccine
            }
        }
    }

    private void MaskCountDisplay()
    {
        maskCount.text = "Mask: " + masks;
    }

    private void VaccineCountDisplay()
    {
        maskCount.text = "Vaccine: " + vaccines + "/" + vaccineTotal;
    }

    private void Death()
    {        
        deathSound.Play();
        //rb.bodyType = RigidbodyType2D.Static;
        if(checkpointReached)
        {
            gv.checkpointReached = true;
            gv.checkpointPos = checkpointPos;
            gv.masks = masks;   
        }

        PlayerPrefs.SetString("level", sceneName);
        
        Debug.Log(PlayerPrefs.GetString("level"));
        anim.SetTrigger("death");
        Debug.Log("After Animation");
    }

    private void RestartLevel()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("GameOver");
    }

    public void FinishLevel()
    {
        gv.checkpointPos = new Vector2(0,0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
