using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPos : MonoBehaviour
{
    private Transform focusPoint;

    // Start is called before the first frame update
    void Start()
    {
        focusPoint = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        focusPoint.position = Input.mousePosition;
    }
}
