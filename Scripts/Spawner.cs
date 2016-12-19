using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//v0.0.1-r5
//Scripts/Spawner.cs

public class Spawner : MonoBehaviour {

    public GameObject[] GameOverPanel;
    // Use this for initialization
    void Start ()
    {
        spawnNext();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    // Groups (of Blocks that fall)
    public GameObject[] groups;
    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i],transform.position,Quaternion.identity);
    }
    //GameOverPanel
    public void GameOverFn()
    {
        Debug.Log("GameOverFn() fired!");
        GameObject gsui = GameObject.FindGameObjectWithTag("gsui");
        gsui.GetComponent<ScoreScript>().UpdateHighScore();
        Instantiate(GameOverPanel[0], new Vector2(0,0),Quaternion.identity);
        GameObject gopactual = GameObject.Find("GameOverPanel(Clone)");
        gopactual.transform.parent = gsui.transform;
        gopactual.transform.SetPositionAndRotation(new Vector2(685, 320), Quaternion.identity);
    }
}
