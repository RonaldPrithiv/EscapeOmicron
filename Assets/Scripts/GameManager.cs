using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public bool caught;
    // Start is called before the first frame update
    void Start()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (caught)
        {
            Time.timeScale = 0f;
            foreach (Transform child in canvas.transform)
            {
                Debug.Log("Set Active");
                child.gameObject.SetActive(true);
                
            }
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
            {
                Restart();
            }
        }
    }

    public void Restart()//restart level
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()//go to mainmenu
    {
        SceneManager.LoadScene(0);
    }
}
