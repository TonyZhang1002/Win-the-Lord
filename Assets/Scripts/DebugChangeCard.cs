using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour {

	cardFlipper flipper;
	cardModel cardmodel;
	int cardIndex = 0;
	public GameObject card;

	void Awake () {
		cardmodel = card.GetComponent<cardModel> ();
		flipper = card.GetComponent<cardFlipper> ();
	}

	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 100, 28), "Hit me!")) {
			if (cardIndex >=cardmodel.faces.Length) {
				cardIndex = 0;
				flipper.FlipCard (cardmodel.faces [cardmodel.faces.Length - 1], cardmodel.cardBack, -1);
			} else {
				if (cardIndex > 0) {
					flipper.FlipCard (cardmodel.faces [cardIndex - 1], cardmodel.faces [cardIndex], cardIndex);
				} else {
					flipper.FlipCard (cardmodel.cardBack, cardmodel.faces [cardIndex], cardIndex);
				}
				cardIndex++;
			}
		}
	}
}
