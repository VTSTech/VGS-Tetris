using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

//v0.0.1-r7
//Scripts/ScoreScript.cs

public class ScoreScript : MonoBehaviour {
    private string ScoreDisplay = "";
    private string HighScoreDisplay = "";
    public Text ScoreLabel;
    public Text HighScoreLabel;

    // Use this for initialization
    void Start () {
        GameManager.Instance.NewGame();
        ScoreLabel.text = "Score: " + GameManager.Instance.ScoreValue.ToString() + "\n Lines: " + GameManager.Instance.LineValue.ToString() + "\n Level: " + GameManager.Instance.GameLevel.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateScore()
    {
        GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 12;
        GameManager.Instance.LineValue = GameManager.Instance.LineValue + 1;
        GameManager.Instance.GameLevel = (GameManager.Instance.LineValue / 10);
        //FallSpeed set here
        if (GameManager.Instance.GameLevel > 1 && GameManager.Instance.GameLevel <= 6)
        {
            GameManager.Instance.FallSpeed = (10f - GameManager.Instance.GameLevel) * 0.1f;
        }
        else if (GameManager.Instance.GameLevel >=7 && GameManager.Instance.GameLevel <= 9)
        {
            GameManager.Instance.FallSpeed = (10f - GameManager.Instance.GameLevel) * 0.1f + 0.05f;
        }
        else if (GameManager.Instance.GameLevel == 10)
        {
            GameManager.Instance.FallSpeed = 0.15f;
        }
        else if (GameManager.Instance.GameLevel == 11)
        {
            GameManager.Instance.FallSpeed = 0.10f;
        }
        ScoreDisplay = "Score: " + GameManager.Instance.ScoreValue.ToString() + "\n Lines: " + GameManager.Instance.LineValue.ToString() + "\n Level: " + GameManager.Instance.GameLevel.ToString();
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
        WriteHighScore();
        Debug.Log("UpdateHighScore() fired!");
    }
    public void RefreshHighScore()
    {
        //GameManager.Instance.HighScore = GameManager.Instance.ScoreValue;
        HighScoreDisplay = "High Score: " + GameManager.Instance.HighScore.ToString();
        HighScoreLabel.text = HighScoreDisplay;
        Debug.Log("RefreshHighScore() fired!");
    }
    public void WriteHighScore()
    {
        string Filename = Application.dataPath + "/Scores.dat";
        string output = "";
        if (GameManager.Instance.HighScore > 0 && File.Exists(Filename))
        {
            output = GameManager.Instance.SetPlayerName + "," + GameManager.Instance.HighScore + "," + System.DateTime.Now;
            File.AppendAllText(Filename, output);
            Debug.Log("WriteHighScore() fired!");
        }
    }
}
