using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButton : MonoBehaviour {

	public Texture2D normalTex;
	public Texture2D hoverTex;

	public GameObject frog;
	protected Animator avatar;

	void Start () 
	{
		avatar = frog.GetComponent<Animator>();
		avatar.SetBool("Skill", false);
	}

	
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
		avatar.SetBool("Skill", true);
		
	}

	private void OnMouseUp()
	{
		avatar.SetBool("Skill", false);
	}

}
