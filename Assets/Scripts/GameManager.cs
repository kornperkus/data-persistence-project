using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string username;

    //All records in database
    public List<Record> recordList = new List<Record>();

    //The highest score in records
    public Record highestRecord = new Record("Name", 0);

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
        LoadRecords();
    }

    public string GetHightScoreText() {
        return $"Best Score : { highestRecord.username} : {highestRecord.score}";
    }

    public void SaveRecords(int score) {
        recordList.Add(new Record(username, score));
        SaveData data = new SaveData(recordList.ToArray());

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    public void LoadRecords() {
        if (File.Exists(dataPath)) {
            string json = File.ReadAllText(dataPath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data == null || data.recordList == null) return;

            if (data.recordList.Length > 0) {
                recordList.Clear();
                foreach (Record record in data.recordList) {
                    recordList.Add(record);
                }
                CalculateHighestRecord();
            }
        }
    }

    private void CalculateHighestRecord() {
        foreach (Record record in recordList) {
            if (record.score > highestRecord.score) {
                highestRecord.username = record.username;
                highestRecord.score = record.score;
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public Record[] recordList;

        public SaveData(Record[] recordList) {
            this.recordList = recordList;
        }
    }
}
