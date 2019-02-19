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

        playButton = GameObject.Find("Play Button").GetComponent<Button>();

        hideUIButton = GameObject.Find("HideUIButton").GetComponent<Button>();

        showUIButton = GameObject.Find("ShowUIButton").GetComponent<Button>();

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
        if(volumeToggle.isOn == true)
        {
            StartCoroutine(ShowVolumeBackgroundCo());
        }
        else
        {
            StartCoroutine(HideVolumeBackgroundCo());
        }     
    }

    IEnumerator ShowVolumeBackgroundCo()
    {
        

        if (volumeBackgroundMaterial.color.a == 1)
        {
            Debug.Log("start Show Loop");
            float alpha = volumeBackgroundMaterial.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0f, t));
                volumeBackgroundMaterial.color = newColor;
                yield return null;
            }
        }
        else
        {
            Debug.Log("wow");
            float alpha = volumeBackgroundMaterial.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0f, t));
                volumeBackgroundMaterial.color = newColor;
                yield return null;
            }
        }
        
        /*
        Color fullColor = new Color(255, 255, 255, 255);
        Color alphaColor = new Color(255, 255, 255, 0);
        Debug.Log("Show Volume Fill UI");

        if (volumeBackground.color.Equals(fullColor))
        {
            for (float t = 0.01f; t < fadeInTime; t += 0.01f)
            {
                Debug.Log("start Show Loop");
                volumeBackground.color = Color.Lerp(alphaColor, fullColor, t / fadeInTime);
                volumeHandle.color = Color.Lerp(alphaColor, fullColor, t / fadeInTime);
                yield return null;
            }
        }
        else
        {            
            yield return null;
            volumeBackground.color = new Color(255, 255, 255, 0);
            StartCoroutine(HideVolumeBackgroundCo());
        }*/
        
    }

        IEnumerator HideVolumeBackgroundCo()
    {
        if (volumeBackgroundMaterial.color.a.Equals(255))
        {
            float alpha = volumeBackgroundMaterial.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0f, t));
                volumeBackgroundMaterial.color = newColor;
                yield return null;
            }
        }
        else
        {
            float alpha = volumeBackgroundMaterial.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeInTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0255f, t));
                volumeBackgroundMaterial.color = newColor;
                yield return null;
            }
        }
        /*
        Color fullColor = new Color(255, 255, 255, 255);
        Color alphaColor = new Color(255, 255, 255, 0);
        Debug.Log("Hide Volume Fill UI");

        if (volumeBackground.color.Equals(alphaColor))
        {
            for (float t = 0.01f; t < fadeInTime; t += 0.01f)
            {
                Debug.Log("start Hide Loop");
                volumeBackground.color = Color.Lerp(fullColor, alphaColor, t/fadeInTime);
                volumeHandle.color = Color.Lerp(fullColor, alphaColor, t / fadeInTime);
                yield return null;
            }
        }
        else
        {
            yield return null;
            volumeBackground.color = new Color(255, 255, 255, 255);
            StartCoroutine(ShowVolumeBackgroundCo());
        }*/
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
