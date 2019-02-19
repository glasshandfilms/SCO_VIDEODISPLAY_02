using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
[RequireComponent(typeof(VideoPlayer))]

public class VideoCtrl : MonoBehaviour
{
    public VideoClip[] vids;
    public VideoPlayer videoPlayer;
    public float volume = 1f;
    public Slider sliderVolume; 
        
    public int videoClipIndex;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        volume = sliderVolume.value;
    }
    
    void Start()
    {
        
        videoPlayer.clip = vids[videoClipIndex];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;   
        
        
   
       
    }

    

    // Update is called once per frame
    void Update()
    {
        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    public void ChangeVolume()
    {
        volume = sliderVolume.value;
    }



}
