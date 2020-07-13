using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class MenuScript : MonoBehaviour
{
    #region public variables
    public Button PopupButton;
    public Button TextButton;
    public Button AudioButton;
    [Space]
    public List<Toggle> AudioToogleList = new List<Toggle>();
    [Space]
    public Canvas PopUpCanvas;
    public Canvas TextCanvas;
    #endregion

    #region private variables
    private Animator popUpAnimator;
    private float timer = 0;
    private List<AudioSource> audioSourceList = new List<AudioSource>();

    private const string animatorShowParameter = "Show";
    private const int textShowTime = 3;
    #endregion

    #region Unity methods
    void Start()
    {
        PopupButton.WarnIfReferenceIsNull(gameObject);
        TextButton.WarnIfReferenceIsNull(gameObject);
        AudioButton.WarnIfReferenceIsNull(gameObject);
        PopUpCanvas.WarnIfReferenceIsNull(gameObject);
        TextCanvas.WarnIfReferenceIsNull(gameObject);

        popUpAnimator = PopUpCanvas.GetComponent<Animator>();
        popUpAnimator.WarnIfReferenceIsNull(gameObject);

        if (TextCanvas != null)
        {
            TextCanvas.enabled = false;
        }
        else
        {
            Debug.Log($"TextCanvas is null!", gameObject);
        }

        if (AudioToogleList.Count <= 0)
        {
            Debug.LogWarning("AudioToogle list is empty!", gameObject);
        }
        else
        {
            InitAudioSamples();
        }
    }

    void Update()
    {
        if (TextCanvas != null && TextCanvas.enabled)
        {
            if (timer <= textShowTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                TextCanvas.enabled = false;
            }
        }
    }
    #endregion

    #region public methods
    public void ShowPopUp(bool show)
    {
        popUpAnimator.SetBool(animatorShowParameter, show);
    }

    public void ShowText()
    {
        if (TextCanvas != null && TextCanvas.enabled == false)
        {
            TextCanvas.enabled = true;
            timer = 0.0f;
        }
    }

    public void PlayRandomAudio()
    {
        if (audioSourceList.Count > 0)
        {
            audioSourceList[Random.Range(0, audioSourceList.Count)].Play();
        }
    }

    public void ManageToggleAudioSource(ToggleAudioScript toggleAudio)
    {
        if (toggleAudio.ToggleComponent.isOn)
        {
            AddAudioSource(toggleAudio.AudioSourceComponent);
        }
        else
        {
            RemoveAudioSource(toggleAudio.AudioSourceComponent);
        }
    }
    #endregion

    #region private methods
    private void InitAudioSamples()
    {
        foreach (var toogle in AudioToogleList)
        {
            if (toogle.isOn)
            {
                var audio = toogle.GetComponentInChildren<AudioSource>();

                if (audio != null && !audioSourceList.Contains(audio))
                {
                    audioSourceList.Add(audio);
                }
            }
        }
    }

    private void AddAudioSource(AudioSource audioSource)
    {
        if (!audioSourceList.Contains(audioSource))
        {
            audioSourceList.Add(audioSource);
        }
    }

    private void RemoveAudioSource(AudioSource audioSource)
    {
        if (audioSourceList.Contains(audioSource))
        {
            audioSourceList.Remove(audioSource);
        }
    }
    #endregion
}
