using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource BallHitSource;
    
    public AudioClip BackGround;
    public AudioClip Hitclip;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        MusicSource.clip = BackGround;
        
        MusicSource.Play();
       
        
    }
    public void BallSFX()
    {
        BallHitSource.clip = Hitclip;
        BallHitSource.Play();
    }
}
