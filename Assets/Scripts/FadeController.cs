using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour 
{
	//透明度が変わるスピードを管理
	float fadeSpeed = 0.02f;        
	//パネルの色、不透明度を管理
	float red, green, blue, alfa;   
  //フェードアウト処理の開始、完了を管理フラグ
	public bool isFadeOut = false;  
	//フェードイン処理の開始、完了を管理フラグ
	public bool isFadeIn = false;   
	//次シーンでフェードインが必要かどうか
	public static bool needFadeIn = false; 
	//透明度を変更するパネルのイメージ
	Image fadeImage;
	//シーン名変更
	public string changeSceneName;
 
	void Start () 
	{
		fadeImage = GetComponent<Image> ();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
		if(needFadeIn)
		{
			needFadeIn = false;
			StartFadeIn();
		}
	}
 
	void Update () 
	{
		if(isFadeIn)
		{
			UpdateFadeIn ();
		}
 
		if (isFadeOut) 
		{
			UpdateFadeOut ();
		}
	}
	
	void StartFadeIn()
	{
		alfa = 1.0f;
		isFadeIn = true;
		fadeImage.enabled = true;
	}
	
	public void StartFadeOut()
	{
		alfa = 0.0f;
		isFadeOut = true;
		fadeImage.enabled = true;
	}
 
	void UpdateFadeIn()
	{
		alfa -= fadeSpeed;                
		SetAlpha ();                      
		if(alfa <= 0)											
		{                    
			isFadeIn = false;             
			fadeImage.enabled = false;      
		}
	}
 
	void UpdateFadeOut()
	{
		fadeImage.enabled = true;  
		alfa += fadeSpeed;         
		SetAlpha ();               
		if(alfa >= 1)							 
		{             
			isFadeOut = false;

			if(changeSceneName != "")
			{
				needFadeIn = true;
				SceneManager.LoadScene(changeSceneName);
			}
		}
	}
 
	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}
	
	public void ToGameScene1()
	{
		StartFadeOut();
		changeSceneName = "GameScene 1";
	}
}
