using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour {

	List<int> cards;

	public bool isGameDeck;
	public bool faceUp = false;

	public bool HasCards{
		get { return cards != null && cards.Count > 0;}
	}

	public event CardRemovedEventHandler CardRemoved;

	public int CardCount{
		get{ 
			if (cards == null) {
				return 0;
			} else {
				return cards.Count;
			}
		}
	}

	public IEnumerable<int> GetCards(){
		foreach (int i in cards) {
			yield return i; 
		}
	}

	public int GetCard(int index){
		return cards[index];
		cards.RemoveAt (index);
	}

	public int Pop(){
		int temp = cards [0];
		cards.RemoveAt (0);

		if (CardRemoved != null) {
			CardRemoved (this, new CardRemovedEventArgs (temp));
		}
		return temp;
	}

	public int Remove(int p){
		int temp = p;
		cards.Remove (p);

		if (CardRemoved != null) {
			CardRemoved (this, new CardRemovedEventArgs (temp));
		}
		return temp;
	}

	public int RemoveAt(int p){
		int temp = cards [p];
		cards.RemoveAt (p);

		if (CardRemoved != null) {
			CardRemoved (this, new CardRemovedEventArgs (temp));
		}
		return temp;
	}

	public void Clear(){
		cards.Clear ();
	}

	public void Push(int card){
		cards.Add (card);
		cards.Sort ();
	}

	public int HandValue(){
		int total = 0;
		foreach (int card in GetCards()) {
			int cardRank = card % 13 + 1;
			total += cardRank;
		}

		return total;
	}

	public int HandColor(){
		int total = 0;
		foreach (int card in GetCards()) {
			int cardColor = card / 13;
			total += cardColor;
		}
		return total;
	}

	public void shuffle(){
		
		for (int i = 0; i < 54; i++) {
			cards.Add (i);
		}

		int n = cards.Count;
		while(n>1){
			n--;
			int k = Random.Range (0, n + 1);
			int temp = cards [k];
			cards [k] = cards [n];
			cards [n] = temp;
		}
	}

	/*
	public void sort(){
		int n = cards.Count;

		while(n>=1){
			int cardRank1 = cards[n] % 13 + 1;
			int cardColor1 = cards[n] / 13;
			int m = n;
			while (m >= 0) {
				int cardRank2 = cards [m] % 13 + 1;
				int cardColor2 = cards[m] / 13;
				if (cardRank1 > cardRank2) {
					int temp = cards[n];
					cards[n] = cards [m];
					cards [m] = temp;
				} else if (cardRank1 == cardRank2) {
					if (cardColor1 > cardColor2) {
						int temp = cards[n];
						cards[n] = cards [m];
						cards [m] = temp;
					}
				}
				m--;
			}
			n--;
		}
	}
	*/

	void Awake () {
		cards = new List<int> ();
		if (isGameDeck) {
			shuffle ();
		}
	}

}
