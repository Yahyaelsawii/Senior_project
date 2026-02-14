using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPTManager : MonoBehaviour
{
    public List<Sprite> slideImages;
    public Image img;
    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        if(slideImages != null)
            img.sprite = slideImages[currentIndex];
    }
    public void OnClickNextButton()
    {
        if(currentIndex < slideImages.Count-1)
        {
            currentIndex++;
            img.sprite = slideImages[currentIndex];
        }
    }
    public void OnClickPrevButton()
    {
        if(currentIndex > 0)
        {
            currentIndex--;
            img.sprite = slideImages[currentIndex];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
