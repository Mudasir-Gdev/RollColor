using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Singleton;
    public float maxScenes;

    private GroundScript[] allGroundPices; 
    void Start()
    {
        
        SetupNewLvl();
        
    }
    private void SetupNewLvl()
    {
        allGroundPices = FindObjectsOfType<GroundScript>();
    }
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else if (Singleton != this)
        {
            Destroy(Singleton);
            DontDestroyOnLoad(Singleton);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnFinishLoading ;
    }
    private void OnFinishLoading(Scene scene,LoadSceneMode mode)
    {
        SetupNewLvl();
    }
    bool isfinished;
    public void CheckComplete()
    {
        isfinished = true;
        for (int i = 0;i< allGroundPices.Length;i++)
        {
            
            if (allGroundPices[i].isColored== false)
            {
                isfinished = false;
                break;
            }
        }
        if (isfinished)
        {
            NextLvl();
        }
    }
    private void NextLvl()
    {
        if (SceneManager.GetActiveScene().buildIndex == maxScenes-1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
        
    
}
