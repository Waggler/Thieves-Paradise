using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerPreferencesHandler prefsHandler;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject audioTab;
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject preferencesTab;



    [Header("Audio")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private AudioMixer mainMixer;

    [Header("Video")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropDown;

    [SerializeField] private Slider gammaSlider;

    [SerializeField] private Toggle fullScreenToggle;

    private Resolution[] screenResolutions;
    int currentResolutionIndex;

    bool isResFullScreen;

    private float screenWidth;
    private float screenHeight;
    private float screenRatio;

    private Resolution[] resolutions;

    [Header("Preferences")]
    [SerializeField] private Button crouchOnButton;
    [SerializeField] private Button crouchOffButton;
    [SerializeField] private Button sprintOnButton;
    [SerializeField] private Button sprintOffButton;
    [SerializeField] private Button verticalOnButton;
    [SerializeField] private Button verticalOffButton;
    [SerializeField] private Button horizontalOnButton;
    [SerializeField] private Button horizontalOffButton;
    [SerializeField] private Button uIOnButton;
    [SerializeField] private Button uIOffButton;

    public Button thirtyFPSButton;
    public Button sixtyFPSButton;

    [SerializeField] private Image radioImage;
    [SerializeField] private Slider lookSlider;
    [SerializeField] private Slider throwLookSlider;
    [SerializeField] private Slider hueSlider;
    [SerializeField] private Slider saturationSlider;

    [Header("Controller Support")]
    public Button audioFirstButton;
    public Button videoFirstButton;
    public Button preferencesFirstButton;
    public Button settingsCloseButton;

    #region Methods


    //-----------------------//
    private void Start()
    //-----------------------//
    {

        Init();

    }//END Start

    #region Statics & Startup

    //-----------------------//
    void Init()
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
        if (PlayerPrefs.GetInt("FlipUI") == 0)
        {
            uIOnButton.interactable = true;
            uIOffButton.interactable = false;

        }
        else
        {
            uIOffButton.interactable = true;
            uIOnButton.interactable = false;
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

        mainMenu.SetActive(true);

        audioTab.SetActive(true); //Sets audio tab as default
        videoTab.SetActive(false);
        preferencesTab.SetActive(false);

        settingsMenu.SetActive(false);

        // Have to null before reset
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsCloseButton.gameObject);


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
            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(audioFirstButton.gameObject);

        }
        else if (tabValue == 1)
        {
            audioTab.SetActive(false);
            videoTab.SetActive(true);
            preferencesTab.SetActive(false);
            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(videoFirstButton.gameObject);

        }
        else if (tabValue == 2)
        {
            audioTab.SetActive(false);
            videoTab.SetActive(false);
            preferencesTab.SetActive(true);
            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(preferencesFirstButton.gameObject);

        }

    }//END ChangeTab


    #endregion Statics & Startup


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
        Debug.Log($"Resolution should have been {resolution}");

    }//END SetResolution


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
            prefsHandler.SetHorizontalToggle(0);
            horizontalOnButton.interactable = true;
            horizontalOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetHorizontalToggle(1);
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
            //freeLookCam.m_YAxis.m_InvertInput = true;
            verticalOnButton.interactable = true;
            verticalOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetVerticalToggle(0);
            //freeLookCam.m_YAxis.m_InvertInput = false;
            verticalOffButton.interactable = true;
            verticalOnButton.interactable = false;
        }

    }//END ToggleVertical

    //-----------------------//
    public void ToggleInventory(int value)
    //-----------------------//
    {
        if (value == 0)
        {
            prefsHandler.SetUIToggle(1);
            uIOnButton.interactable = true;
            uIOffButton.interactable = false;
        }
        else
        {
            prefsHandler.SetUIToggle(0);
            uIOffButton.interactable = true;
            uIOnButton.interactable = false;
        }

    }//END ToggleInventory

    //-----------------------//
    public void LookSensitivity(float sensitivity)
    //-----------------------//
    {
        prefsHandler.SetLookSensitivity(sensitivity);

    }//END LookSensitivity

    //-----------------------//
    public void ThrowSensitivity(float sensitivity)
    //-----------------------//
    {
        prefsHandler.SetThrowSensitivity(sensitivity);

    }//END ThrowSensitivity

    //-----------------------//
    public void UpdateRadioHue(float hue)
    //-----------------------//
    {
        prefsHandler.SetRadioHue(hue);
        Color previewColor = Color.HSVToRGB(hue, PlayerPrefs.GetFloat("RadioSaturation"), 1);

        radioImage.color = previewColor;

    }//END UpdateRadioHue

    //-----------------------//
    public void UpdateRadioSaturation(float saturation)
    //-----------------------//
    {
        prefsHandler.SetRadioSaturation(saturation);
        Color previewColor = Color.HSVToRGB(PlayerPrefs.GetFloat("RadioHue"), saturation, 1);

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


}//END SettingsMenuManager
