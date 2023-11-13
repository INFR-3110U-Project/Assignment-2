using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalc : MonoBehaviour
{
    int currentScore;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentScore;
    }

    //modify score with observer pattern
    void OnEnable()
    {
        Unity.FPS.AI.EnemyController.onEnemyDamage += UpdateScore;
    }

    void OnDisable()
    {
        Unity.FPS.AI.EnemyController.onEnemyDamage -= UpdateScore;
    }

    void UpdateScore()
    {
        currentScore += 100;
    }
}
