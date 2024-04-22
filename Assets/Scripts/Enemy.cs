using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (!GameManager.Instance) return;

        // agent.SetDestination(GameManager.Instance.playerObject.transform.position);
    }
}
