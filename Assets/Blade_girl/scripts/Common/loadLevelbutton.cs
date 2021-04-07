using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadLevelbutton : MonoBehaviour {

	public Texture2D normalTex;
	public Texture2D hoverTex;

	public int loadLevel;





	private void OnMouseEnter ()
	{
		GetComponent<Texture>().texture = hoverTex;
	}

	private void OnMouseExit ()
	{

		GetComponent<Texture>().texture = normalTex;
	}


	private void OnMouseDown()
	{
		Application.LoadLevel(loadLevel);

	}







}
