using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonGroupAlphaControls : MonoBehaviour
{
    public List<GameObject> buttons;
    private List<Clickable> clickables = new List<Clickable>();
    public List<GameObject> images;
    private List<Material> buttonMats = new List<Material>();
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    [SerializeField]
    private float buttonToTextAlphaRatio2 = 1 / 28;
    private float buttonToImageAlphaRatio = 1 / 60;
    private float maxButtonMatAlpha = 28;
    public float displayTime = 0.7f;
    public float initAlpha = 0;
    public float imageEndAlpha = 0.4f;
    public bool initActive = false;
    private TextGroupAlphaControls textGroup;

    // Start is called before the first frame update
    void Start()
    {
        textGroup = GetComponent<TextGroupAlphaControls>();

        foreach (GameObject obj in buttons)
        {
            Material btnMat = obj.GetComponent<Image>().materialForRendering;
            buttonMats.Add(btnMat);
            Clickable clickable = obj.GetComponent<Clickable>();
            clickables.Add(clickable);
            obj.SetActive(initActive);
            if(clickable) clickable.clickable = initActive;
        }

        foreach (GameObject obj in images)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.b, renderer.color.g, initAlpha);
            sprites.Add(renderer);
            obj.SetActive(initActive);
        }

        foreach (Material mat in buttonMats)
        {
            mat.SetFloat("_publicAlpha", initAlpha);
        }
        SendMessageUpwards("initDone", null, SendMessageOptions.DontRequireReceiver);
    }

    void OnEnable(){
        buttonToTextAlphaRatio2 = 1 / 28;
    }

    void Update()
    {
        buttonToTextAlphaRatio2 = 1 / 28;

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (buttonMats[0].GetFloat("_publicAlpha") == initAlpha)
            {
                displayAll();
            }
            else
            {
                hideAll();
            }
        }
    }

    public void displayAll()
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(true);
        }

        foreach (GameObject img in images)
        {
            img.SetActive(true);
        }
        if(textGroup) textGroup.displayAll();
        tweenButtonsAlphaTo(maxButtonMatAlpha, displayTime);
        tweenImageAlphaTo(imageEndAlpha, displayTime);
        Invoke("setAllClickable", displayTime);
    }

    //this grossness because invoke can't take an argument 
    // and I didn't want to start another tween
    private void setAllClickable()
    {
        foreach (Clickable c in clickables)
        {
            if(c) c.clickable = true;
        }
    }
    private void setAllUnClickable()
    {
        foreach (Clickable c in clickables)
        {
            if(c) c.clickable = false;
        }
    }
    public void hideAll()
    {
        tweenButtonsAlphaTo(0, displayTime);
        tweenImageAlphaTo(0, displayTime);
        if(textGroup) textGroup.hideAll();
        setAllUnClickable(); //don't need to be clickable as they're fading out
        Invoke("deactivateAllButtons", displayTime);
    }

    public void tweenButtonsAlphaTo(float to, float time)
    {
        foreach (Material mat in buttonMats)
        {
            LeanTween.value(
            gameObject, mat.GetFloat("_publicAlpha"), to, time)
            .setOnUpdate((float val) =>
            {
                mat.SetFloat("_publicAlpha", val);
            });
        }
    }

    public void tweenImageAlphaTo(float to, float time){
        foreach (SpriteRenderer renderer in sprites)
        {
            LeanTween.value(
            gameObject, renderer.color.a, to, time)
            .setOnUpdate((float val) =>
            {
                renderer.color = new Color(renderer.color.r, renderer.color.b, renderer.color.g, val);
            });
        }
    }

    private void deactivateAllButtons()
    {

        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }
    }

}
