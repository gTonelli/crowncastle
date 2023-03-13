using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject openFade;
    public GameObject ResourceUI;
    
    // Start is called before the first frame update
    void Start()
    {
        Show();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Hide();
            Time.timeScale = 1;
            openFade.SetActive(true);
            ResourceUI.SetActive(true);
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
