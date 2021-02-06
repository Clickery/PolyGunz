using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public Text scoreText;
    public Text pointsText;
    public GunStatsManager gunStatManager;

    public int score = 0;
    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        points = 0;
    }

    public void AddScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddPoints()
    {
        points += 20;
        pointsText.text = "Points: " + points.ToString();
    }


    public int getCurrentScore()
    {
        return score;
    }


    public void IncreaseDamage(int index)
    {
        Debug.Log("UPGRADE!!");
        if (points >= 1000)
        {
            gunStatManager.upgradeWeapon(index);
            points -= 1000;
            pointsText.text = "Points: " + points.ToString();
        }
        else
        {
            Debug.Log("Not Enough Points!");
        }
    }

    public void addReward()
    {
        points += 1000;
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points.ToString();
        }
    }
}
