using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public ColorChanger[] pics;
    public Slider scoreSlider;

    // Update is called once per frame
    void Update()
    {
        float score = 0;
        foreach (var pic in pics)
        {
            score += pic.GetScore();
        }

        float currentScore = (1 - (score / pics.Length)) / 0.5f;

        scoreSlider.value = Mathf.Lerp(scoreSlider.value,currentScore,0.01f);

        if (scoreSlider.value > 0.99f)
        {
            Debug.Log("You win");
        }
    }
}
