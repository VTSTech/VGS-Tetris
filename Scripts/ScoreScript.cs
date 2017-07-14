using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.2-r12
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/ScoreScript.cs
 */

public class ScoreScript : MonoBehaviour
{
    private string ScoreDisplay = "";
    private string HighScoreDisplay = "";
    //private string PlayerDisplay = "";
    public Text ScoreLabel;
    public Text HighScoreLabel;
    public Text PlayerLabel;
    bool HighScoreRefreshed = false;
    const string privCode = "";
    const string pubCode = "";
    const string baseURL = "http://dreamlo.com/lb/";

    // Use this for initialization
    void Start()
    {
            GameManager.Instance.NewGame();
            ScoreLabel.text = "Score: " + GameManager.Instance.ScoreValue.ToString() + "\n Lines: " + GameManager.Instance.LineValue.ToString() + "\n Level: " + GameManager.Instance.GameLevel.ToString();
            PlayerLabel.text = GameManager.Instance.SetPlayerName; 
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore()
    {
        GameManager.Instance.LineValue = GameManager.Instance.LineValue + 1;
        GameManager.Instance.GameLevel = (GameManager.Instance.LineValue / 10);
        GameObject gmgr = GameObject.FindGameObjectWithTag("GameManager");
        if (GameManager.Instance.GameLevel == 0)
        {
            if (Grid.cons == 4)
            {
                gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 96;
            }
            else if (Grid.cons == 3)
            {
                //gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 36;
            }
            else if (Grid.cons == 2)
            {
                //gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 24;
            }
            else if (Grid.cons <= 1)
            {
                //gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 12;
            }
        } else if (GameManager.Instance.GameLevel >= 1 && GameManager.Instance.GameLevel <= 6)
        {
            if (Grid.cons == 4)
            {
                gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + (96 * GameManager.Instance.GameLevel);
            }
            else if (Grid.cons == 3)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + (36 * GameManager.Instance.GameLevel);
            }
            else if (Grid.cons == 2)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + (24 * GameManager.Instance.GameLevel);
            }
            else if (Grid.cons <= 1)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + (12 * GameManager.Instance.GameLevel);
            }
        } else if (GameManager.Instance.GameLevel >= 7)
        {
            if (Grid.cons == 4)
            {
                gmgr.GetComponent<GameManager>().ScreenFlash();
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 576;
            }
            else if (Grid.cons == 3)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 216;
            }
            else if (Grid.cons == 2)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 144;
            }
            else if (Grid.cons <= 1)
            {
                GameManager.Instance.ScoreValue = GameManager.Instance.ScoreValue + 72;
            }
            
        }
        //FallSpeed set here
        if (GameManager.Instance.GameLevel > 1 && GameManager.Instance.GameLevel <= 6)
        {
            GameManager.Instance.FallSpeed = (10f - GameManager.Instance.GameLevel) * 0.1f;
        }
        else if (GameManager.Instance.GameLevel >= 7 && GameManager.Instance.GameLevel <= 9)
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
        GameManager.Instance.LastScore = GameManager.Instance.ScoreValue;
        if (GameManager.Instance.ScoreValue > GameManager.Instance.HighScore && GameManager.Instance.ScoreValue != 0)
        {
            GameManager.Instance.HighScore = GameManager.Instance.ScoreValue;
            GameManager.Instance.LastScore = GameManager.Instance.ScoreValue;
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
        if (GameManager.Instance.HighScore == 0 && HighScoreRefreshed == false)
        {
            LoadHighScore();
            //StartCoroutine("DownloadHighscoresFromDatabase");
            HighScoreRefreshed = true;
        }
        HighScoreDisplay = "High Score: " + GameManager.Instance.HighScore.ToString();
        HighScoreLabel.text = HighScoreDisplay;
        StartCoroutine("DownloadHighscoresFromDatabase");
        Debug.Log("RefreshHighScore() fired!");
        
    }
    public void WriteHighScore()
    {
        StartCoroutine(UploadNewHighScore(GameManager.Instance.SetPlayerName, GameManager.Instance.LastScore));
        string Filename = Application.dataPath + "/Scores.dat";
        string output = "";
        if (GameManager.Instance.HighScore > 0 && File.Exists(Filename) && GameManager.Instance.isGameOver == true)
        {
            output = GameManager.Instance.SetPlayerName + "," + GameManager.Instance.HighScore + "," + System.DateTime.Now;
            File.AppendAllText(Filename, output + "\n");
            Debug.Log("WriteHighScore() fired!");
        }
        
    }

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new WWW(baseURL + privCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void LoadHighScore()
    {
        string Filename = Application.dataPath + "/Scores.dat";
        //string ScoreDataRead = "";
        //string[] ScoreDataResult;
        if (File.Exists(Filename))
        {
            //output = GameManager.Instance.SetPlayerName + "," + GameManager.Instance.HighScore + "," + System.DateTime.Now;
            string ScoreDataRead = File.ReadAllText(Filename);
            string[] ScoreDataResult = ScoreDataRead.Split('\n');
            foreach (string x in ScoreDataResult)
            {
                string[] ScoreDataResult2 = x.Split(',');
                if (ScoreDataResult2[0] == GameManager.Instance.SetPlayerName)
                {
                    GameManager.Instance.HighScore = int.Parse(ScoreDataResult2[1]);
                }
                StartCoroutine("DownloadHighscoresFromDatabase");
                //RefreshHighScore();
            }
            //Debug Stuff
            Debug.Log("LoadHighScore() fired!");
            Debug.Log("Result: " + ScoreDataResult[0]);
        }
    }
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(baseURL + pubCode + "/quote/10");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            ShowOnlineScores(www.text);
        else
        {
            print("Error Downloading: " + www.error);
        }
    }
    public void ShowOnlineScores(string scores)
    {
        GameObject onlineScores = GameObject.FindGameObjectWithTag("OnlineScores");
        string[] OnlineScoreResult;
        OnlineScoreResult = scores.Split('\n');
        onlineScores.GetComponent<Text>().text = "";
        int zt = 0;
        int t = 0;
        string DisplayString = "";
        foreach (string xt in OnlineScoreResult)
        {
            string[] OnlineScoreResult2 = xt.Split(',');
            //Debug Stuff
            zt = 0;
            Debug.Log("ShowOnlineScores() fired!");
            Debug.Log("Array Length: " + OnlineScoreResult.GetUpperBound(t));
            Debug.Log("Index 0: " + OnlineScoreResult[0]);
            Debug.Log("ZT Value: " + zt.ToString());
            Debug.Log("XT Value: " + xt);
            if (xt.Length > 3)
            {
                DisplayString = OnlineScoreResult2[zt] + " " + OnlineScoreResult2[zt + 1];
                DisplayString = DisplayString.Replace('"', ' ');
                onlineScores.GetComponent<Text>().text = onlineScores.GetComponent<Text>().text + DisplayString + "\n";
                if (zt == 0)
                {
                    zt += 2;
                }
                else
                {
                    zt += 3;
                }
                //GameManager.Instance.HighScore = int.Parse(OnlineScoreResult2[1]);
            }
            //RefreshHighScore();
            //Debug Stuff
            Debug.Log("ShowOnlineScores() fired!");
            Debug.Log("Result: " + DisplayString);
        }
        onlineScores.GetComponent<Text>().text = "Online High Scores:\n" + onlineScores.GetComponent<Text>().text;
    }
    public void ShowHighScores()
    {
        string Filename = Application.dataPath + "/Scores.dat";
        //string ScoreDataRead = "";
        //string[] ScoreDataResult;
        if (File.Exists(Filename))
        {
            //output = GameManager.Instance.SetPlayerName + "," + GameManager.Instance.HighScore + "," + System.DateTime.Now;
            string ScoreDataRead = File.ReadAllText(Filename);
            string[] ScoreDataResult = ScoreDataRead.Split('\n');
            GameObject AllScores = GameObject.FindGameObjectWithTag("AllScoresDisplay");
            AllScores.GetComponent<Text>().text = "";
            int zt = 0;
            int t = 0;
            string DisplayString = "";
            foreach (string xt in ScoreDataResult)
            {
                string[] ScoreDataResult2 = xt.Split(',');
                //Debug Stuff
                zt = 0;
                Debug.Log("ShowHighScores() fired!");
                Debug.Log("Array Length: " + ScoreDataResult.GetUpperBound(t));
                Debug.Log("Index 0: " + ScoreDataResult[0]);
                Debug.Log("ZT Value: " + zt.ToString());
                Debug.Log("XT Value: " + xt);
                if (xt.Length > 3)
                {
                    DisplayString = ScoreDataResult2[zt] + " " + ScoreDataResult2[zt + 1] + " " + ScoreDataResult2[zt + 2];
                    AllScores.GetComponent<Text>().text = AllScores.GetComponent<Text>().text + DisplayString + "\n";
                    if (zt == 0)
                    {
                        zt += 2;
                    } else
                    {
                        zt += 3;
                    }
                    //GameManager.Instance.HighScore = int.Parse(ScoreDataResult2[1]);
                }
                //RefreshHighScore();
                //Debug Stuff
                Debug.Log("ShowHighScores() fired!");
                Debug.Log("Result: " + DisplayString);
            }

        }
    }
}