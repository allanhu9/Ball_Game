using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int score)
    {
        text.text = score.ToString();
    }

}
