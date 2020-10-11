using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrondoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform scientist;
    public GameObject iron_door1;
    public GameObject iron_door2;
    public GameObject pressurePlate;
    private AudioSource audio;
    private bool isPlayed = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPlayed)
        {
            if (other.tag == "Player")
            {
                pressurePlate.SetActive(true);
                iron_door1.SetActive(true);
                iron_door2.SetActive(true);
                audio.Play();
                isPlayed = true;
            }
        }
    }
}
