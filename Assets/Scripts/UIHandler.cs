using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    public InputField inputField;
    public Text hightScoreText;
    public Text leaderboardText;

    public Canvas menuCanva;
    public Canvas leaderboardCanva;

    private void Start() {
        hightScoreText.text = GameManager.Instance.GetHightScoreText();
    }

    private void UpdateLeaderboard() {
        string leaderboard = "";
        List<Record> records = GameManager.Instance.recordList;
        records.Sort(CompareRecord);

        foreach (Record record in records) {
            leaderboard += $"{record.username} : {record.score}\n";
        }
        leaderboardText.text = leaderboard;
    }



    public void OnStartClicked() {
        string username = inputField.text.Trim();
        if (username.Length > 0) {
            GameManager.Instance.username = username;
            SceneManager.LoadScene(1);
        }
    }

    public void OnLoaderboardClicked() {
        UpdateLeaderboard();

        menuCanva.enabled = false;
        leaderboardCanva.enabled = true;
    }

    public void OnBackClicked() {
        menuCanva.enabled = true;
        leaderboardCanva.enabled = false;
    }

    public void OnExitClicked() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private int CompareRecord(Record x, Record y) {
        return y.score - x.score;
    }
}
