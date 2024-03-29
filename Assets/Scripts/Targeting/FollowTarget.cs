using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }

        if(target != null && agent != null)
        {
            agent.destination = target.position;
        }        
    }
}
