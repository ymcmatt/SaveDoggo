using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerTransform;
    private Transform cameraTransform;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = this.transform;
    }

    void Update()
    {
        // cameraTransform = new Vector3(cameraTransform.position.x, cameraTransform.position.y, playerTransform.position.z - 1);
        Vector3 newPos = cameraTransform.position;
        newPos.z = playerTransform.position.z - 5;
        cameraTransform.position = newPos;
    }

}
