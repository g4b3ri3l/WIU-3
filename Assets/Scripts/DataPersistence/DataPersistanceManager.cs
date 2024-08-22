using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance { get; private set;  }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the Scene");
        }
        instance = this;

        
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initialising data to defaults");
            NewGame();
        }
        string sceneName = SceneManager.GetActiveScene().name;



        if (sceneName == "level1 1")
        {
            if (!gameData.level1)
            gameData.playerPos = new Vector3(-122.66f, -54.59f, 0f);
            
        }
        else if (sceneName == "level2 2")
        {
            if (!gameData.level2)
                gameData.playerPos = new Vector3(-110.4f, -58.2f, 0f);
        }
        else if (sceneName == "level3 3")
        {
            if (!gameData.level3)
                gameData.playerPos = new Vector3(22.32f, -84.33f, 0f);
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void ContinueScene()
    {
        SceneManager.LoadScene(this.gameData.levelname);
        LoadGame();
    }

    public void SaveGame()
    {

        string sceneName = SceneManager.GetActiveScene().name;
        gameData.levelname = sceneName;

        

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }



    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = 
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
