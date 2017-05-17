using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardModel : MonoBehaviour {
	SpriteRenderer spriteRenderer;

	public Sprite[] faces;
	public Sprite cardBack;

	public int cardIndex;	//use for faces[cardIndex]

	public void ToggleFace(bool showFace){
		if (showFace) {
			spriteRenderer.sprite = faces [cardIndex];// show the card face
		} else {
			spriteRenderer.sprite = cardBack;// show the card back
		}
	}

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
}
