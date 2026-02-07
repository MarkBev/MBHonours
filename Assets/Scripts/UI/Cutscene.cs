using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
     
    private VideoPlayer player;
    private float videoLength;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
        player.clip = null;
    }

    //recieves a video clip, sets the player to that video, then plays it
    public void PlayVideo(VideoClip clip)
    {
        //sets the clip to be played
        player.clip = clip; 
        videoLength = (float)player.clip.length;
        //enables the video player
        player.enabled = true;   
        player.Play();
        StartCoroutine(CutsceneRoutine());

    }


    private void StopPlaying()
    {
        player.Stop();
        player.enabled = false;
        player.clip = null;
    }

    IEnumerator CutsceneRoutine()
    {
        //waits for the duration of the video, then stops the player and clears the clip.
        yield return new WaitForSeconds(videoLength);
        StopPlaying();
    }
}
