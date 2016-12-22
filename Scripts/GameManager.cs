using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r10
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/GameManager.cs
 */
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
    public InputField myInputField;
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
        LoadSettings();
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
        GameObject PlayerLabel = GameObject.FindGameObjectWithTag("PlayerLabel");
        gsui.GetComponent<ScoreScript>().RefreshHighScore();
        if (File.Exists(ScoreFile) == false)
        {
            File.Create(ScoreFile);
        }
        PlayerLabel.GetComponent<Text>().text = GameManager.Instance.SetPlayerName;
        LoadSettings();
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
        string MusicFile = Application.dataPath + "/Music.dat";
        if (File.Exists(MusicFile) == false)
        {
            File.Create(MusicFile);
        }
        GameObject MusicToggle = GameObject.FindGameObjectWithTag("MusicToggle");
        GameObject BGM = GameObject.FindGameObjectWithTag("bgmusic");
        if (MusicToggle.GetComponent<Toggle>().isOn == false)
        {
            BGM.GetComponent<AudioSource>().Stop();
            Debug.Log("Music Stopped");
            File.WriteAllText(MusicFile, "Off");
        }
        else
        {
            BGM.GetComponent<AudioSource>().Play();
            Debug.Log("Music Started");
            File.WriteAllText(MusicFile, "On");
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
        //GameObject PlayerText = GameObject.FindGameObjectWithTag("Player");
        myInputField.text = SetPlayerName;
        
        Debug.Log("Player Name set to: " + SetPlayerName);
    }
    public void LoadMusic()
    {
        string MusicFile = Application.dataPath + "/Music.dat";
        string MusicSetting;
        GameObject MusicToggle = GameObject.FindGameObjectWithTag("MusicToggle");
        GameObject BGM = GameObject.FindGameObjectWithTag("bgmusic");
        if (File.Exists(MusicFile) == false)
        {
            File.Create(MusicFile);
            File.WriteAllText(MusicFile, "On");
        }
        MusicSetting = File.ReadAllText(MusicFile);
        if (MusicSetting == "Off")
        {
            BGM.GetComponent<AudioSource>().Stop();
            Debug.Log("Music Stopped");
            MusicToggle.GetComponent<Toggle>().isOn = false;
        } else if (MusicSetting == "On")
        {
            BGM.GetComponent<AudioSource>().Play();
            Debug.Log("Music Started");
            MusicToggle.GetComponent<Toggle>().isOn = true;
        }
    }
    public void LoadSettings()
    {
        LoadPlayer();
        LoadMusic();
    }
}
