using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class recordButtonShine : MonoBehaviour
{
    public Button button;
    public Sprite oldPaleImg;
    public Sprite newShinyImg;
    private bool clickFlag = false;
    void Start(){
        button = GetComponent<Button>();
    }
    public void buttonClicked()
    {
        if (clickFlag)
        {
            clickFlag = false;
            button.image.overrideSprite = oldPaleImg;
        }
        else
        {
            clickFlag = true;
            button.image.overrideSprite = newShinyImg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
