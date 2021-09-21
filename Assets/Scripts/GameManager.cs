using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string username;
    public string oldHightScoreUsername;
    public int oldHightScore;

    public static GameManager Instance;

    private string dataPath;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        dataPath = Application.persistentDataPath + "savedata.json";
        LoadHightScore();
    }

    public string GetHightScoreText() {
        if (oldHightScoreUsername == "")
            return "Best Score : Name : 0";
        else
            return $"Best Score : { oldHightScoreUsername} : {oldHightScore}";
    }

    public void SaveHightScore(int score) {
        if (score < oldHightScore) return;

        SaveData data = new SaveData(username, score);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public void LoadHightScore() {
        if (File.Exists(dataPath)) {
            string json = File.ReadAllText(dataPath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.username != null && data.username != "") {
                oldHightScoreUsername = data.username;
                oldHightScore = data.hightScore;
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string username;
        public int hightScore;

        public SaveData(string username, int hightScore) {
            this.username = username;
            this.hightScore = hightScore;
        }
    }
}
