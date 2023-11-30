using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistance> dataObjects;
    private DataFileHandler dataHandler;
    public static DataPersistanceManager instance { get; private set; }


    private void Awake()
    {
        //throw error if too much is found
        if (instance != null)
        {
            Debug.LogError("Found more than one Persistance Manager in the scene.");
        }

        instance = this;
    }

    //load game on start
    private void Start()
    {
        this.dataHandler = new DataFileHandler(Application.persistentDataPath, fileName);
        this.dataObjects = FindAllData();
        LoadGame();
    }


    //start game with no previous data
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    //load in previous game data
    public void LoadGame()
    {
        //load data
        this.gameData = dataHandler.Load();

        //default to new game if there's no data to load
        if (this.gameData == null)
        {
            Debug.Log("No data found. Starting a new game.");
            NewGame();
        }

        //load any saved data on file
        foreach (IDataPersistance dpo in dataObjects)
        {
            dpo.LoadData(gameData);
        }
        Debug.Log("Loaded Data. Score was: " + gameData.currentScore);
    }

    //save current game data
    public void SaveGame()
    {
        //save data to file
        foreach (IDataPersistance dpo in dataObjects)
        {
            dpo.SaveData(ref gameData);
        }
        Debug.Log("Saved Data. Score is: " + gameData.currentScore);

        //save the game data to a file
        dataHandler.Save(gameData);
    }

    //save game on close
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllData()
    {
        IEnumerable<IDataPersistance> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataObjects);
    }
}
