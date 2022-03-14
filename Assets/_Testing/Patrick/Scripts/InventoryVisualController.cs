using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryVisualController : MonoBehaviour
{
    [SerializeField] public GameObject InventoryUI;
    [SerializeField] public GameObject OpeningCutsceneUI;
    [SerializeField] private RectTransform CamScreen;
    [SerializeField] private Image blackScreen;
    [SerializeField] public TextMeshProUGUI camCount;
    //[SerializeField] private AudioClip cameraSFX;

    public bool useCoolTransition;

    public void SetupCutscene()
    {
        //swap out inventory for the temporary cutscene
        InventoryUI.SetActive(false);
        OpeningCutsceneUI.SetActive(true);
        blackScreen.enabled = true;

        //this.gameObject.AddComponent<AudioSource>().clip = cameraSFX;

        //fade from black
        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        var tempColor = blackScreen.color;
        float alpha = 1;
        tempColor.a = alpha;

        yield return new WaitForSeconds(0.5f);

        while (blackScreen.color.a > 0.1f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            alpha -= Time.deltaTime/1.5f;
            tempColor.a = alpha;
            blackScreen.color = tempColor;
        }

        tempColor.a = 0;
        blackScreen.color = tempColor;
        blackScreen.enabled = false;
    }

    public void ReturnToGameplay()
    {
        
        if(useCoolTransition)
        {
            StartCoroutine(CoolTransition());
        }else
        {
            InventoryUI.SetActive(true);
            OpeningCutsceneUI.SetActive(false);
        }
    }

    private IEnumerator CoolTransition()
    {
        float x = 1;
        while (x < 6)
        {
            CamScreen.localScale = new Vector3(x, x, x);
            x += Time.deltaTime * 20;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(0.25f);
        //disable cutscene canvas and enable proper one
        InventoryUI.SetActive(true);
        OpeningCutsceneUI.SetActive(false);
    }
}
