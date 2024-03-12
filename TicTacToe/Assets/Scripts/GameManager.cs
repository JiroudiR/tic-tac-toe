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
    public GameObject xWinsUI;
    public GameObject oWinsUI;
    public GameObject catsGameUI;
    public int xWins = 0;
    public int oWins = 0;
    public int draws = 0;
    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        // Reset User Interface
        FindObjectOfType<UIManager>().ResetGameButtons();
        // Initialize our game variables
        gameOver = false;
        numTurns = 0;
        currentPlayer = Player.X;
        // Sets the data to empty
        InitializeData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeData()
    {
        for (int row = 0; row < squares.GetLength(0); row++)
        {
            for (int col = 0; col < squares.GetLength(1); col++)
            {
                squares[row, col] = Player.Empty;
            }
        }
    }

    public void GameButtonClicked(GameObject clickedButton)
    {
        if (!gameOver)
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
                GameOver(currentPlayer);
            }
            row++;
            if (currentPlayer == Player.X && gameOver)
            {
                xWins++;
                xWinsUI.SetActive(true);
            } else if (currentPlayer == Player.O && gameOver)
            {
                oWins++;
                oWinsUI.SetActive(true);
            }
        }
        //Check each column to see if player has won
        if (!gameOver)
        {
            int col = 0;
            while (col < squares.GetLength(1) && !gameOver)
            {
                if (CheckCol(col))
                {
                    GameOver(currentPlayer);
                }
                col++;
            }
            if (currentPlayer == Player.X && gameOver)
            {
                xWins++;
                xWinsUI.SetActive(true);
            }
            else if (currentPlayer == Player.O && gameOver)
            {
                oWins++;
                oWinsUI.SetActive(true);
            }
        }
        //Check diagonals to see if player has won
        if (!gameOver)
        {
            if (squares[0, 0] == currentPlayer
            && squares[1, 1] == currentPlayer
            && squares[2, 2] == currentPlayer)
            {
                GameOver(currentPlayer);
            }
            if (currentPlayer == Player.X && gameOver)
            {
                xWins++;
                xWinsUI.SetActive(true);
            }
            else if (currentPlayer == Player.O && gameOver)
            {
                oWins++;
                oWinsUI.SetActive(true);
            }
        }
        if (!gameOver)
        {
            if (squares[0, 2] == currentPlayer
            && squares[1, 1] == currentPlayer
            && squares[2, 0] == currentPlayer)
            {
                GameOver(currentPlayer);
            }
            if (currentPlayer == Player.X && gameOver)
            {
                xWins++;
                xWinsUI.SetActive(true);
            }
            else if (currentPlayer == Player.O && gameOver)
            {
                oWins++;
                oWinsUI.SetActive(true);
            }
        }
        //Check for cats game
        if (!gameOver)
        {
            if (numTurns == 9)
            {
                draws++;
                GameOver(Player.Empty);
                catsGameUI.SetActive(true);
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

    private void GameOver(Player winner)
    {
        FindObjectOfType<UIManager>().UpdateWins();
        gameOver = true;
    }
}
