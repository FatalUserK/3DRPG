using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    bool isFocus = false;
    Transform player;

    public float radius = 3f;
    public Transform interactionTransform;


    bool hasInteracted = false;

    public virtual void Interact()
    {
        // scripts go here
        Debug.Log("INTERACTION: " + gameObject + "\nLOCATION: " + interactionTransform.position);

    }


    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius && !hasInteracted)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
}
