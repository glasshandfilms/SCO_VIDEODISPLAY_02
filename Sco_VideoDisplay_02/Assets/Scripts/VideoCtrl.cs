﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;
[RequireComponent(typeof(VideoPlayer))]

public class VideoCtrl : MonoBehaviour
{
    private string videoName;    
    public int videoClipIndex;
    public RawImage[] tabs;
    public int tabsIndex;
    public VideoPlayer videoPlayer;
    private string pathVideos;
    public float volume = 1f;
    public Slider sliderVolume; 
        
    public Button playButton;
    public Image playButtonImage;
    public Sprite playSprite;
    public Sprite pauseSprite;
    private int counter = 0;

    public Button hideUIButton;    
    public Image volumeHandle;
    public Image volumeBackground;
    public Toggle volumeToggle;
    public Material volumeBackgroundMaterial;
    public Material volumeHandleMaterial;

    private CanvasGroup canvasGroup;

    public float fadeInTime = 0.5f;
    public float fadeOutTime = 0.2f;

    public Material playButtonMat;
    public bool isCycling;
    private bool goingForward;    
    public Color startColor;
    public Color endColor;
    [SerializeField]
    private bool isGoingForward;
    public float time;
    public bool flashing;
    public Button homeScreenButton;


    void Awake()
    {
        Cursor.visible = false;
        videoName = "Scoppechio.mp4";
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);        
        volume = sliderVolume.value;
        tabs[0].enabled = false;
        tabs[1].enabled = false;
        tabs[2].enabled = false;
        tabs[3].enabled = false;
        tabs[4].enabled = false;
        
        isCycling = false;
        isGoingForward = true;
        playButtonMat = playButtonImage.GetComponent<Image>().material;
        playButtonMat.color = startColor;
        

    }
    
    void Start()
    {
        
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;

        playButton = GameObject.Find("Play Button").GetComponent<Button>();

        hideUIButton = GameObject.Find("HideUIButton").GetComponent<Button>();       

        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();

        volumeHandle = GameObject.Find("VolumeHandle").GetComponent<Image>();

        volumeBackground = GameObject.Find("VolumeBackground").GetComponent<Image>();        

        volumeToggle = GameObject.Find("VolumeToggle").GetComponent<Toggle>();

        homeScreenButton = GameObject.Find("HomeScreenButton").GetComponent<Button>();

        

    }

    void Update()
    {
        videoPlayer.SetDirectAudioVolume(0, volume);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (flashing == true)
        {
            if (!isCycling)
            {
                //Debug.Log("is Cycling = true");
                if (goingForward)
                    StartCoroutine(PlayButtonFlashCo(startColor, endColor, time, playButtonMat));
                else
                    StartCoroutine(PlayButtonFlashCo(endColor, startColor, time, playButtonMat));
            }
        }
        else
        {
            playButtonMat.color = startColor;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            counter++;
            if (counter % 2 == 0)
            {

                Cursor.visible = true;
                //Debug.Log("cursor hidden");

            }
            else
            {
                Cursor.visible = false;
                //Debug.Log("cursor show");
            }
        }

    }

    public void ShowVolumeBar()
    {
        //Debug.Log("is On == true");
        StartCoroutine(ShowVolumeBackgroundCo());        
    }

    IEnumerator ShowVolumeBackgroundCo()
    {
        Debug.Log("Ran Show Vol Background Co");

        if (volumeBackground.GetComponent<Image>().color.a == 1)
        {
            Debug.Log("start Show Loop");
            float alpha = volumeBackground.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, 1));
                volumeBackground.color = newColor;
                volumeHandle.color = newColor;
                yield return null;
            }
        }
        else
        {
            Debug.Log("wow");
            float alpha = volumeBackground.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0f, 0));
                volumeBackground.color = newColor;
                volumeHandle.color = newColor;
                yield return null;
            }
        }
        
        
        
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
            
            playButton.image.overrideSprite = pauseSprite;
            videoPlayer.Play();
            flashing = false;


        }
        else
        {
            playButton.image.overrideSprite = playSprite;            
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
            for (float t = 0.01f; t < fadeInTime; t += 0.01f)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeInTime);
                yield return null;
            }
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

    IEnumerator PlayButtonFlashCo(Color startColor, Color endColor, float cycleTime, Material mat)
    {
        //Debug.Log("In Coroutine");
        isCycling = true;
        float currentTime = 0;
        while (currentTime < cycleTime)
        {
            //Debug.Log("start");
            currentTime += Time.deltaTime;
            float t = currentTime / cycleTime;
            Color currentColor = Color.Lerp(startColor, endColor, t);
            mat.color = currentColor;
            yield return null;
        }
        isCycling = false;
        goingForward = !goingForward;




    }

    public void CivitasVideo()
    {
        Debug.Log("play Civitas Video");
        tabs[0].enabled = true;
        tabs[1].enabled = false;
        tabs[2].enabled = false;
        tabs[3].enabled = false;
        tabs[4].enabled = false;
        
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "Civitas.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {
            
            videoPlayer.Play();
            flashing = false;

        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }
    }

    public void BadgeVideo()
    {
        
        tabs[0].enabled = false;
        tabs[1].enabled = true;
        tabs[2].enabled = false;
        tabs[3].enabled = false;
        tabs[4].enabled = false;
        
        Debug.Log("play Badge Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "Badge.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {
            
            videoPlayer.Play();
            flashing = false;

        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }
    }

    public void ScoppechioVideo()
    {
        
        tabs[0].enabled = false;
        tabs[1].enabled = false;
        tabs[2].enabled = true;
        tabs[3].enabled = false;
        tabs[4].enabled = false;
        
        Debug.Log("play Scoppechio Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "Scoppechio.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {
            
            videoPlayer.Play();
            flashing = false;

        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }
    }

    public void ThreeAnimationVideo()
    {
        
        tabs[0].enabled = false;
        tabs[1].enabled = false;
        tabs[2].enabled = false;
        tabs[3].enabled = true;
        tabs[4].enabled = false;
        
        Debug.Log("play 3 Animation Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "TheThreeAnimation.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {
            
            videoPlayer.Play();
            flashing = false;

        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }
    }

    public void ThreeLiveAction()
    {
        
        tabs[0].enabled = false;
        tabs[1].enabled = false;
        tabs[2].enabled = false;
        tabs[3].enabled = false;
        tabs[4].enabled = true;
        
        Debug.Log("play 3 Live Action Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "TheThreeLiveAction.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {
            
            videoPlayer.Play();
            flashing = false;
            
        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }

    }

    public void HomeScreenVideo()
    {
        tabs[0].enabled = false;
        tabs[1].enabled = false;
        tabs[2].enabled = false;
        tabs[3].enabled = false;
        tabs[4].enabled = false;
        
        Debug.Log("play Home Screen Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;
        videoName = "HomeScreen.mp4";
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
        if (playButton.image.overrideSprite == pauseSprite)
        {

            videoPlayer.Play();
            flashing = false;

        }
        else
        {
            playButton.image.overrideSprite = playSprite;
            videoPlayer.Pause();
            PlayButtonPulse();
        }
    }

    public void PlayButtonPulse()
    {        
        
        flashing = true;
        
    }

    

    
}
