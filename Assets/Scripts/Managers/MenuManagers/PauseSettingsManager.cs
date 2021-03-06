using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using Cinemachine;
using UnityEngine.EventSystems;

public class PauseSettingsManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private InitializeBasicSettings basicSettingsHandler;
    [SerializeField] private PlayerPreferencesHandler prefsHandler;
    [SerializeField] private RadioManager radioManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject audioTab;
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject preferencesTab;
    [SerializeField] private GameObject closeButton;

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
    [SerializeField] private Button crouchOnButton;
    [SerializeField] private Button crouchOffButton;
    [SerializeField] private Button sprintOnButton;
    [SerializeField] private Button sprintOffButton;
    [SerializeField] private Button verticalOnButton;
    [SerializeField] private Button verticalOffButton;
    [SerializeField] private Button horizontalOnButton;
    [SerializeField] private Button horizontalOffButton;

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


    #endregion Components


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
        if (radioManager == null)
        {
            radioManager = FindObjectOfType<RadioManager>();
        }
 



    }//END Init
    //-----------------------//
    private void OnEnable()
    //-----------------------//
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterAudio");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicAudio");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SFXAudio");

        gammaSlider.value = PlayerPrefs.GetFloat("Gamma");
        fullScreenToggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.GetInt("CrouchToggle") == 0)
        {
            crouchOnButton.interactable = true;
            crouchOffButton.interactable = false;

        }
        else
        {
            crouchOffButton.interactable = true;
            crouchOnButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("SprintToggle") == 0)
        {
            sprintOnButton.interactable = true;
            sprintOffButton.interactable = false;

        }
        else
        {
            sprintOffButton.interactable = true;
            sprintOnButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("InvertVerticalToggle") == 0)
        {
            verticalOnButton.interactable = true;
            verticalOffButton.interactable = false;

        }
        else
        {
            verticalOffButton.interactable = true;
            verticalOnButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 0)
        {
            horizontalOnButton.interactable = true;
            horizontalOffButton.interactable = false;

        }
        else
        {
            horizontalOffButton.interactable = true;
            horizontalOnButton.interactable = false;
        }
        //if (PlayerPrefs.GetInt("FlipUI") == 0)
        //{
        //    uIOnButton.interactable = true;
        //    uIOffButton.interactable = false;

        //}
        //else
        //{
        //    uIOffButton.interactable = true;
        //    uIOnButton.interactable = false;
        //}
        hueSlider.value = PlayerPrefs.GetFloat("RadioHue");
        saturationSlider.value = PlayerPrefs.GetFloat("RadioSaturation");
        lookSlider.value = PlayerPrefs.GetFloat("CamSensitivity");
        throwLookSlider.value = PlayerPrefs.GetFloat("ThrowSensitivity");
    }

    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {
        pauseMenu.SetActive(true);

        audioTab.SetActive(true); //Sets audio tab as default
        videoTab.SetActive(false);
        preferencesTab.SetActive(false);

        settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);


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


    public void DefaultControls()
    {
        basicSettingsHandler.SetDefaultSettings();
    }


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

        if (isFullScreen == true)
        {
            prefsHandler.SetFullScreenPref(1);
        }
        else
        {
            prefsHandler.SetFullScreenPref(0);

        }

    }//END SetFullScreen


    #endregion Video


    #region Preferences

    //-----------------------//
    public void CrouchToggle(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            prefsHandler.SetCrouchToggle(0);
            crouchOnButton.interactable = true;
            crouchOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetCrouchToggle(1);
            crouchOffButton.interactable = true;
            crouchOnButton.interactable = false;
        }

    }//END CrouchToggle

    //-----------------------//
    public void SprintToggle(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            prefsHandler.SetSprintToggle(0);
            sprintOnButton.interactable = true;
            sprintOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetSprintToggle(1);
            sprintOffButton.interactable = true;
            sprintOnButton.interactable = false;
        }

    }//END SprintToggle

    //-----------------------//
    public void ToggleHorizontal(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            prefsHandler.SetHorizontalToggle(1);
            freeLookCam.m_XAxis.m_InvertInput = true; 
            horizontalOnButton.interactable = true;
            horizontalOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetHorizontalToggle(0);
            freeLookCam.m_XAxis.m_InvertInput = false; 
            horizontalOffButton.interactable = true;
            horizontalOnButton.interactable = false;

        }

    }//END ToggleHorizontal

    //-----------------------//
    public void ToggleVertical(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            prefsHandler.SetVerticalToggle(1);
            freeLookCam.m_YAxis.m_InvertInput = true; 
            verticalOnButton.interactable = true;
            verticalOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetVerticalToggle(0);
            freeLookCam.m_YAxis.m_InvertInput = false;
            verticalOffButton.interactable = true;
            verticalOnButton.interactable = false;
        }

    }//END ToggleVertical

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

        radioManager.subtitleText.color = previewColor;

    }//END UpdateRadioHue

    //-----------------------//
    public void UpdateRadioSaturation(float saturation)
    //-----------------------//
    {
        Color previewColor = Color.HSVToRGB(PlayerPrefs.GetFloat("RadioHue"), saturation, 1);

        prefsHandler.SetRadioSaturation(saturation);

        radioImage.color = previewColor;

        radioManager.subtitleText.color = previewColor;


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
