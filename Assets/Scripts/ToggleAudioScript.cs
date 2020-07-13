using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle), typeof(AudioSource))]
public class ToggleAudioScript : MonoBehaviour
{
    #region public variables
    public Toggle ToggleComponent;
    public AudioSource AudioSourceComponent;
    #endregion

    #region Unity methods
    void Start()
    {
        ToggleComponent = GetComponent<Toggle>();
        AudioSourceComponent = GetComponent<AudioSource>();
    }
    #endregion
}
