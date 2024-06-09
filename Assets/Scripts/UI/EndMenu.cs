using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EndMenu : MonoBehaviour
{
    Color gold = new(1, 206f/255f, 4f/255f);
    Color shaded = new(122f/255f, 122f/255f, 122f/255f, 57f/255f);
    [SerializeField] private TextMeshProUGUI complimentText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI minScoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject leftStar;
    [SerializeField] private GameObject rightStar;
    [SerializeField] private GameObject middleStar;
    private RawImage leftStarImg;
    private RawImage middleStarImg;
    private RawImage rightStarImg;
    private GameManager gameManager;
    private float relativeScore;
    private float score = 0;
    private float maxScore = 0;
    private float minScore = 0;

    private void Start()
    {
        gameManager = GameManager.GetGameManager();
        maxScore = gameManager.maxScore;
        minScore = gameManager.minScore;
        score = gameManager.score;
        //Debug.Log(maxScore + " " + minScore);
        minScoreText.text = minScore.ToString();
        maxScoreText.text = maxScore.ToString();
        scoreText.text = score.ToString() + " POINTS IN " + gameManager.TurnsUsed().ToString() + " TURNS";
        relativeScore = (score - minScore) / (maxScore - minScore);
        Debug.Log(relativeScore);
        leftStarImg = leftStar.GetComponent<RawImage>();
        middleStarImg = middleStar.GetComponent<RawImage>();
        rightStarImg = rightStar.GetComponent<RawImage>();
        ColourStars();
        Compliment();
        FillSlider();
    }

    private void FillSlider()
    {
        slider.value = relativeScore;
    }

    private void ColourStars()
    {
        if (0 <= relativeScore && 0.33 > relativeScore)
        {
            leftStarImg.color = shaded;
            middleStarImg.color = shaded;
            rightStarImg.color = shaded;
        }
        else if (0.33 <= relativeScore && 0.66 > relativeScore)
        {
            leftStarImg.color = gold;
            middleStarImg.color = shaded;
            rightStarImg.color = shaded;
        }
        else if (0.66 <= relativeScore && 1 > relativeScore)
        {
            leftStarImg.color = gold;
            middleStarImg.color = gold;
            rightStarImg.color = shaded;
        } else if (relativeScore == 1) {
            leftStarImg.color = gold;
            middleStarImg.color = gold;
            rightStarImg.color = gold;
        } else {
            Debug.Log("star error");
        }
    }

    private void Compliment() {
        if (0 <= relativeScore && 0.33 > relativeScore)
        {
            complimentText.text = "NICE TRY";
        }
        else if (0.33 <= relativeScore && 0.66 > relativeScore)
        {
            complimentText.text = "NOT BAD";
        }
        else if (0.66 <= relativeScore && 1 > relativeScore)
        {
            complimentText.text = "WELL DONE";
        } else if (relativeScore == 1) {
            complimentText.text = "PERFECT";
        } else {
            Debug.Log("star error");
        }
    }

    public void Return()
    {
        gameManager.Reset();
        SceneManager.LoadScene("Menu");
    }
}
