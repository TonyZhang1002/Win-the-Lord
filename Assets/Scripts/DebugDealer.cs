using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour {

	public CardStack dealer;
	public CardStack player;

	int count = 0;
	int[] cards = new int[] {9,12};

	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 256, 28),"Deal the Cards!")) {
			player.Push (dealer.Pop ());
		}
	}

	//void OnGUI(){
	//	if (GUI.Button (new Rect (10, 10, 256, 28),"Deal the Cards!")) {
	//		player.Push (cards [count++]);
	//	}
	//}
}
