using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target.transform);
        transform.position = new Vector3 (target.transform.position.x,target.transform.position.y, transform.position.z);//follows the player in the x and y axis
    }
}
