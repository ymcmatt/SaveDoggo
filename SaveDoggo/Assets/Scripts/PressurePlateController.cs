using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public DoorController door;
    private int on = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            if (on <= 0)
            {
                StartCoroutine(door.Open());
            }
            on++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            if (on <= 1)
            {
                StartCoroutine(door.Close());
            }
            on--;
        }
    }
}
