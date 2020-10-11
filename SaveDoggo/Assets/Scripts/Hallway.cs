using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hallway : MonoBehaviour
{

    public float finalZ;
    public float length;

    public Image fade;
    public Color clear = new Color(0.8f, 0.8f, 0.8f, 0);
    public Color full = new Color(0.8f, 0.8f, 0.8f, 1);

    public Transform player;
    public DogController dog;
    public Transform pursuer;

    public Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        fade.color = clear;
        player = GameObject.FindWithTag("Player").transform;    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.position.z >= finalZ)
        {
            SceneController.StartVictory();
        }
        else if (Mathf.Abs(player.position.z - finalZ) < length )
        {
            fade.color = Color.Lerp(full, clear, Mathf.Abs(player.position.z - finalZ) / length);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (!dog.following)
            {
                dog.following = true;
            }

            float dist = Vector3.Magnitude(player.position - pursuer.position);
            if (dist >= 4f)
            {
                Vector3 pos = player.position;
                pos.z -= 6;
                pursuer.position = pos;
            }

            slide.gameObject.SetActive(false);

        }
    }
}
