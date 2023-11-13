using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class QuestManager : MonoBehaviour
    {
        public EnemyPoolManager enemyPoolManager;
        public GameObject wallToRemove;
        public int EnemiesToDestroy = 10;
        

        public bool IsQuestActive
        {
            get
            {
                return questActive;
            }
            set
            {
                questActive = value;
                if (IsQuestActive)                
                    displayMessage("Kill all enemies in open space to advance to Boss");                
            }
        }

        private bool questActive;

        /// <summary>
        /// Called when Quest Enemy is Destroyed
        /// </summary>
        public void QuestEnemyDestroyed()
        {
            // Called when an enemy is destroyed
            EnemiesToDestroy--;            

            if (EnemiesToDestroy <= 0)
            {
                displayMessage("Quest Completed. You may now fight the boss!",5f);
                RemoveWall();
                IsQuestActive = false;
            }
            else
                displayMessage(string.Format("Destroyed Enemy, {0} more to go.", EnemiesToDestroy));
        }
        // Remove the wall or trigger the next stage
        private void RemoveWall()
        {
            // Remove the wall or trigger the next stage
            if (wallToRemove != null)
            {
                Destroy(wallToRemove);
                questActive = false;
            }

        }
        /// <summary>
        /// Display message immediately.
        /// </summary>
        /// <param name="message">Text to display</param>
        private void displayMessage(string message)
        {
            DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
            displayMessage.Message = message;
            displayMessage.DelayBeforeDisplay = 0.0f;
            EventManager.Broadcast(displayMessage);
        }
        /// <summary>
        /// Display message immediately.
        /// </summary>
        /// <param name="message">Text to display</param>
        /// <param name="delay">seconds to delay</param>
        private void displayMessage(string message, float delay)
        {
            DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
            displayMessage.Message = message;
            displayMessage.DelayBeforeDisplay = delay;
            EventManager.Broadcast(displayMessage);
        }
    }
}
