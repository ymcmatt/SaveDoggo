using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public static bool actionLock = false;
    public float radius = 3f;
    public Transform interactionTransform;

    protected bool inFocus = false;
    bool hasInteracted = false;
    public Transform player;


    public virtual void Interact()
    {
        // This method is meant to be overwritten
        //Debug.Log("Interacting with "+transform.name);
    }

    public virtual void LoseFocus()
    {

    }

    public void OnFocused(Transform playerTransform)
    {
        Debug.Log(this + " is in focus");
        inFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void DeFocused()
    {
        Debug.Log(this + " is out of focus");
        inFocus = false;
        player = null;
        hasInteracted = false;
        LoseFocus();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            hasInteracted = true;
            Interact();

        }
        //float distance = Vector3.Distance(player.position, interactionTransform.position);
        //if (distance <= radius)
        //{
        //    Interact();
        //    hasInteracted = true;

        //}
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            OnFocused(other.gameObject.transform);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            DeFocused();
        }
    }
}
