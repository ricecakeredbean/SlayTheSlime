using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public static int clearStage;
    private void Awake()
    {
        Instance = this;
    }
    public void Stage1Load()
    {
        SceneManager.LoadScene(1);
    }
    public void Stage2Load()
    {
        if(clearStage >=1)
            SceneManager.LoadScene(2);
    }
    public void Stage3Load()
    {
        if(clearStage >=2)
            SceneManager.LoadScene(3);
    }
    public void Stage4Load()
    {
        if(clearStage >=3)
            SceneManager.LoadScene(4);
    }
}

