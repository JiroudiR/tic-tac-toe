using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Player
    {
        X, O, Empty
    }
    public Sprite xSprite;
    public Sprite oSprite;
    private Player currentPlayer;
    private int xWins;
    private int oWins;
    private int draws;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = Player.X;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameButtonClicked(GameObject clickedButton)
    {
        GameButton gameButton = clickedButton.GetComponent<GameButton>();
        int row = gameButton.row;
        int col = gameButton.col;
        Debug.Log($"Row: {row}; Col: {col}");
        Image buttonImage = clickedButton.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1;
        buttonImage.color = buttonColor;
        if (buttonImage.sprite == null)
        {
            if (currentPlayer == Player.X)
            {
                buttonImage.sprite = xSprite;
                Debug.Log("Anything");
                currentPlayer = Player.O;
            }
            else
            {
                buttonImage.sprite = oSprite;
                currentPlayer = Player.X;
            }
        }
    }
}
