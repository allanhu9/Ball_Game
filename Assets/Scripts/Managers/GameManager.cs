using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score;
    public int ballCount; // number of balls remaining
    public int numberOfPlayerColour = 0;
    public int maxScore;
    public int minScore;
    [SerializeField] private int maxTurns = 10;
    public int turns;
    private static GameManager singleton = null;
    [SerializeField] public int numberOfColours = 1;
    private Colour playerColour;
    
    public enum Colour
    {
        red, cyan, green, blue, magenta, yellow
    };
    void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            singleton = this;
        }
        DontDestroyOnLoad(gameObject);
        turns = maxTurns;
    }
    public static GameManager GetGameManager()
    {
        return singleton;
    }
    public void DisableBall()
    {
        GameObject playerBall = GameObject.FindWithTag("Player");
        if (playerBall)
        {
            playerBall.GetComponent<PlayerBall>().disable();
        }
    }

    public void EnableBall()
    {
        GameObject playerBall = GameObject.FindWithTag("Player");
        if (playerBall)
        {
            playerBall.GetComponent<PlayerBall>().enable();
        }
    }

    public void SetPlayerColour()
    {
        playerColour = GetRandomColour();
    }

    public Colour GetPlayerColour()
    {
        return playerColour;
    }

    public Colour GetRandomColour()
    {
        int num = UnityEngine.Random.Range(1, numberOfColours + 1);
        return ColourByIndex(num);
    }

    public int GetBallCount()
    {
        return ballCount;
    }
    
    public void GainScore() {
        score++;
        numberOfPlayerColour--;
        ballCount--;
        UpdateScoreBoard();
        if (numberOfPlayerColour == 0) {
            EndGame();
        }
    }

    public void LoseScore() {
        score--;
        ballCount--;
        UpdateScoreBoard();
    }

    public void UpdateScoreBoard() {
        GameObject scoreBoard = GameObject.FindWithTag("Score");
        scoreBoard.GetComponent<ScoreText>().UpdateText(score);
    }

    public void UpdateMoves() {
        GameObject movesText = GameObject.FindWithTag("Moves");
        movesText.GetComponent<ScoreText>().UpdateText(turns);
    }

    public void EndGame() {
        SceneManager.LoadScene("End Screen");
    }

    public int TurnsUsed() {
        return maxTurns - turns;
    }

    public void Reset() {
        numberOfPlayerColour = 0;
        score = 0;
        maxScore = 0;
        minScore = 0;
        turns = maxTurns;
    }
    
    private Colour ColourByIndex(int num)
    {
        if (num == 1)
        {
            return Colour.red;
        }
        else if (num == 2)
        {
            return Colour.cyan;
        }
        else if (num == 3)
        {
            return Colour.green;
        }
        else if (num == 4)
        {
            return Colour.blue;
        }
        else if (num == 5)
        {
            return Colour.magenta;
        }
        else
        {
            return Colour.yellow;
        }
    }
}
