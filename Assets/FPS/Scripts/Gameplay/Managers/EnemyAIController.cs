using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private void Start()
    {
        
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyAIController aiController = enemy.GetComponent<EnemyAIController>();
        
    }
}

public class EnemyAIController : MonoBehaviour
{
    private IEnemyAIState currentState;
    public float patrolSpeed = 3f;

    private void Start()
    {
        SetState(new PatrolState()); 
    }

    private void Update()
    {
        currentState.UpdateState(this); 
    }

    public void SetState(IEnemyAIState state)
    {
        currentState?.ExitState(this); 
        currentState = state;
        currentState.EnterState(this); 
    }

    public void Patrol()
    {
        transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime);
    }

    public void StopPatrolling()
    {
        
    }
}
