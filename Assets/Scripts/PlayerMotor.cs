using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    // Start is called before the first frame update

    Transform target = null;
    NavMeshAgent agent;
    private object quaternion;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        Debug.Log("Following Target: " + newTarget + "");
        agent.stoppingDistance = newTarget.radius * 0.8f;
        target = newTarget.transform;
        agent.updateRotation = true;
    }

    public void StopFollowing(Interactable newTarget = null)
    {
        Debug.Log("No Longer Following Target: " + newTarget);
        agent.stoppingDistance = 0;
        target = null;
        agent.updateRotation = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
