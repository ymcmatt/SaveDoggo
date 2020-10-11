using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMovement : MonoBehaviour
{
    [SerializeField]
    private Animator ani;
    private bool aniNull;

    public float speed = 10f;

    [SerializeField]
    private Rigidbody rb;

    private int speedHash = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (ani == null)
        {
            aniNull = true;
        }
        else
        {
            aniNull = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!aniNull)
        {
            ani.SetFloat(speedHash, rb.velocity.magnitude);
        }
    }

    public void Move(Vector3 direction, float duration, float s = -1f)
    {
        if (s == -1)
        {
            s = speed;
        }
        StartCoroutine(Moving(direction, duration, s));
    }

    public IEnumerator Moving(Vector3 direction, float duration, float s)
    {
        
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            rb.velocity = direction * s;
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
    public void Rotate(float start, float end, float duration)
    {
        StartCoroutine(Rotating(start, end, duration));
    }

    public IEnumerator Rotating(float start, float end, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            rb.rotation = Quaternion.Euler(new Vector3(0, Mathf.Lerp(start, end, t / duration)));
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}
