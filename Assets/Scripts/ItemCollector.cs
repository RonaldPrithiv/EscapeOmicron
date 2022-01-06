using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int masks = 0;

    [SerializeField] private Text maskCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mask"))
        {
            Destroy(collision.gameObject);
            masks++;
            maskCount.text = "Mask: " + masks;
        }
    }
}
