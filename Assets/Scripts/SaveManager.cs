using System;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    [HideInInspector] public UserData userData;
    private string jsonpath;

    // Start is called before the first frame update
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
        userData = JsonUtility.FromJson<UserData>(File.ReadAllText(jsonpath));
    }

    // Saves before disabling
    void OnDisable() { SaveDataJSON(userData); }

    // Saves the user data with new values
    public void SaveDataJSON(UserData save) { Debug.LogWarning("hi"); File.WriteAllText(jsonpath, JsonUtility.ToJson(save, true)); }
}

// User data json serializable class
[Serializable]
public class UserData
{
    public string[] collectibles;
    public Vector3 latestPlayerPosition;
    public EnemyAsData[] enemies;
}

[Serializable]
public class EnemyAsData
{
    public string name;
    public int hp;
    public Vector3 latestEnemyPosition;
}