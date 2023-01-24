using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public GameObject panel;

    KeyboardController keyboardControl;
    DragController dragControl;
    SwipeController swipeControl;

    private void Start()
    {
        keyboardControl = GetComponent<KeyboardController>();
        dragControl = FindObjectOfType<Canvas>().GetComponent<DragController>();
        swipeControl = FindObjectOfType<Canvas>().GetComponent<SwipeController>();

        keyboardControl.enabled = true;
        dragControl.enabled = false;
        swipeControl.enabled = false;
    }

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Keyboard()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;

        keyboardControl.enabled = true;
        dragControl.enabled = false;
        swipeControl.enabled = false;

    }
    public void Drag()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;

        keyboardControl.enabled = false;
        dragControl.enabled = true;
        swipeControl.enabled = false;
    }
    public void Swipe()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;

        keyboardControl.enabled = false;
        dragControl.enabled = false;
        swipeControl.enabled = true;

    }


}
