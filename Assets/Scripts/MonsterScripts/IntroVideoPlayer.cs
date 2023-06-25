using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class IntroVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;           

    private bool videoPlayed;         
    private void Update()
    {
        if (videoPlayer.isPlaying && !videoPlayed && Input.anyKeyDown)
        {
            videoPlayed = true;
            StopVideoAndLoadMainScene();
        }
    }

    private void StopVideoAndLoadMainScene()
    {
        videoPlayer.Stop();              
        SceneManager.LoadScene("Main");    
    }
}
