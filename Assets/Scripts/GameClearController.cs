using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearController : MonoBehaviour
{
    private GameObject[] goblinObjects;
 
    void Update()
    {
        goblinObjects = GameObject.FindGameObjectsWithTag("Goblin");
 
        if (goblinObjects.Length == 0)
        {
            SceneManager.LoadScene("GameClear");
        }
    }
}
