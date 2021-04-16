using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinDeathCount : MonoBehaviour
{
    public Text goblinCountText;
    private int goblinCount = 0;
    public GameObject Goblin;
    GoblinStatus goblinStatus;

    void Start()
    {
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
    }

    void Update()
    {
        CountGoblin(0);
    }

    public void CountGoblin(int addGoblin)
    {    
        this.goblinCount = addGoblin;
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
    }
}
