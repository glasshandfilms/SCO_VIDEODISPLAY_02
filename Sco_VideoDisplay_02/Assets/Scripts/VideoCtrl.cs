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

    public Button playButton;
    public Sprite playSprite;
    public Sprite pauseSprite;
    private int counter = 0;

    public Button hideUIButton;
    public Button showUIButton;

    private CanvasGroup canvasGroup;

    public float fadeInTime = 0.5f;
    public float fadeOutTime = 0.2f;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        volume = sliderVolume.value;
    }
    
    void Start()
    {
        
        videoPlayer.clip = vids[videoClipIndex];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;

        playButton = GameObject.Find("Play Button").GetComponent<Button>();

        hideUIButton = GameObject.Find("HideUIButton").GetComponent<Button>();

        showUIButton = GameObject.Find("ShowUIButton").GetComponent<Button>();

        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();

    }

    void Update()
    {
        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    public void ChangeVolume()
    {
        volume = sliderVolume.value;
    }

    public void ChangeButton()
    {
        counter++;
        if (counter % 2 == 0)
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Play();
        }
        else
        {
            playButton.image.overrideSprite = pauseSprite;
            videoPlayer.Pause();
        }
    }

    public void FadeUI(CanvasGroup canvasGroup)
    {
        StartCoroutine(FadeUICo());
        
    }

    public void ShowUI(CanvasGroup canvas)
    {
        StartCoroutine(ShowUICo());
        
    }

    IEnumerator FadeUICo()
    {
        Debug.Log("Fade UI");

        if(canvasGroup.alpha > 0.94f)
        {
            for (float t = 0.01f; t < fadeInTime; t += 0.01f)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0.00f, t / fadeInTime);
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
        
    }

    
    IEnumerator ShowUICo()
    {
        Debug.Log("Show UI");

        if(canvasGroup.alpha < 0.94f)
        {
            for (float t = 0.01f; t < fadeOutTime; t += 0.01f)
            {
                canvasGroup.alpha = Mathf.Lerp(0.00f, 1f, t / fadeOutTime);
                yield return null;
            }
        }
        else
        {
            yield return null;
        }
        
        
    }
}
