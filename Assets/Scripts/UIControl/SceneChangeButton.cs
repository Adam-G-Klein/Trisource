using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Clickable))]
public class SceneChangeButton : MonoBehaviour
{
    public int sceneToChangeTo = 4;
    private Clickable clickable;
    // Start is called before the first frame update
    void Start()
    {
        clickable = GetComponent<Clickable>();
        if (sceneToChangeTo == -1)
        {
            //indicates we want to change to this scene
            sceneToChangeTo = SceneManager.GetActiveScene().buildIndex;
        }

    }

    // Update is called once per frame
    public void clicked()
    {
        print("changed to scene " + sceneToChangeTo.ToString());
        if (clickable.clickable)
        {
            Time.timeScale = 1; //for the niche case where we're transitioning from the pause menu
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToChangeTo);
        }
    }
}
