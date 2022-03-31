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
    [SerializeField] private PlayerPreferencesHandler prefsHandler;
    [SerializeField] private InputManager inputManager;
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

    [SerializeField] private Button thirtyFPSButton;
    [SerializeField] private Button sixtyFPSButton;

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
        if (inputManager == null)
        {
            inputManager = (InputManager)FindObjectOfType(typeof(InputManager));
        }
        if (freeLookCam == null)
        {
            freeLookCam = (CinemachineFreeLook)FindObjectOfType(typeof(CinemachineFreeLook));
        }

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
        prefsHandler.SetMasterAudioPref(masterVolume);

    }//END MasterVolume

    //-------------------------//
    public void MusicVolume(float musicVolume)
    //-------------------------//
    {
        Debug.Log($"Music volume value set to {musicVolume}");
        mainMixer.SetFloat("musicVolume", musicVolume);
        prefsHandler.SetMusicAudioPref(musicVolume);

    }//END MusicVolume

    //-------------------------//
    public void SoundEffectsVolume(float soundEffectsVolume)
    //-------------------------//
    {
        Debug.Log($"Sound effects volume value set to {soundEffectsVolume}");
        mainMixer.SetFloat("sfxVolume", soundEffectsVolume);
        prefsHandler.SetSFXAudioPref(soundEffectsVolume);

    }//END SoundEffectsVolume


    #endregion Audio


    #region Video


    //-------------------------//
    public void SetQuality(int qualityIndex)
    //-------------------------//
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log($"Quality index set to {QualitySettings.GetQualityLevel()}");
        prefsHandler.SetQualityPref(qualityIndex);

    }//END QualitySettings

    //-------------------------//
    public void SetGamma(float gammaValue)
    //-------------------------//
    {
        Debug.Log($"Gamma value set to {gammaValue}");

        Screen.brightness = gammaValue;
        prefsHandler.SetGammaPref(gammaValue);

    }//END SetGamma

    //-------------------------//
    public void SetFullScreen(bool isFullScreen)
    //-------------------------//
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log($"The fullscreen toggle is set to {isFullScreen}");

        isFullScreen = isResFullScreen;
        
        if(isFullScreen== true)
        {
            prefsHandler.SetFullScreenPref(1);
        }
        else
        {
            prefsHandler.SetFullScreenPref(0);

        }

    }//END SetFullScreen

    /*
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
    */

    #endregion Video


    #region Preferences

    //-----------------------//
    public void CrouchToggle(Toggle value)
    //-----------------------//
    {
        if (value == false)
        {
            prefsHandler.SetCrouchToggle(0);
        }
        else
        {
            prefsHandler.SetCrouchToggle(1);

        }

    }//END CrouchToggle

    //-----------------------//
    public void SprintToggle(Toggle value)
    //-----------------------//
    {
        if (value == false)
        {
            prefsHandler.SetSprintToggle(0);
        }
        else
        {
            prefsHandler.SetSprintToggle(1);

        }

    }//END SprintToggle

    //-----------------------//
    public void ToggleHorizontal(Toggle value)
    //-----------------------//
    {
        if (value == true)
        {
            prefsHandler.SetHorizontalToggle(1);
            freeLookCam.m_XAxis.m_InvertInput = true;
        }
        else
        {
            prefsHandler.SetHorizontalToggle(0);
            freeLookCam.m_XAxis.m_InvertInput = false;

        }

    }//END ToggleHorizontal

    //-----------------------//
    public void ToggleVertical(Toggle value)
    //-----------------------//
    {
        if (value == true)
        {
            prefsHandler.SetVerticalToggle(1);
            freeLookCam.m_YAxis.m_InvertInput = true;

        }
        else
        {
            prefsHandler.SetVerticalToggle(0);
            freeLookCam.m_YAxis.m_InvertInput = false;

        }

    }//END ToggleVertical

    //-----------------------//
    public void ToggleInventory(Toggle value)
    //-----------------------//
    {
        if (value == true)
        {
            prefsHandler.SetUIToggle(1);

        }
        else
        {
            prefsHandler.SetUIToggle(0);

        }

    }//END ToggleInventory

    //-----------------------//
    public void LookSensitivity(float sensitivity)
    //-----------------------//
    {
        prefsHandler.SetLookSensitivity(sensitivity);
        freeLookCam.m_XAxis.m_MaxSpeed = 300 * sensitivity;
        freeLookCam.m_YAxis.m_MaxSpeed = 2 * sensitivity;

    }//END LookSensitivity

    //-----------------------//
    public void ThrowSensitivity(float sensitivity)
    //-----------------------//
    {
        prefsHandler.SetThrowSensitivity(sensitivity);
        inputManager.ChangeZoomLookSensitivity(sensitivity);


    }//END ThrowSensitivity

    //-----------------------//
    public void UpdateRadioHue(float hue)
    //-----------------------//
    {

        Color previewColor = Color.HSVToRGB(hue, PlayerPrefs.GetFloat("RadioSaturation"), 1);

        prefsHandler.SetRadioHue(hue);

        radioImage.color = previewColor;

    }//END UpdateRadioHue

    //-----------------------//
    public void UpdateRadioSaturation(float saturation)
    //-----------------------//
    {
        Color previewColor = Color.HSVToRGB(PlayerPrefs.GetFloat("RadioHue"), saturation, 1);

        prefsHandler.SetRadioSaturation(saturation);

        radioImage.color = previewColor;

    }//END UpdateRadioSaturation

    //-----------------------//
    public void SetFPS(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            Application.targetFrameRate = 30;

            thirtyFPSButton.interactable = false;
            sixtyFPSButton.interactable = true;

            Debug.Log("FPS set to 30");

            prefsHandler.SetFPS(0);

        }
        if (value == 1)
        {
            Application.targetFrameRate = 60;

            sixtyFPSButton.interactable = false;
            thirtyFPSButton.interactable = true;

            Debug.Log("FPS set to 60");

            prefsHandler.SetFPS(1);

        }

    }//END SetFPS


    #endregion Preferences


    #endregion Methods


}//END PauseSettingsManager
