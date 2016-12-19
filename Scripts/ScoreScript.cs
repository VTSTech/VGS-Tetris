using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//v0.0.1-r4
//Scripts/ScoreScript.cs

public class ScoreScript : MonoBehaviour {
    private string ScoreDisplay = "";
    private string HighScoreDisplay = "";
    public Text ScoreLabel;
    public Text HighScoreLabel;

    // Use this for initialization
    void Start () {
        GameManager.Instance.NewGame();
        ScoreLabel.text = "Score: "+ GameManager.Instance.ScoreValue.ToString() + "\n Lines: " + GameManager.Instance.LineValue.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateScore()
    {
        GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 12;
        GameManager.Instance.LineValue = GameManager.Instance.LineValue + 1;
        ScoreDisplay = "Score: " + GameManager.Instance.ScoreValue.ToString() + "\n Lines: " + GameManager.Instance.LineValue.ToString();
        ScoreLabel.text = ScoreDisplay;
    }
    public void UpdateHighScore()
    {
        if (GameManager.Instance.ScoreValue > GameManager.Instance.HighScore && GameManager.Instance.ScoreValue != 0)
        {
            GameManager.Instance.HighScore = GameManager.Instance.ScoreValue;
            GameManager.Instance.ScoreValue = 0;
            Debug.Log("High Score Updated!");
            HighScoreDisplay = "High Score: " + GameManager.Instance.HighScore.ToString();
            HighScoreLabel.text = HighScoreDisplay;
        }
        Debug.Log("UpdateHighScore() fired!");
    }
    public void RefreshHighScore()
    {
        //GameManager.Instance.HighScore = GameManager.Instance.ScoreValue;
        HighScoreDisplay = "High Score: " + GameManager.Instance.HighScore.ToString();
        HighScoreLabel.text = HighScoreDisplay;
    }
}
