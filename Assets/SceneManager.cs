using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject labPoint; // This is the upstairs room
    public Transform xrOriginTransform; // Reference to XR Origin for VR teleportation
    public GameObject brightnessSliderPanel;
    public Light pLight;
    public Slider slider;
    public Slider contrastSlider;
    public List<MeshRenderer> meshRenderers;
    public enum States
    {
        Down,
        Up
    }
    States currentstate;
    // Start is called before the first frame update
    void Start()
    {
        // Safely wire up UI only if references are assigned
        if (slider != null)
        {
            slider.onValueChanged.AddListener(onSliderchange);
        }

        currentstate = States.Down;

        if (contrastSlider != null)
        {
            contrastSlider.minValue = 0;
            contrastSlider.maxValue = 255;
            contrastSlider.onValueChanged.AddListener(OnContrastSliderChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlayButtonClick()
    {
        // Load the Lab scene (main menu Play button)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lab");
    }
    public void OnOptionsButtonClick()
    {
        brightnessSliderPanel.SetActive(true);
    }
     public void CloseBrightnessPanel()
     {
        brightnessSliderPanel.SetActive(false);
     }
     public void onSliderchange(float value) 
     {
        pLight.intensity = 2*value;
        Debug.Log(value);
     }
     void OnContrastSliderChanged(float value)
    {
        float normalized = value / 255f;  // convert to 0–1 range
        Color col = new Color(normalized, normalized, normalized, 1f);

        foreach (var mr in meshRenderers)
        {
            if (mr != null)
                mr.material.color = col;
        }
    }
}
