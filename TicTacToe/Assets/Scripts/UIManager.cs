using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject[] gameButtons;
    
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ResetGameButtons()
    {
        foreach (GameObject gameButton in gameButtons)
        {
            Image buttonImage = gameButton.GetComponent<Image>();
            Color buttonColor = buttonImage.color;
            buttonColor.a = 1;
            buttonImage.color = buttonColor;
        }
    }
}