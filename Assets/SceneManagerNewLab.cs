using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneManagerNewLab : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject labPoint;
    public GameObject ClassPoint;
    public GameObject brightnessSliderPanel;
    public Light pLight;
    public Slider slider;
    public Slider contrastSlider;
    public List<MeshRenderer> meshRenderers;
    public AudioSource audioSource;
    public GameObject bubbleUIGO;
    [TextArea(2, 5)]
    public List<string> contents;
    public TMP_Text myText;
    int contentIndex;
    public enum States
    {
        Down,
        Up
    }
    States currentstate;
    // Start is called before the first frame update
    void Start()
    {
        if(slider !=null)
        {
            slider.onValueChanged.AddListener(onSliderchange);
            currentstate = States.Down;
            contrastSlider.minValue = 0;
            contrastSlider.maxValue = 255;
            contrastSlider.onValueChanged.AddListener(OnContrastSliderChanged);
        }
    }
    public void OnClickProfessor()
    {
        Debug.Log("Professor Pressed");
        if(audioSource != null)
            audioSource.Play();
        bubbleUIGO.SetActive(true);

        myText.text = contents[contentIndex];
        if(contentIndex < contents.Count-1)
        {
            contentIndex++;
        }
    }
    public void OnClickBack()
    {
        Debug.Log("Back Pressed");
        if(contentIndex > 0)
        {
            contentIndex--;
        }
        myText.text = contents[contentIndex];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPlayButtonClick()
    {
        cameraTransform.position = labPoint.transform.position;
        if(currentstate == States.Down)
        {
            currentstate = States.Up;
        }
        else
        {
            currentstate = States.Down;
        }
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
    public void OnTutorialClick()
    {
        cameraTransform.position = ClassPoint.transform.position;
        cameraTransform.rotation = ClassPoint.transform.rotation;
    }
}
