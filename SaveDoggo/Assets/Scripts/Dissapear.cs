using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
    public float wait = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Begone()
    {
        yield return new WaitForSeconds(wait);
        this.gameObject.SetActive(false);
    }
}
