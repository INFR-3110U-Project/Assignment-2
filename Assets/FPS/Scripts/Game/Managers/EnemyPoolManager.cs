using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public int poolSize = 100;

        private List<GameObject> enemyPool;
        private QuestManager questManager;
        private BoxCollider enemyManagerCollider;
        private int currentIndex = 0;

        void Start()
        {
            InitializePool();
            questManager = FindObjectOfType<QuestManager>();
            enemyManagerCollider = GetComponent<BoxCollider>();
        }

        private void InitializePool()
        {
            enemyPool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }

        private GameObject GetPooledEnemy()
        {
            // Get the next inactive enemy
            GameObject enemy = enemyPool[currentIndex];

            if (!enemy.activeInHierarchy)
            {
                // Set the enemy's position to a random point inside the BoxCollider bounds
                enemy.transform.position = GetRandomPositionInsideCollider();

                enemy.SetActive(true);

                // Move to the next index for the next call
                currentIndex++;
            }

            return enemy;
        }

        private Vector3 GetRandomPositionInsideCollider()
        {
            if (enemyManagerCollider != null)
            {
                // Calculate a random position within the BoxCollider bounds
                Vector3 randomOffset = new Vector3(
                    Random.Range(-enemyManagerCollider.size.x / 2f, enemyManagerCollider.size.x / 2f),
                    0f,
                    Random.Range(-enemyManagerCollider.size.z / 2f, enemyManagerCollider.size.z / 2f)
                );
                return enemyManagerCollider.transform.position + randomOffset;
            }
            else
            {
                // If no BoxCollider is found, return the default position
                return Vector3.zero;
            }
        }

        /// <summary>
        /// check to see if this gameobject is managed by this script
        /// </summary>
        /// <param name="enemy">Game Object</param>
        public void ReturnEnemyToPool(GameObject enemy)
        {
            enemy.SetActive(false);

            // Notify the QuestManager that an enemy is destroyed
            questManager.QuestEnemyDestroyed();
            if (questManager.IsQuestActive)
                GetPooledEnemy();
        }
        public bool IsEnemyInPool(GameObject enemy)
        {
            return enemyPool.Contains(enemy);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !questManager.IsQuestActive)
            {
                questManager.IsQuestActive = true;
                GetPooledEnemy();
            }
        }
    }
}