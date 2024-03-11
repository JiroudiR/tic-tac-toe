using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GRID_SIZE = 3;
    public enum Player
    {
        X, O, Empty
    }
    public Sprite xSprite;
    public Sprite oSprite;
    private Player currentPlayer;
    private Player[,] squares = new Player[GRID_SIZE,GRID_SIZE];
    private int numTurns;
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        numTurns = 0;
        currentPlayer = Player.X;
        for (int row = 0; row < squares.GetLength(0); row++)
        {
            for (int col = 0; col < squares.GetLength(1); col++)
            {
                squares[row, col] = Player.Empty;
                Debug.Log($"Row: {row} Col: {col} {squares[row,col]}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            FindObjectOfType<UIManager>().ResetGameButtons();
            NewGame();
        }
    }

    public void GameButtonClicked(GameObject clickedButton)
    {
        GameButton gameButton = clickedButton.GetComponent<GameButton>();
        int row = gameButton.row;
        int col = gameButton.col;
        Image buttonImage = clickedButton.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1;
        buttonImage.color = buttonColor;
        if (buttonImage.sprite == null)
        {
            numTurns++;
            if (currentPlayer == Player.X)
            {
                buttonImage.sprite = xSprite;
                squares[row, col] = Player.X;
            }
            else
            {
                buttonImage.sprite = oSprite;
                squares[row, col] = Player.O;
            }
            CheckWin();
            currentPlayer = 1 - currentPlayer;
        }
    }

    private void CheckWin()
    {
        gameOver = false;
        //Check each row to see if player has won
        int row = 0;
        while (row < squares.GetLength(0) && !gameOver)
        {
            if (CheckRow(row))
            {
                Debug.Log(currentPlayer + " Wins!");
                gameOver = true;
            }
            row++;
        }
        //Check each column to see if player has won
        if (!gameOver)
        {
            int col = 0;
            while (col < squares.GetLength(1) && !gameOver)
            {
                if (CheckCol(col))
                {
                    Debug.Log(currentPlayer + " Wins!");
                    gameOver = true;
                }
                col++;
            }
        }
        //Check diagonals to see if player has won
        if (!gameOver)
        {
            if (squares[0, 0] == currentPlayer
            && squares[1, 1] == currentPlayer
            && squares[2, 2] == currentPlayer)
            {
                Debug.Log(currentPlayer + " Wins!");
                gameOver = true;
            }
        }
        if (!gameOver)
        {
            if (squares[0, 2] == currentPlayer
            && squares[1, 1] == currentPlayer
            && squares[2, 0] == currentPlayer)
            {
                Debug.Log(currentPlayer + " Wins!");
                gameOver = true;
            }
        }
        //Check for cats game
        if (!gameOver)
        {
            if (numTurns == 9)
            {
                Debug.Log("Cats Game");
                gameOver = true;
            }
        }
    }

    private bool CheckRow(int row)
    {
        bool victory = true;
        for (int col = 0; col < squares.GetLength(1); col++)
        {
            if (squares[row, col] != currentPlayer)
            {
                victory = false;
            }
        }
        return victory;
    }

    private bool CheckCol(int col)
    {
        bool victory = true;
        for (int row = 0; row < squares.GetLength(0); row++)
        {
            if (squares[row, col] != currentPlayer)
            {
                victory = false;
            }
        }
        return victory;
    }
}
