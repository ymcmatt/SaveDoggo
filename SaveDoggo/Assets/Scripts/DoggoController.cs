using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    public Transform scientist;
    private Transform doggo;
    public float followSpeed = 0.01f;
    Vector3 scientistPos;
    // Start is called before the first frame update
    void Start()
    {
        doggo = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        scientistPos = scientist.transform.position;
        Vector3 modifiedPos = new Vector3(scientistPos.x - 3, scientistPos.y, scientistPos.z - 3);
        doggo.position = Vector3.MoveTowards(doggo.position, modifiedPos, followSpeed);
        doggo.LookAt(scientist.transform);
    }
}
