using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform[] targets;
    public GameObject vision,GM;
    public float speed = 5;
    private bool switchFlip=false;
    public int i=0,j=0;
   public  bool playerNotFound,patrol=false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in targets)
            i++;
        //playerNotFound = true;
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    // Update is called once per frame
    void Update()
    {
        if (i != 0)
        {
            if (patrol && playerNotFound)
                logicNpc();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNotFound = false;
            Debug.LogWarning(playerNotFound);

            //GM.GetComponent<GameManager>().caught = true;
            print("isolate");
        }
    }

    void logicNpc()
    {
        float step = speed * Time.deltaTime;
        if(j<i)
        {
            transform.position = Vector2.MoveTowards(transform.position, targets[j].position, step);     
        }        

        if (Vector2.Distance(transform.position, targets[j].position) < 2)
        {
            
            Vector3 dir = targets[j].transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            gameObject.GetComponentInChildren<SpriteRenderer>().flipY=switchFlip;

            j++;
            
             if(j%2==0)
             switchFlip=true;
             else
             switchFlip = false;





        }
        if (j == i)
            j = 0;
        
    }
}
