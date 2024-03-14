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
    public GameObject turnLabel;
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
        // Activates the turnLabel
        turnLabel.SetActive(true);
        FindObjectOfType<UIManager>().UpdateTurn();
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
                if(!gameOver)
                {
                    currentPlayer = 1 - currentPlayer;
                    FindObjectOfType<UIManager>().UpdateTurn(); 
                }
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
                if (currentPlayer == Player.X)
                {
                    GameOver(Player.X);
                }
                else if (currentPlayer == Player.O)
                {
                    GameOver(Player.O);
                }
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
                    if (currentPlayer == Player.X)
                    {
                        GameOver(Player.X);
                    }
                    else if (currentPlayer == Player.O)
                    {
                        GameOver(Player.O);
                    }
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
                if (currentPlayer == Player.X)
                {
                    GameOver(Player.X);
                }
                else if (currentPlayer == Player.O)
                {
                    GameOver(Player.O);
                }
            }
        }
        if (!gameOver)
        {
            if (squares[0, 2] == currentPlayer
            && squares[1, 1] == currentPlayer
            && squares[2, 0] == currentPlayer)
            {
                if (currentPlayer == Player.X)
                {
                    GameOver(Player.X);
                } else if (currentPlayer == Player.O)
                {
                    GameOver(Player.O);
                }
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

    public string CheckTurn()
    {
        if (gameOver)
        {
            turnLabel.SetActive(false);
        }
        if (currentPlayer == Player.X)
        {
            return "X's";
        } else if (currentPlayer == Player.O)
        {
            return "O's";
        }
        return null;
    }

    private void GameOver(Player winner)
    {
        gameOver = true;
        turnLabel.SetActive(false);
        
        if (winner == Player.X && gameOver)
        {
            xWins++;
            xWinsUI.SetActive(true);
        }
        else if (winner == Player.O && gameOver)
        {
            oWins++;
            oWinsUI.SetActive(true);
        }
        FindObjectOfType<UIManager>().UpdateWins();
    }
}
