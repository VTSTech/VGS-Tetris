using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;
    public int HighScore = 0;
    public int ScoreValue = 0;
    public int LineValue = 0;
    public bool IsPaused = false;
    public bool InputAllowed = true;
    
    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NewGame()
    {    
        GameManager.Instance.LineValue = 0;
        GameManager.Instance.ScoreValue = 0;
        GameObject gsui = GameObject.FindGameObjectWithTag("gsui");
        gsui.GetComponent<ScoreScript>().RefreshHighScore();
    }
}
