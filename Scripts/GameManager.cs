using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r11
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
    public bool isGameOver = false;
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
        GameObject sm = GameObject.FindGameObjectWithTag("SettingsMenu");
        DontDestroyOnLoad(sm);
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
        GameObject pl = GameObject.FindGameObjectWithTag("PauseLabel");
        pl.GetComponent<Text>().enabled = false;
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
    public void ScreenFlash()
    {
        Debug.Log("ScreenFlash() fired!");
        StartCoroutine("LerpColor");
        //GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        //go.GetComponent<Camera>().backgroundColor = Color.white;
        //go.GetComponent<Camera>().backgroundColor = Color.Lerp(Color.black,Color.white,2.5f);
        //go.GetComponent<Camera>().backgroundColor = Color.black;
    }
    IEnumerator LerpColor()
    {
        for (int c = 0; c <= 4; c++) { 
        float t = 0f;
        float duration = 0.06f;
        float smoothness = 0.01f;
        float increment = smoothness / duration;
        Debug.Log("LerpColor() fired!");
        GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
        go.GetComponent<Camera>().backgroundColor = Color.white;
        while (t <= 1)
        {
            go.GetComponent<Camera>().backgroundColor = Color.Lerp(Color.gray, Color.white, t);
            t += increment;
            Debug.Log("T Value: " + t.ToString());
            yield return new WaitForSeconds(smoothness);
        }
        go.GetComponent<Camera>().backgroundColor = Color.black;
        yield return true;
        }
    }
}
