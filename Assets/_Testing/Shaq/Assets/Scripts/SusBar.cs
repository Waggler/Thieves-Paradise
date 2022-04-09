using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SusBar : MonoBehaviour
{
    #region Variables

    [SerializeField] private Slider slider;

    [SerializeField] private Gradient gradient;

    [SerializeField] Image fill;

    [SerializeField] private EyeballScript eyeball;

    [SerializeField] private float susLevel;


    #endregion Variables


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = SetSusLevel(eyeball.susLevel);
    }

    private void Init()
    {
        slider.minValue = 0f;

        slider.maxValue = 10f;

        //fill.color = gradient.Evaluate(1f);

    }

    private float SetSusLevel(float susInput)
    {
        susInput = eyeball.susLevel;

        //fill.color = gradient.Evaluate(slider.normalizedValue);

        return susInput;
    }

}
