using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainMenuUI : MonoBehaviour
{
    //public ColorPicker ColorPicker;

   //public void NewColorSelected(Color color)
   //{
   //    // add code here to handle when a color is selected
   //    MainManager.Instance.TeamColor = color;
   //}

    private void Start()
    {
    //    ColorPicker.Init();
    //    //this will call the NewColorSelected function when the color picker have a color button clicked.
    //    ColorPicker.onColorChanged += NewColorSelected;
    //    ColorPicker.SelectColor(MainManager.Instance.TeamColor); //set the color picker to the current team color
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //public void SaveColorClicked()
    //{
    //    MainManager.Instance.SaveColor();
    //}

    //public void LoadColorClicked()
    //{
    //    MainManager.Instance.LoadColor();
    //    ColorPicker.SelectColor(MainManager.Instance.TeamColor); //update the color picker to the loaded color
    //}

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        //MainManager.Instance.SaveColor();
    }
}
