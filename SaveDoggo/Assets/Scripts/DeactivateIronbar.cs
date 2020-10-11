using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateIronbar : MonoBehaviour
{
    public GameObject iron_door1;
    public GameObject pressure_plate;
    private GameObject plate;

    // Start is called before the first frame update
    void Start()
    {
        plate = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(plate.name);
        if (plate.name == "PressurePlate")
        {
            pressure_plate.SetActive(true);
        }
        else
        {
            iron_door1.SetActive(false);
        }
        //iron_door2.SetActive(true);
    }
}
