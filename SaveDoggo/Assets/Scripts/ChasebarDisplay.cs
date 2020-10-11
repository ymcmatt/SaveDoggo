using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChasebarDisplay : MonoBehaviour
{
    private Slider chaseBar;
    public Transform pursuer;
    public Transform scientist;
    private float distance;
    public float initial_dis;
    private float proximity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        chaseBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pursuer != null)
        {
            distance = scientist.position.z - pursuer.position.z;
            proximity = (initial_dis - distance) / initial_dis;
            chaseBar.value = proximity;
        }
        else
        {
            chaseBar.enabled = false;
        }
        
    }
}
