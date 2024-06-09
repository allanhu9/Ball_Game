using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private const float bottomBorder = -4.5f;
    private const float rightBorder = 8.39f;
    private const float leftBorder = -8.39f;
    private const float topBorder = 3.52f;
    private GameManager gameManager = GameManager.GetGameManager();
    private GameObject[] balls;
    private Vector3 playerLocation;
    private int maxBalls = 10; // number of balls to spawn
    [SerializeField] private GameObject playerFab;
    [SerializeField] private GameObject ballFab;
    [SerializeField] private int minSameColour = 1;

    private void Start()
    {
        balls = new GameObject[maxBalls];
        gameManager.ballCount = maxBalls;
        playerLocation = new Vector3(0, 0, 0);
        SpawnBalls();
        Instantiate(playerFab, playerLocation, Quaternion.identity, gameObject.transform);
        gameManager.minScore = -(maxBalls - gameManager.numberOfPlayerColour);
        gameManager.maxScore = gameManager.numberOfPlayerColour;
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            Vector3 position;
            //Debug.Log(i);
            do
            { // keep generating until not overlapping.
                position = new Vector3(Random.Range(leftBorder, rightBorder), Random.Range(bottomBorder, topBorder), 0);
                //Debug.Log(position);
            } while (Overlapping(position));
            balls[i] = Instantiate(ballFab, position, Quaternion.identity, gameObject.transform);
            SetBallColour(balls[i]);
        }
    }

    private void SetBallColour(GameObject ball)
    {
        GameManager.Colour ballColour;
        if (minSameColour > 0)
        {
            ballColour = gameManager.GetPlayerColour();
            gameManager.numberOfPlayerColour++;
            minSameColour--;
        }
        else
        {
            ballColour = gameManager.GetRandomColour();
            if (ballColour == gameManager.GetPlayerColour())
            {
                gameManager.numberOfPlayerColour++;
            }
        }
        ball.GetComponent<Ball>().colour = ballColour;
        ball.GetComponent<Ball>().ColourSelf();
    }

    private bool Overlapping(Vector3 position)
    {
        if (position.x <= playerLocation.x + 1f && position.x >= playerLocation.x - 1f
                && position.y <= playerLocation.y + 1f && position.y >= playerLocation.y - 1f)
        {
            return true;
        }
        foreach (GameObject b in balls)
        {
            if (b)
            {
                if (position.x <= b.transform.position.x + 1f && position.x >= b.transform.position.x - 1f
                && position.y <= b.transform.position.y + 1f && position.y >= b.transform.position.y - 1f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
