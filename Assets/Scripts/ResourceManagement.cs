using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManagement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Text stoneText;

    [SerializeField]
    public Text goldText;

    [SerializeField]
    public float gold = 0, stone = 0;

    StonePile stonePile = new StonePile();

    void Start()
    {
        goldText.text = gold.ToString();
        stoneText.text = stone.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        stone = stonePile.GetAmount() * 8;
        stoneText.text = stone.ToString();
    }
  
}
