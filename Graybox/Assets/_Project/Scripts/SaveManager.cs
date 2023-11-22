using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The manager for saving and loading games. Stores important values in memory, such as the number of unlocked levels and whether
// the user wants music/sound or not. The save manager should NOT be placed in any scene as it is loaded automatically on launch.
// The save manager also stays loaded when loading a new level.
// Events should be used to call the SaveGame() and LoadGame() functions in this manager.
public class SaveManager : MonoBehaviour
{
    public static GameObject saveManagerPrefab;
    public static SaveManager instance {  get; set; }
    public int unlockedLevels = 1;
    public bool music = true;
    public bool sound = true;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static private void LoadManager()
    {
        // First, we load the save manager into memory.
        saveManagerPrefab = Resources.Load("SaveManager") as GameObject;
        if(saveManagerPrefab == null)
        {
            // Some error handling. The save manager should always be in the heirarchy in the resources folder.
            throw new ArgumentException("CRITICAL ERROR! Can't find the save manager prefab in Project/Prefabs/Resources!");
        }
        // Then we place it into the game world.
        saveManagerPrefab = Instantiate(saveManagerPrefab);
        saveManagerPrefab.name = "SaveManager";

        // We don't want to destroy the save manager on load, since we want to access it whenever we like during gameplay.
        GameObject.DontDestroyOnLoad(saveManagerPrefab);
    }
    void Start()
    {
        LoadGame();
    }

    private static int timesAwaken = 0;
    private void Awake()
    {
        timesAwaken++;
        if(timesAwaken >= 2)
        {
            Debug.Log("WARNING: There can only be one save manager. Please delete the duplicate in your scene.");
            Debug.Log("Deleting clone save manager.");
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        ES3.CreateBackup("save.dat");
        ES3.Save("unlockedLevels", unlockedLevels);
        ES3.Save("sound", sound);
        ES3.Save("music", sound);
        ES3.CreateBackup("save.dat");
#if UNITY_EDITOR
        Debug.Log("Saved game. Unlocked Levels - " + unlockedLevels);
#endif
    }

    public void LoadGame()
    {
        try {
            unlockedLevels = ES3.Load("unlockedLevels", 1);
            music = ES3.Load("music", true);
            sound = ES3.Load("sound", true);
        }
        catch
        {
            if(ES3.RestoreBackup("save.dat")) {
                #if UNITY_EDITOR 
                Debug.Log("Save file was corrupted. Restored Successfully.");
                #endif
                ES3.CreateBackup("save.dat");
                LoadGame();
            }
            else {
                #if UNITY_EDITOR
                Debug.Log("Save File was corrupted. Restore failed. Deleting corrupted file.");
                #endif
                ES3.DeleteFile("save.dat");
            }
        }
        
#if UNITY_EDITOR
        Debug.Log("Loaded game. Unlocked Levels - " + unlockedLevels);
#endif
    }

// Increments the level unlocked counter by one.
    public void SetUnlockedLevels(){
        unlockedLevels += 1;
    }
}
