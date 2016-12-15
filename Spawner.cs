using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* v0.0.1
 * Initial Build
 * Follows Tutorial from: https://noobtuts.com/unity/2d-tetris-game
 */
public class Spawner : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        spawnNext();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    // Groups
    public GameObject[] groups;
    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i],
                    transform.position,
                    Quaternion.identity);
    }
}
