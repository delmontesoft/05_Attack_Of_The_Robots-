using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 20f)] float secondsBetweenSpawns = 2f;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Text enemyScoreText;

    int enemyScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyScoreText.text = enemyScore.ToString();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)        //TODO do until some condition (win or lose)
        {
            AddEnemyScore(); 
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddEnemyScore()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();
    }
}
