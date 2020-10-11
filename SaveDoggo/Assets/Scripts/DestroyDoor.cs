using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyDoor : MonoBehaviour
{
    private GameObject dor;
    private Transform door;
    public Transform potion;
    private float distance;
    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {
        door = this.transform;
        dor = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //distance = door.position.z - potion.position.z;
        //Debug.Log(distance);
        //if ((door.position.z - potion.position.z) <= 0.2f)
        //{
        //    Destroy(gameObject, 1);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Flask1")
        {
            //foreach (var comp in dor.GetComponents<Component>())
            //{
            //    if (!(comp is Transform))
            //    {
            //        Destroy(comp);
            //    }
            //}
            Destroy(gameObject, 1);
            displayText.text = "";

        }
    }
}
