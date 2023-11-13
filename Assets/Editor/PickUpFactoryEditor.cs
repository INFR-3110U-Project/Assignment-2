/// Jayce Lovell 100775118
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(PickupFactory))]
public class PickupFactoryEditor : Editor
{
    private PickupFactory pickupFactory;
    private int selectedPickupIndex;

    // Create a dictionary to map prefab names to pickup types
    private Dictionary<string, string> prefabToPickupTypeMapping = new Dictionary<string, string>();

    private void OnEnable()
    {
        pickupFactory = (PickupFactory)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("PickUpDataBase", EditorStyles.boldLabel);

        // Dropdown to select pickup type
        selectedPickupIndex = EditorGUILayout.Popup("Select Pickup Type", selectedPickupIndex, pickupFactory.pickupTypes.ToArray());

        if (GUILayout.Button("Create Pickup"))
        {
            if (selectedPickupIndex >= 0 && selectedPickupIndex < pickupFactory.pickupTypes.Count)
            {
                string selectedPickupType = pickupFactory.pickupTypes[selectedPickupIndex];
                GameObject newPickup = pickupFactory.CreatePickup(selectedPickupType);

                // had this for testing but only the factory is spawning
                //if (newPickup != null)
                //{
                //    Instantiate(newPickup, pickupFactory.transform.position, Quaternion.identity);
                //}
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Drag and Drop Prefabs", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Drag and drop prefabs here to add them to the factory.", MessageType.Info);

        for (int i = 0; i < pickupFactory.pickupTypes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(pickupFactory.pickupTypes[i], GUILayout.Width(120));

            GameObject prefab = (GameObject)EditorGUILayout.ObjectField(pickupFactory.pickupPrefabs[pickupFactory.pickupTypes[i]], typeof(GameObject), false);

            pickupFactory.AddPrefab(pickupFactory.pickupTypes[i], prefab);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Load Pickups"))
        {
            // this method is to open a pop up
            //string folderPath = EditorUtility.OpenFolderPanel("Load PickUp Folder", "", "");
            // Specify the path to the "Pickups" folder
            string folderPath = "Assets/FPS/Prefabs/Pickups";

            if (!string.IsNullOrEmpty(folderPath))
            {
                string[] prefabPaths = Directory.GetFiles(folderPath, "*.prefab");
                foreach (string path in prefabPaths)
                {
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab != null)
                    {
                        // Add the prefab to the factory without a mapping
                        string prefabName = Path.GetFileNameWithoutExtension(path);
                        pickupFactory.AddPrefab(prefabName, prefab);
                    }
                }
            }
        }

        DrawDefaultInspector();
    }
}