using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WolfDeathCount : MonoBehaviour
{
    //ウルフが死んだ数数える（必要あればつかう）
    public Text wolfCountText;
    private int wolfCount = 0;
    public GameObject wolf;
    WolfStatus wolfStatus;

    void Start()
    {
        this.wolfCountText.text = string.Format("{0} / 3", wolfCount);
    }

    void Update()
    {
        CountWolf(0);
    }

    public void CountWolf(int addwolf)
    {   
        this.wolfCount = wolfCount + addwolf;
        this.wolfCountText.text = string.Format("{0} / 3", wolfCount);
        if(wolfCount >= 3)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
}
