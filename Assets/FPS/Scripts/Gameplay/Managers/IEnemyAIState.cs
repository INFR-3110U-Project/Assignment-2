using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAIState
{
    void EnterState(EnemyAIController enemy);
    void UpdateState(EnemyAIController enemy);
    void ExitState(EnemyAIController enemy);
}
