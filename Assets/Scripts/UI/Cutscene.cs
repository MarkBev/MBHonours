using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
     
    private VideoPlayer player;
    public float videoLength;
    private GameObject renderTextureDisplay;

    private void Awake()
    {
       player = GetComponent<VideoPlayer>();
        player.clip = null;
        renderTextureDisplay = GameObject.FindWithTag("RenderTexture");
    }

    //recieves a video clip, sets the player to that video, then plays it
    public void PlayVideo(VideoClip clip)
    {
        //sets the clip to be played
        player.clip = clip; 
        videoLength = (float)player.clip.length;
        Debug.Log("Video Length is" +  videoLength);
        //enables the video player
        renderTextureDisplay.SetActive(true);
        player.Play();
        //StartCoroutine(CutsceneRoutine());

    }


    public void StopPlaying()
    {
        player.Stop();
        player.enabled = false;
        player.clip = null;
        renderTextureDisplay.SetActive(false);
    }

    IEnumerator CutsceneRoutine()
    {
        //waits for the duration of the video, then stops the player and clears the clip.
        yield return new WaitForSeconds(videoLength);
        StopPlaying();
    }
}
