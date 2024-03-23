using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string jsonpath;
    private UserData saveData;

    void Start()
    {
        // Only one JsonManager on scene.
        if (!Instance) Instance = this;
        else { Destroy(gameObject); return; }

        // Default status
        jsonpath = $"{Application.persistentDataPath}/userdata.json";
        // UserData defaultData = JsonUtility.FromJson<UserData>(Resources.Load<TextAsset>("DefaultPlayerData").text);

        // Create user json if the file doesnt exist
        if (!File.Exists(jsonpath)) SaveDataJSON(new UserData());

        // Load the user's json file
        saveData = JsonUtility.FromJson<UserData>(File.ReadAllText(jsonpath));
    }

    // Saves the user data with new values
    public void SaveDataJSON(UserData save, bool saveTransform = false)
    {
        // (Optionally) Sets player transform
        if (saveTransform)
        {
            Transform playerTF = GameObject.Find("Player").transform.Find("Knight D Pelegrini");
            saveData.latestPlayerPosition = playerTF.position;
            saveData.latestPlayerRotation = playerTF.rotation;
        }

        // Saves to persistentdatapath
        save ??= saveData;
        File.WriteAllText(jsonpath, JsonUtility.ToJson(save, true));
    }

    // Adds a collectible to the private savedata list
    public void AddCollectible(string collectibleName)
    {
        saveData.collectibles = saveData.collectibles.Append(collectibleName).ToArray();
    }
}

// User data json serializable class
[Serializable]
public class UserData
{
    public string[] collectibles;
    public Vector3 latestPlayerPosition;
    public Quaternion latestPlayerRotation;
    public EnemyAsData[] enemies;
}

[Serializable]
public class EnemyAsData
{
    public string name;
    public int hp;
    public Vector3 latestEnemyPosition;
}