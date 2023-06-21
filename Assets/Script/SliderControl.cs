using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Button button;
    [SerializeField] private PlayerAttack playerAttack;

    private bool isPlaying = false;
    private float pausedValue;
    private bool end;
   
    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        isPlaying = true;
        end = false;
    }

    private void Update()
    {
        SliderPlay();
    }

    private void SliderPlay()
    {
        if (isPlaying)
        {
            if (slider.value < 100f && end == false)
            {
                slider.value += Time.deltaTime * 80f;
                if (slider.value == 100)
                {
                    end = true;
                }
            }
            else
            {
                slider.value -= Time.deltaTime * 80f;
                if (slider.value <= 0)
                {
                    end = false;
                }

            }
        }
    }

    private void OnButtonClick()
    {
        if (isPlaying)
        {
            isPlaying = false;
            pausedValue = slider.value;
        }
        if (pausedValue > 50f)
        {
            playerAttack.Jump();
        }
    }
}
