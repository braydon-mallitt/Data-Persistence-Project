using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText_Main;   

    private void Start()
    {
        if (MainManager.Instance != null)
        highScoreText_Main.text = "High Score: " + MainManager.Instance.highScore;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        
    }
}
