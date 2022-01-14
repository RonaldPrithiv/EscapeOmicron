using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject attackUI,closeNPC;
<<<<<<< Updated upstream
=======
    private Animator anim;
>>>>>>> Stashed changes
    public bool canAttack=false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack && Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        attackUI.GetComponent<SpriteRenderer>().enabled = false;
<<<<<<< Updated upstream
=======
        anim.SetTrigger("attack");
>>>>>>> Stashed changes
        canAttack = false;
        Destroy(closeNPC);
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Virus_Back"))
        {
            closeNPC = other.gameObject.transform.parent.gameObject;
           attackUI.GetComponent<SpriteRenderer>().enabled = true;
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("Virus_Back"))
        {
            attackUI.GetComponent<SpriteRenderer>().enabled = false;
            canAttack = false;
        }
    }

   
}
