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
        Image buttonImage = GetComponent<Image>();
        if (currentPlayer == Player.X)
        {
            buttonImage.sprite = xSprite;
            currentPlayer = Player.O;
        }
        else
        {
            buttonImage.sprite = oSprite;
            currentPlayer = Player.X;
        }
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1;
        buttonImage.color = buttonColor;
    }
}
