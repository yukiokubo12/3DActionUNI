using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoblinDeathCount : MonoBehaviour
{
    public Text goblinCountText;
    private int goblinCount = 0;
    public GameObject Goblin;
    GoblinStatus goblinStatus;

    public FadeController fadeController;

    public GameObject missionCompleteText;

    public bool isMissionComplete;

    void Start()
    {
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);

        isMissionComplete = false;
    }

    void Update()
    {
        CountGoblin(0);
    }

    public void CountGoblin(int addGoblin)
    {   
        this.goblinCount = goblinCount + addGoblin;
        this.goblinCountText.text = string.Format("{0} / 3", goblinCount);
        if(goblinCount >= 3 && isMissionComplete == false)
        {
            isMissionComplete = true;
            missionCompleteText.GetComponent<Text>().text = "Mission Complete";
            Invoke("ToMission2", 3);
            // ToMission2();
            // SceneManager.LoadScene("Mission2");
        }
    }

    void ToMission2()
	{
		fadeController.StartFadeOut();
		fadeController.changeSceneName = "Mission2";
	}
}
