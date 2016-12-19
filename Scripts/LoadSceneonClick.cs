using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneonClick : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {
        DestroyImmediate(GameObject.Find("GameOverPanel(Clone)"));
        SceneManager.LoadScene(sceneIndex);
    }
}
