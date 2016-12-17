using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    private int ScoreValue;
    private string ScoreDisplay = "";
    public Text ScoreLabel;

    // Use this for initialization
    void Start () {
        ScoreValue = 0;
        ScoreLabel.text = "Score: "+ ScoreValue.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateScore()
    {
        ScoreValue += 10;
        ScoreDisplay = "Score: "+ ScoreValue.ToString();
        ScoreLabel.text = ScoreDisplay;
    }

}
