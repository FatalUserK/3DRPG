using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Interactable focus;

    public LayerMask movementMask;
    public int cursorRange = 150;
    public int movementRange = 150;

    Camera cam;
    PlayerMotor motor;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            RemoveFocus();

            if (Physics.Raycast(ray, out hit, movementRange, movementMask))
            {
                motor.MoveToPoint(hit.point);
                Debug.Log("raycast hit " + hit.collider.name + " at " + hit.point);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, cursorRange))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Debug.Log("raycast hit " + hit.collider.name + " at " + hit.point);
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                else
                {
                    RemoveFocus();
                }
            }
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            { focus.OnDefocused(); }

            focus = newFocus;
            motor.FollowTarget(newFocus);
            Debug.Log("Focus Set: " + newFocus);
        }
        newFocus.OnFocused(transform);

    }
    void RemoveFocus()
    {
        Debug.Log("Losing Focus on: " + focus);
        if (focus != null)
        { focus.OnDefocused(); }

        focus = null;
        motor.StopFollowing();
    }
}
