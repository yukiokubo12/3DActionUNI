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
    // private bool deadGoblin = false;

    void Start()
    {
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
        // goblinStatus.GetComponent<GoblinStatus>().DestroyGoblin();
    }

    void Update()
    {
        // CountGoblin();
    }

    // public void CountGoblin()
    // {
        // if(deadGoblin = true)
        // {
            // goblinStatus.DestroyGoblin();
            // this.goblinCount++;
        // }
        
        // if(goblinStatus.DestroyGoblin())
        // {
            // this.goblinCount++;
        // }
        // this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
    // }
}
