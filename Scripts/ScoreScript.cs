using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//v0.0.1-r4
//Scripts/ScoreScript.cs

public class ScoreScript : MonoBehaviour {
    private int ScoreValue;
    private int LineValue;
    private string ScoreDisplay = "";
    public Text ScoreLabel;

    // Use this for initialization
    void Start () {
        ScoreValue = 0;
        LineValue = 0;
        ScoreLabel.text = "Score: "+ ScoreValue.ToString() + "\n Lines: " + LineValue.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateScore()
    {
        ScoreValue += 12;
        LineValue += 1;
        ScoreDisplay = "Score: " + ScoreValue.ToString() + "\n Lines: " + LineValue.ToString();
        ScoreLabel.text = ScoreDisplay;
    }
}
