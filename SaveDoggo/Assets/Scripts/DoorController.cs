using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform door;
    public bool sliding = false;
    public float openAngle = -160f;
    public float openPos = -0.9f;

    // Start is called before the first frame update
    void Start()
    {
        door = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Open()
    {
        float t = 0;
        //SoundController.SC.PlaySfx("OpenDoor");
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            if (sliding)
            {
                float pos = Mathf.Lerp(0, openPos, t / 0.2f);
                door.transform.localPosition =new Vector3(pos, 0, 0);
            }
            else
            {
                float angle = Mathf.Lerp(0, openAngle, t / 0.2f);
                door.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }

            yield return null;
        }
    }


    public IEnumerator Close()
    {
        float t = 0;
        //SoundController.SC.PlaySfx("OpenDoor");
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            if (sliding)
            {
                float pos = Mathf.Lerp(openPos, 0, t / 0.2f);
                door.transform.localPosition = new Vector3(pos, 0, 0);
            }
            else
            {
                float angle = Mathf.Lerp(openPos, 0, t / 0.2f);
                door.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
            yield return null;
        }
    }

    public void Melt(GameObject other)
    {
        Destroy(gameObject, 1);
        ItemInteract flask = other.GetComponent<ItemInteract>();
        if (flask == ItemInteract.pHolding)
        {
            flask.PutDownPlayer();
        }
        else if (flask == ItemInteract.dogHolding)
        {
            flask.PutDownDog();
        }
        Destroy(other, 1);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Flask")
        {
            Melt(other.gameObject);
        }
    }
}
