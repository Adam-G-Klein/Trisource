using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class PlayButton : MonoBehaviour
{
    public GameObject levelMenuWrapper;
    private ButtonGroupAlphaControls levelButtons;
    public GameObject mainMenuWrapper;

    private ButtonGroupAlphaControls mainMenuButtons;
    private Clickable clickable;

    void Start()
    {
        clickable = GetComponent<Clickable>();
        levelButtons = levelMenuWrapper.GetComponent<ButtonGroupAlphaControls>();
        mainMenuButtons = mainMenuWrapper.GetComponent<ButtonGroupAlphaControls>();
    }

    public void onClick()
    {
        if (clickable.clickable)
        {
            levelButtons.displayAll();
            mainMenuButtons.hideAll();
            print("displaying level menu");
        }
    }

}