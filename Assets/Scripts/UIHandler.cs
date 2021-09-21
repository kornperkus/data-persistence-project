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

    private void Start() {
        hightScoreText.text = GameManager.Instance.GetHightScoreText();
    }

    public void OnStartClicked() {
        string username = inputField.text.Trim();
        if (username.Length > 0) {
            GameManager.Instance.username = username;
            SceneManager.LoadScene(1);
        }
    }

    public void OnExitClicked() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
