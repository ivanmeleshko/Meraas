using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{

    public static SceneMgr instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            //Destroy(gameObject);
        }
    }


    public void LoadScene(int sceneIndex)
    {

        if (sceneIndex == 2)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            
        }
        else if (sceneIndex == 0)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(2);
        }

    }
}
