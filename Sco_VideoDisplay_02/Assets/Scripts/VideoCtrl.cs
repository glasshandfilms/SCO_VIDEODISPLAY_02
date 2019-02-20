using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
[RequireComponent(typeof(VideoPlayer))]

public class VideoCtrl : MonoBehaviour
{
    public VideoClip[] vids;
    public int videoClipIndex;
    public VideoPlayer videoPlayer;
    public float volume = 1f;
    public Slider sliderVolume; 
        
    public Button playButton;
    public Sprite playSprite;
    public Sprite pauseSprite;
    private int counter = 0;

    public Button hideUIButton;
    public Button showUIButton;
    public Image volumeHandle;
    public Image volumeBackground;
    public Toggle volumeToggle;
    public Material volumeBackgroundMaterial;
    public Material volumeHandleMaterial;

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
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = false;

        playButton = GameObject.Find("Play Button").GetComponent<Button>();

        hideUIButton = GameObject.Find("HideUIButton").GetComponent<Button>();       

        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();

        volumeHandle = GameObject.Find("VolumeHandle").GetComponent<Image>();

        volumeBackground = GameObject.Find("VolumeBackground").GetComponent<Image>();        

        volumeToggle = GameObject.Find("VolumeToggle").GetComponent<Toggle>();

    }

    void Update()
    {
        videoPlayer.SetDirectAudioVolume(0, volume);
    }

    public void ShowVolumeBar()
    {
        Debug.Log("is On == true");
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

    public void CivitasVideo()
    {
        Debug.Log("play Civitas Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.clip = vids[2];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
    }

    public void BadgeVideo()
    {
        Debug.Log("play Badge Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.clip = vids[3];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
    }

    public void ScoppechioVideo()
    {
        Debug.Log("play Scoppechio Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.clip = vids[0];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
    }

    public void ThreeAnimationVideo()
    {
        Debug.Log("play 3 Animation Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.clip = vids[4];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
    }

    public void ThreeLiveAction()
    {
        Debug.Log("play 3 Live Action Video");
        this.gameObject.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.clip = vids[1];
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.Direct;
    }
}
