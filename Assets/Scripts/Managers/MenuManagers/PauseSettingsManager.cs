using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;

public class PauseSettingsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject audioTab;
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject preferencesTab;

    [SerializeField] private Image radioImage;

    [Header("Audio")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private AudioMixer mainMixer;

    [Header("Video")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private Slider gammaSlider;

    [Header("Preferences")]
    [SerializeField] private Toggle crouchToggle;
    [SerializeField] private Toggle sprintToggle;
    [SerializeField] private Toggle verticalToggle;
    [SerializeField] private Toggle horizontalToggle;
    [SerializeField] private Toggle uIToggle;

    [SerializeField] private Slider hueSlider;
    [SerializeField] private Slider saturationSlider;
    [SerializeField] private Slider lookSlider;
    [SerializeField] private Slider throwLookSlider;

    [Header("Tempe Crutch")]
    [SerializeField] CinemachineFreeLook freeLookCam;

    private Resolution[] screenResolutions;
    int currentResolutionIndex;

    bool isResFullScreen;

    private float screenWidth;
    private float screenHeight;
    private float screenRatio;

    private Resolution[] resolutions;

    #region Methods


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    void Init()
    //-----------------------//
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterAudio");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicAudio");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SFXAudio");

        gammaSlider.value = PlayerPrefs.GetFloat("Gamma");
        fullScreenToggle.isOn = Screen.fullScreen;

        if(PlayerPrefs.GetInt("CrouchToggle") == 0)
        {
            crouchToggle.isOn = false;
        }
        else
        {
            crouchToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("SprintToggle") == 0)
        {
            sprintToggle.isOn = false;
        }
        else
        {
            sprintToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("InvertVerticalToggle") == 0)
        {
            verticalToggle.isOn = false;
        }
        else
        {
            verticalToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 0)
        {
            horizontalToggle.isOn = false;
        }
        else
        {
            horizontalToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("FlipUI") == 0)
        {
            uIToggle.isOn = false;
        }
        else
        {
            uIToggle.isOn = true;
        }
        hueSlider.value = PlayerPrefs.GetFloat("RadioHue");
        saturationSlider.value = PlayerPrefs.GetFloat("RadioSaturation");
        lookSlider.value = PlayerPrefs.GetFloat("CamSensitivity");
        throwLookSlider.value = PlayerPrefs.GetFloat("ThrowSensitivity");



    }//END Init

    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        pauseMenu.SetActive(true);

        audioTab.SetActive(true); //Sets audio tab as default
        videoTab.SetActive(false);
        preferencesTab.SetActive(false);

        settingsMenu.SetActive(false);


    }//END ChangeScreen

    //-----------------------//
    public void ChangeTab(int tabValue)
    //-----------------------//
    {
        if (tabValue == 0)
        {
            audioTab.SetActive(true);
            videoTab.SetActive(false);
            preferencesTab.SetActive(false);

        }
        else if (tabValue == 1)
        {
            audioTab.SetActive(false);
            videoTab.SetActive(true);
            preferencesTab.SetActive(false);

        }
        else if (tabValue == 2)
        {
            audioTab.SetActive(false);
            videoTab.SetActive(false);
            preferencesTab.SetActive(true);

        }

    }//END ChangeTab


    #region Audio


    //-------------------------//
    public void MasterVolume(float masterVolume)
    //-------------------------//
    {
        Debug.Log($"Master volume value set to {masterVolume}");
        mainMixer.SetFloat("masterVolume", masterVolume);

    }//END MasterVolume

    //-------------------------//
    public void MusicVolume(float musicVolume)
    //-------------------------//
    {
        Debug.Log($"Music volume value set to {musicVolume}");
        mainMixer.SetFloat("musicVolume", musicVolume);


    }//END MusicVolume

    //-------------------------//
    public void SoundEffectsVolume(float soundEffectsVolume)
    //-------------------------//
    {
        Debug.Log($"Sound effects volume value set to {soundEffectsVolume}");
        mainMixer.SetFloat("sfxVolume", soundEffectsVolume);


    }//END SoundEffectsVolume


    #endregion Audio


    #region Video


    //-------------------------//
    public void SetQuality(int qualityIndex)
    //-------------------------//
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log($"Quality index set to {QualitySettings.GetQualityLevel()}");

    }//END QualitySettings

    //-------------------------//
    public void SetGamma(float gammaValue)
    //-------------------------//
    {
        Debug.Log($"Gamma value set to {gammaValue}");

        Screen.brightness = gammaValue;

    }//END SetGamma

    //-------------------------//
    public void SetFullScreen(bool isFullScreen)
    //-------------------------//
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log($"The fullscreen toggle is set to {isFullScreen}");

        isFullScreen = isResFullScreen;

    }//END SetFullScreen


    //--------------------------//
    private void AddResolutions()
    //--------------------------//
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width >= 1280 && resolutions[i].height >= 720)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                screenWidth = resolutions[i].width;
                screenHeight = resolutions[i].height;
                screenRatio = Mathf.Round((screenWidth / screenHeight) * 100) / 100;

                //Checking resolution against available aspect ratios
                if (screenRatio >= 1.7f && screenRatio <= 1.78f) //only 16:9 monitors
                {
                    options.Add(option);

                }
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }


        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

    }//END AddResolutions

    //--------------------------//
    public void SetResolution(int resolutionIndex)
    //--------------------------//
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isResFullScreen);

        Debug.Log($"Resolution Set to {Screen.currentResolution}");

    }//END SetResolution


    #endregion Video


    #region Preferences

    //-----------------------//
    public void CrouchToggle(bool isCrouchToggleOn)
    //-----------------------//
    {

        Debug.Log($"The crouch toggle is set to {isCrouchToggleOn}");

    }//END CrouchToggle

    //-----------------------//
    public void SprintToggle(bool isSprintToggleOn)
    //-----------------------//
    {

        Debug.Log($"The sprint toggle is set to {isSprintToggleOn}");

    }//END CrouchToggle

    //-----------------------//
    public void InvertHorizontalAxis(bool isInvertHorizontalOn)
    //-----------------------//
    {

        Debug.Log($"The horizontal invert toggle is set to {isInvertHorizontalOn}");

    }//END CrouchToggle

    //-----------------------//
    public void InvertVerticalAxis(bool isInvertVerticalOn)
    //-----------------------//
    {

        Debug.Log($"The vertical invert toggle is set to {isInvertVerticalOn}");

    }//END CrouchToggle

    public void AdjustHUD(bool isInventoryRight)
    //-----------------------//
    {

        Debug.Log($"The inventory toggle for the right side is set to {isInventoryRight}");

    }//END CrouchToggle


    #endregion Preferences


    //-----------------------//
    public void UpdatePreview()
    //-----------------------//
    {
        float hue = PlayerPrefs.GetFloat("RadioHue");
        float saturation = PlayerPrefs.GetFloat("RadioSaturation");

        Color previewColor = Color.HSVToRGB(hue, saturation, 1);

        radioImage.color = previewColor;

    }//END UpdatePreview


    //-----------------------//
    public void InvertHorizontalTemp(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            freeLookCam.m_XAxis.m_InvertInput = false;
        }
        else if (value == 1)
        {
            freeLookCam.m_XAxis.m_InvertInput = true;
        }

    }//END InvertHorizontalTemp

    //-----------------------//
    public void InvertVerticalTemp(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            freeLookCam.m_YAxis.m_InvertInput = false;
        }
        else if (value == 1)
        {
            freeLookCam.m_YAxis.m_InvertInput = true;
        }

    }//END InvertVerticalTemp

    #endregion Methods


}//END PauseSettingsManager
