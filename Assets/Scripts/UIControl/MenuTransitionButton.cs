using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class MenuTransitionButton : MonoBehaviour
{
    public GameObject currentMenuWrapper;
    private ButtonGroupAlphaControls currentMenu;
    public GameObject nextMenuWrapper;

    private ButtonGroupAlphaControls nextMenu;
    private Clickable clickable;

    void Start()
    {
        clickable = GetComponent<Clickable>();
        nextMenu = nextMenuWrapper.GetComponent<ButtonGroupAlphaControls>();
        currentMenu = currentMenuWrapper.GetComponent<ButtonGroupAlphaControls>();
    }

    public void onClick()
    {
        if (clickable.clickable)
        {
            nextMenu.displayAll();
            currentMenu.hideAll();
            print("displaying level menu");
        }
    }

}
