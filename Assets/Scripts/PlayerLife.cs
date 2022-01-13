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
    private int masks = 0;
    [SerializeField] private Text maskCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Transform>();
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
            Destroy(collision.gameObject);
            masks++;
            MaskCountDisplay();

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
    }

    private void MaskCountDisplay()
    {
        maskCount.text = "Mask: " + masks;
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Debug.Log("Before Animation");
        anim.SetTrigger("death");
        Debug.Log("After Animation");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}