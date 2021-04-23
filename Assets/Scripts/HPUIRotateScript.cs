using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIRotateScript : MonoBehaviour
{
    //敵のHPスライダーをカメラへ常に向けておく
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
