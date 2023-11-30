using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyAIState
{
    public void EnterState(EnemyAIController enemy)
    {
        Debug.Log("Entering Patrol State");
       
    }

    public void UpdateState(EnemyAIController enemy)
    {

        enemy.Patrol();
    }

    public void ExitState(EnemyAIController enemy)
    {

        enemy.StopPatrolling();
    }
}

