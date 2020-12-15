using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public Text scoreText;
    public Text pointsText;

    public int score = 0;
    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update Text Here
        scoreText.text = "Score: " + score.ToString();
        pointsText.text = "Points: " + points.ToString();
    }

    public void AddScore()
    {
        score += 100;
    }

    public void AddPoints()
    {
        points += 100;
    }

    public void BuyUpgrade()
    {
        if (points >= 1000)
        {
            points -= 1000;
        }

        else
        {
            Debug.Log("Not Enough Points!");
        }
    }
}
