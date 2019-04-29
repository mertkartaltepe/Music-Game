using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonShine : MonoBehaviour
{
    public Button button;
    public Sprite oldPaleImg;
    public Sprite newShinyImg;


    void Start(){
        button = GetComponent<Button>();
    }
    public void buttonClicked()
    {
        StartCoroutine(waitClick(0.4f));
    }

    IEnumerator waitClick(float duration){
        button.image.overrideSprite = newShinyImg;
        yield return new WaitForSeconds(duration);
        button.image.overrideSprite = oldPaleImg;
    }

    void Update()
    {
        
    }
}
