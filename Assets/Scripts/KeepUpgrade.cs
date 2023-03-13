using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUpgrade : MonoBehaviour
{

    public GameObject castle1;
    public GameObject castle2;
    public GameObject castle3;
    private int currentLevel = 1;
    [SerializeField] private AudioSource NotEnoughSound;
    public GameObject trebuchetSpawner;
    public GameObject mangaonelSpawner;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentLevel);
    }

    public void Interact() {
        Debug.Log("Got Interaction");
        if (Player.Instance.Stone >= 4) {
            currentLevel++;
            Player.Instance.Stone = Player.Instance.Stone - 4;
            if (currentLevel == 2) {
                castle1.SetActive(false);
                castle2.SetActive(true);
                trebuchetSpawner.SetActive(true);
            } else if (currentLevel == 3) {
                castle2.SetActive(false);
                castle3.SetActive(true);
                mangaonelSpawner.SetActive(true);
            }
        } else if (Player.Instance.Stone < 4) {
            NotEnoughSound.enabled = true;
            NotEnoughSound.Play();
        }
    }

}
