///Jayce Lovell 100775118
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class PickupFactory : MonoBehaviour
{
    public Dictionary<string, GameObject> pickupPrefabs = new Dictionary<string, GameObject>();

    public List<string> pickupTypes = new List<string>();

    public GameObject defaultPrefab; // In case no valid prefab is found

    public GameObject CreatePickup(string type)
    {
        if (pickupPrefabs.TryGetValue(type, out GameObject prefab))
        {
            // Access the player's position and forward direction
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerPosition = playerTransform.position;
            Vector3 playerForward = playerTransform.forward;

            // Calculate the position in front of the player
            Vector3 spawnPosition = playerPosition + playerForward * 2.0f; // Adjust the distance as needed

            // Instantiate the chosen prefab at the calculated position
            GameObject pickup = Instantiate(prefab, spawnPosition, Quaternion.identity);

            return pickup;
        }

        // If the type is not found, return a default prefab in front of the player
        if (defaultPrefab != null)
        {
            // Access the player's position and forward direction
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 playerPosition = playerTransform.position;
            Vector3 playerForward = playerTransform.forward;

            // Calculate the position in front of the player
            Vector3 spawnPosition = playerPosition + playerForward * 2.0f; // Adjust the distance as needed

            // Instantiate the default prefab at the calculated position
            GameObject defaultPickup = Instantiate(defaultPrefab, spawnPosition, Quaternion.identity);

            return defaultPickup;
        }

        // Handle the case where there's no default prefab set (return null, log an error, or handle it as you prefer)
        Debug.LogError("No default prefab set for the PickupFactory.");
        return null;
    }

    public void AddPrefab(string type, GameObject prefab)
    {
        if (!pickupPrefabs.ContainsKey(type))
        {
            pickupTypes.Add(type); // Add the type to the list if not already added
        }

        pickupPrefabs[type] = prefab;
    }
}
