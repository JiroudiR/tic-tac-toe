using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] gameButtons;
    public GameObject xWinsUI;
    public GameObject oWinsUI;
    public GameObject catsGameUI;
    public TMP_Text xWinsLabel;
    public TMP_Text oWinsLabel;
    public TMP_Text drawsLabel;
    
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
            buttonColor.a = 0;
            buttonImage.color = buttonColor;
            buttonImage.sprite = null;
        }
        xWinsUI.SetActive(false);
        oWinsUI.SetActive(false);
        catsGameUI.SetActive(false);
    }

    public void UpdateWins()
    {
        xWinsLabel.text = $"X Wins: {FindObjectOfType<GameManager>().xWins}";
        oWinsLabel.text = $"O Wins: {FindObjectOfType<GameManager>().oWins}";
        drawsLabel.text = $"Draws: {FindObjectOfType<GameManager>().draws}";
    }
}
