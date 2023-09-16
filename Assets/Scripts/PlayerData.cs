using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerInfoArray
{
    public PlayerInfo[] playerInfos;
}

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance;
    public static PlayerData GetData { get { Init(); return instance; } }

    private string path;
    List<PlayerInfo> playerInfoList;

    private void Start()
    {
        Init();
        path = Application.dataPath + "/Resources/Json/PlayerData.json";
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("PlayerData");
            if (go == null)
            {
                go = new GameObject { name = "PlayerData" };
                go.AddComponent<PlayerData>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<PlayerData>();
        }
    }

    public void SavePlayerInfo(PlayerInfo info)
    {

    }

    public PlayerInfo[] GetPlayerInfo()
    {
        PlayerInfo[] infos = null;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            infos = JsonUtility.FromJson<PlayerInfoArray>(json).playerInfos;
        }

        return infos;
    }
}
