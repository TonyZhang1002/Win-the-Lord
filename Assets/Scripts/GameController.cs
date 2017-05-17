using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public CardStack player1;
	public CardStack player2;
	public CardStack player3;
	public CardStack deck;
	public CardStack prePlayedDeck;
	public Button player1Button;
	public Button player1Pass;
	public Button player2Button;
	public Button player2Pass;
	public Button player3Button;
	public Button player3Pass;

	public Text theText;

	List<int> prePlayer1;
	List<int> prePlayer2;
	List<int> prePlayer3;
	List<int> tempPlay;
	List<int> tempPlayer;
	List<int> hasPlayed;
	List<int> p1Start;
	List<int> p2Start;
	List<int> p3Start;
	int p1StartCardsNumber;
	int p2StartCardsNumber;
	int p3StartCardsNumber;
	int tempPri;
	int playPri;
	int tempType;
	int trigger1;
	int trigger2;
	int trigger3;
	int passTrigger;
	int lord;

	void Start(){
		Screen.SetResolution(1120, 630, true);

		p1Start = new List<int> ();
		p2Start = new List<int> ();
		p3Start = new List<int> ();
		prePlayer1 = new List<int> ();
		prePlayer2 = new List<int> ();
		prePlayer3 = new List<int> ();
		hasPlayed = new List<int> ();
		tempPlay = new List<int> ();
		tempPlayer = new List<int> ();
		tempPri = -1;
		playPri = -1;
		tempType = -1;
		passTrigger = 0;

		StartGame ();

		if (lord == 0) {
			trigger1 = 1;
		} else if (lord == 1) {
			trigger2 = 1;
		} else {
			trigger3 = 1;
		}
	}

	void Update(){
		if (trigger3 == 1) {
			player3Choose ();
			player1Button.interactable = false;
			player1Pass.interactable = false;
			player2Button.interactable = false;
			player2Pass.interactable = false;
			player3Button.interactable = true;
			player3Pass.interactable = true;
		} else if (trigger1 == 1) {
			player1Choose ();
			player1Button.interactable = true;
			player1Pass.interactable = true;
			player2Button.interactable = false;
			player2Pass.interactable = false;
			player3Button.interactable = false;
			player3Pass.interactable = false;
		} else if (trigger2 == 1) {
			player2Choose ();
			player1Button.interactable = false;
			player1Pass.interactable = false;
			player2Button.interactable = true;
			player2Pass.interactable = true;
			player3Button.interactable = false;
			player3Pass.interactable = false;
		} else {
		}
		if (checkIfEnd () == 1) {
			if (lord == 1) {
				theText.text = "Lord wins !";
			} else {
				theText.text = "Framers win !";
			}
		} else if (checkIfEnd () == 2) {
			if (lord == 2) {
				theText.text = "Lord wins !";
			} else {
				theText.text = "Framers win !";
			}
		} else if (checkIfEnd () == 3) {
			if (lord == 3) {
				theText.text = "Lord wins !";
			} else {
				theText.text = "Framers win !";
			}
		}
		/*
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log (Input.mousePosition.x + "   " + Input.mousePosition.y);
		}
		*/
	}

	void StartGame(){
		for (int i = 0; i < 17; i++) {
			player1.Push (deck.Pop ());
			player2.Push (deck.Pop ());
			player3.Push (deck.Pop ());
		}

		lord = Random.Range (0, 3);
		if (lord == 0) {
			for (int j = 0; j < 3; j++) {
				player1.Push (deck.Pop ());
			}
		} else if (lord == 1) {
			for (int j = 0; j < 3; j++) {
				player2.Push (deck.Pop ());
			}
		} else {
			for (int j = 0; j < 3; j++) {
				player3.Push (deck.Pop ());
			}
		}

		foreach (int i in player1.GetCards()) {
			p1Start.Add (i);
		}
		foreach (int i in player2.GetCards()) {
			p2Start.Add (i);
		}
		foreach (int i in player3.GetCards()) {
			p3Start.Add (i);
		}
		p1StartCardsNumber = player1.CardCount;
		p2StartCardsNumber = player2.CardCount;
		p3StartCardsNumber = player3.CardCount;
		theText.text = "Player " + (lord+1) + " is the LORD!!!";

	}

	int checkIfEnd(){
		if (player1.CardCount == 0)
			return 1;
		else if (player2.CardCount == 0)
			return 2;
		else if (player3.CardCount == 0)
			return 3;
		else
			return 0;
	}

	void player1Choose(){
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < p1StartCardsNumber; i++) {
				if (Input.mousePosition.x > (659 + 19 * i) && Input.mousePosition.x <= (678 + 19 * i) && Input.mousePosition.y > 276 && Input.mousePosition.y < 362) {
					//Debug.Log (player2.CardCount);
					//Debug.Log ("HIT!");
					//Debug.Log (player2.GetCard (i));
					if (hasPlayed.Contains (p1Start[i])) {
						theText.text = "Your need to choose again !";
					} else {
						if (prePlayer1.Contains (p1Start[i])) {
							prePlayer1.Remove (p1Start[i]);
						} else {
							prePlayer1.Add (p1Start[i]);
							prePlayer1.Sort ();
						}
					}
				}
			}
		}
	}

	public void player1Plays(){
		if (prePlayer3.Count != 0) {
			int h = PlayCards (prePlayer3);
			//Debug.Log (h);
			if (h == -1) {
				theText.text = "You can not play these cards !";
				prePlayer3.Clear ();
				//player3Choose ();
				trigger1 = 0;
				trigger2 = 0;
				trigger3 = 1;
			} else {
				//prePlayedDeck.Clear ();
				foreach (int i in prePlayer3) {
					player3.Remove (i);
					hasPlayed.Add (i);
					//prePlayedDeck.Push (i);
				}
				prePlayer3.Clear ();
				//player1Choose ();
				trigger1 = 1;
				trigger2 = 0;
				trigger3 = 0;
			}
		} else {
			theText.text = "You need to choose some cards to play !";
		}
	}

	public void play1PassButton(){
		prePlayer1.Clear ();
		trigger1 = 0;
		trigger2 = 1;
		trigger3 = 0;
		passTrigger++;
		if (passTrigger == 2) {
			passTrigger = 0;
			tempPlay.Clear ();
			tempPri = -1;
			tempType = -1;
		}
	}

	void player2Choose(){
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < p2StartCardsNumber; i++) {
				if (Input.mousePosition.x > (279 + 19 * i) && Input.mousePosition.x <= (298 + 19 * i) && Input.mousePosition.y > 87 && Input.mousePosition.y < 171) {
					//Debug.Log (player2.CardCount);
					//Debug.Log ("HIT!");
					//Debug.Log (player2.GetCard (i));
					if (hasPlayed.Contains (p2Start[i])) {
						theText.text = "Your need to choose again !";
					} else {
						if (prePlayer2.Contains (p2Start[i])) {
							prePlayer2.Remove (p2Start[i]);
						} else {
							prePlayer2.Add (p2Start[i]);
							prePlayer2.Sort ();
						}
					}
				}
			}
		}
	}

	public void player2Plays(){
		if (prePlayer1.Count != 0) {
			int h = PlayCards (prePlayer1);
			//Debug.Log (h);
			if (h == -1) {
				theText.text = "You can not play these cards !";
				prePlayer1.Clear ();
				//player1Choose ();
				trigger1 = 1;
				trigger2 = 0;
				trigger3 = 0;
			} else {
				//prePlayedDeck.Clear ();
				foreach (int i in prePlayer1) {
					player1.Remove (i);
					hasPlayed.Add (i);
					//prePlayedDeck.Push (i);
				}
				prePlayer1.Clear ();
				//player2Choose ();
				trigger1 = 0;
				trigger2 = 1;
				trigger3 = 0;
			}
		} else {
			theText.text = "You need to choose some cards to play !";
		}
	}

	public void play2PassButton(){
		prePlayer2.Clear ();
		trigger1 = 0;
		trigger2 = 0;
		trigger3 = 1;
		passTrigger++;
		if (passTrigger == 2) {
			passTrigger = 0;
			tempPlay.Clear ();
			tempPri = -1;
			tempType = -1;
		}
	}

	void player3Choose(){
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < p3StartCardsNumber; i++) {
				if (Input.mousePosition.x > (279 + 19 * i) && Input.mousePosition.x <= (298 + 19 * i) && Input.mousePosition.y > 531 && Input.mousePosition.y < 615) {
					//Debug.Log (player2.CardCount);
					//Debug.Log ("HIT!");
					//Debug.Log (player2.GetCard (i));
					if (hasPlayed.Contains (p3Start[i])) {
						theText.text = "Your need to choose again !";
					} else {
						if (prePlayer3.Contains (p3Start[i])) {
							prePlayer3.Remove (p3Start[i]);
						} else {
							prePlayer3.Add (p3Start[i]);
							prePlayer3.Sort ();
						}
					}
				}
			}
		}
	}

	public void player3Plays(){
		if (prePlayer2.Count != 0) {
			int h = PlayCards (prePlayer2);
			//Debug.Log (h);
			if (h == -1) {
				theText.text = "You can not play these cards !";
				prePlayer2.Clear ();
				//player2Choose ();
				trigger1 = 0;
				trigger2 = 1;
				trigger3 = 0;
			} else {
				//prePlayedDeck.Clear ();
				foreach (int i in prePlayer2) {
					player2.Remove (i);
					hasPlayed.Add (i);
					//prePlayedDeck.Push (i);
				}
				prePlayer2.Clear ();
				//player3Choose ();
				trigger1 = 0;
				trigger2 = 0;
				trigger3 = 1;
			}
		} else {
			theText.text = "You need to choose some cards to play !";
		}
	}

	public void play3PassButton(){
		prePlayer3.Clear ();
		trigger1 = 1;
		trigger2 = 0;
		trigger3 = 0;
		passTrigger++;
		if (passTrigger == 2) {
			passTrigger = 0;
			tempPlay.Clear ();
			tempPri = -1;
			tempType = -1;
		}
	}

	int PlayCards(List<int> a){
		if (a.Count == 1) {
			if (tempPlay.Count == 0) {
				a.ForEach (i => tempPlay.Add (i));
				tempPri = a [0] / 4;
				tempType = 1;
				return 1;
			} else if (tempType == 1 && a [0] / 4 > tempPri) {
				tempPlay.Clear ();
				a.ForEach (i => tempPlay.Add (i));
				tempPri = a [0] / 4;
				tempType = 1;
				return 1;
			} else {
				return -1;
			}
		} 

		else if (a.Count == 2) {
			if (a [0] / 4 == a [1] / 4 && a [0] / 4 != 13) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 2;
					return 2;
				} else if (tempType == 2 && a [0] / 4 > tempPri) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 2;
					return 2;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [0] / 4 == 13) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 10;
					return 10;
			}

			else {
				return -1;
			}
		} 

		else if (a.Count == 3) {
			if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else if (tempType == 3 && a [0] / 4 > tempPri) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else {
					return -1;
				}
			} else {
				return -1;
			}	
		}

		else if(a.Count == 4){
			if (a [0] / 4 != a [1] / 4 && a [1] / 4 == a [2] / 4 && a [2] / 4 == a [3] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [1] / 4;
					tempType = 3;
					return 3;
				} else if (tempType == 3 && a [1] / 4 > tempPri && tempPlay.Count == 4) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [1] / 4;
					tempType = 3;
					return 3;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [2] / 4 != a [3] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else if (tempPri == 3 && a [0] / 4 > tempPri && tempPlay.Count == 4) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [2] / 4 == a [3] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 9;
					return 9;
				} else if (tempPri == 9 && a [0] / 4 > tempPri) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 9;
					return 9;
				} else if (tempPri != 9) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 9;
					return 9;
				} else {
					return -1;
				}
			} else {
				return -1;
			}
		}

		else if(a.Count == 5){
			if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [3] / 4 == a [4] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else if (tempType == 3 && a [0] / 4 > tempPri && tempPlay.Count == 5) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 3;
					return 3;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [2] / 4 == a [3] / 4 && a [3] / 4 == a [4] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 3;
					return 3;
				} else if (tempType == 3 && a [2] / 4 > tempPri && tempPlay.Count == 5) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 3;
					return 3;
				} else {
					return -1;
				}
			} else if (a [0] / 4 + 1 == a [1] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [2] / 4 + 1 == a [3] / 4 && a [3] / 4 + 1 == a [4] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else if (tempType == 4 && a [0] / 4 > tempPri && tempPlay.Count == 5) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else {
					return -1;
				}
			} else {
				return -1;
			}
		}
			
		else if(a.Count == 6){
			if (a [0] / 4 + 1 == a [1] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [2] / 4 + 1 == a [3] / 4 && a [3] / 4 + 1 == a [4] / 4 && a [4] + 1 == a [5]) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else if (tempType == 4 && a [0] / 4 > tempPri && tempPlay.Count == 6) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [2] / 4 == a [3] / 4 && a [4] / 4 == a [5] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [3] / 4 + 1 == a [4] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 5;
					return 5;
				} else if (tempType == 5 && a [0] / 4 > tempPri && tempPlay.Count == 6) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 5;
					return 5;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [2] / 4 == a [3] / 4 && a [4] / 4 == a [5] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 6;
					return 6;
				} else if (tempType == 6 && a [0] / 4 > tempPri && tempPlay.Count == 6) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 6;
					return 6;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [2] / 4 == a [3] / 4 && a [3] / 4 == a [4] / 4 && a [4] / 4 == a [5] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 6;
					return 6;
				} else if (tempType == 6 && a [2] / 4 > tempPri && tempPlay.Count == 6) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 6;
					return 6;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [3] / 4 == a [4] / 4 && a [4] / 4 == a [5] / 4 && a [2] / 4 + 1 == a [3] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 7;
					return 7;
				} else if (tempType == 7 && a [0] / 4 > tempPri && tempPlay.Count == 6) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 7;
					return 7;
				} else {
					return -1;
				}
			} else {
				return -1;
			}
		}

		else if(a.Count == 7){
			if (a [0] / 4 + 1 == a [1] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [2] / 4 + 1 == a [3] / 4 && a [3] / 4 + 1 == a [4] / 4 && a [4] + 1 == a [5] && a [5] + 1 == a [6]) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else if (tempType == 4 && a [0] / 4 > tempPri && tempPlay.Count == 7) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else {
					return -1;
				}
			} else {
				return -1;
			}
		}

		else if(a.Count == 8){
			if (a [0] / 4 + 1 == a [1] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [2] / 4 + 1 == a [3] / 4 && a [3] / 4 + 1 == a [4] / 4 && a [4] + 1 == a [5] && a [5] + 1 == a [6] && a [6] + 1 == a [7]) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else if (tempType == 4 && a [0] / 4 > tempPri && tempPlay.Count == 8) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 4;
					return 4;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [2] / 4 == a [3] / 4 && a [4] / 4 == a [5] / 4 && a [6] / 4 == a [7] / 4 && a [1] / 4 + 1 == a [2] / 4 && a [3] / 4 + 1 == a [4] / 4 && a [5] / 4 + 1 == a [6] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 5;
					return 5;
				} else if (tempType == 5 && a [0] / 4 > tempPri && tempPlay.Count == 8) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 5;
					return 5;
				} else {
					return -1;
				}
			} else if (a [0] / 4 == a [1] / 4 && a [1] / 4 == a [2] / 4 && a [3] / 4 == a [4] / 4 && a [4] / 4 == a [5] / 4 && a [2] / 4 + 1 == a [3] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 7;
					return 7;
				} else if (tempType == 7 && a [0] / 4 > tempPri && tempPlay.Count == 8) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [0] / 4;
					tempType = 7;
					return 7;
				} else {
					return -1;
				}
			} else if (a [1] / 4 == a [2] / 4 && a [2] / 4 == a [3] / 4 && a [4] / 4 == a [5] / 4 && a [5] / 4 == a [6] / 4 && a [3] / 4 + 1 == a [4] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [1] / 4;
					tempType = 7;
					return 7;
				} else if (tempType == 7 && a [1] / 4 > tempPri && tempPlay.Count == 8) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [1] / 4;
					tempType = 7;
					return 7;
				} else {
					return -1;
				}
			} else if (a [2] / 4 == a [3] / 4 && a [3] / 4 == a [4] / 4 && a [5] / 4 == a [6] / 4 && a [6] / 4 == a [7] / 4 && a [4] / 4 + 1 == a [5] / 4) {
				if (tempPlay.Count == 0) {
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 7;
					return 7;
				} else if (tempType == 7 && a [2] / 4 > tempPri && tempPlay.Count == 8) {
					tempPlay.Clear ();
					a.ForEach (i => tempPlay.Add (i));
					tempPri = a [2] / 4;
					tempType = 7;
					return 7;
				} else {
					return -1;
				}
			} else {
				return -1;
			}
		}

		else {
			return -1;
		}
	}
}
