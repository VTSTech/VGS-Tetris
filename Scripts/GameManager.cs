using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

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
    public float FallSpeed = 1f;
    public int GameLevel = 0;
    public string SetPlayerName = "Player";
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
        GameManager.Instance.ScoreValue = 0;
        string ScoreFile = Application.dataPath + "/Scores.dat";
        GameObject gsui = GameObject.FindGameObjectWithTag("gsui");
        gsui.GetComponent<ScoreScript>().RefreshHighScore();
        if (File.Exists(ScoreFile) == false)
        {
            File.Create(ScoreFile);
        }
        //Debug Stuff
        Debug.Log(ScoreFile);
        Debug.Log(File.Exists(ScoreFile));
    }
    public void SavePlayer(string PlayerName)
    {
        SetPlayerName = PlayerName;
        string PlayerFile = Application.dataPath + "/Player.dat";
        if (File.Exists(PlayerFile) == false)
        {
            File.Create(PlayerFile);
        }
        File.WriteAllText(PlayerFile, SetPlayerName);
        Debug.Log("Player Name set to: " + SetPlayerName);
    }
    public void SaveMusic()
    {
        GameObject MusicToggle = GameObject.FindGameObjectWithTag("MusicToggle");
        GameObject BGM = GameObject.FindGameObjectWithTag("bgmusic");
        if (MusicToggle.GetComponent<Toggle>().isOn == false)
        {
            BGM.GetComponent<AudioSource>().Stop();
            Debug.Log("Music Stopped");
        }
        else
        {
            BGM.GetComponent<AudioSource>().Play();
            Debug.Log("Music Started");
        }
    }
    public void SaveSettings()
    {
        SavePlayer(SetPlayerName);
        SaveMusic();
    }
    public void LoadPlayer()
    {
        string PlayerFile = Application.dataPath + "/Player.dat";
        if (File.Exists(PlayerFile) == false)
        {
            File.Create(PlayerFile);
        }
        SetPlayerName = File.ReadAllText(PlayerFile);
        GameObject PlayerText = GameObject.FindGameObjectWithTag("Player");
        PlayerText.GetComponent<Text>().text.Insert(0,SetPlayerName);
        Debug.Log("Player Name set to: " + SetPlayerName);
    }
}
