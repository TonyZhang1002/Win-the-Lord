using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStack))]
public class View : MonoBehaviour {

	CardStack d;
	Dictionary<int,GameObject> fetchedCards;
	int lastCount;

	public Vector3 start;
	public float cardOffset;
	public bool faceUp = false;
	public bool reverseLayerOrder=false;
	public GameObject cardPrefab;

	void Start(){
		fetchedCards = new Dictionary<int,GameObject> ();
		d = GetComponent <CardStack>();
		ShowCards ();
		lastCount = d.CardCount;
		d.CardRemoved += d_CardRemoved;

	}

	void d_CardRemoved(object sender, CardRemovedEventArgs e){
		if (fetchedCards.ContainsKey (e.CardIndex)) {
			Destroy (fetchedCards [e.CardIndex]);
			fetchedCards.Remove (e.CardIndex);
		}
	}

	void Update(){
		if (lastCount != d.CardCount) {
			lastCount = d.CardCount;
			ShowCards ();
		}
	}

	void ShowCards(){
		int cardCount = 0;

		if (d.HasCards) {
			foreach (int i in d.GetCards()) {
				float co = cardOffset * cardCount;
				Vector3 temp = start + new Vector3 (co, 0f);
				AddCard (temp, i, cardCount);
				cardCount++;
				}
			}
		}

	void AddCard(Vector3 position,int cardIndex,int positionalIndex){
		if (fetchedCards.ContainsKey (cardIndex)) {
			return;
		}

		GameObject cardCopy = (GameObject)Instantiate (cardPrefab);
		cardCopy.transform.position = position;

		cardModel cm = cardCopy.GetComponent<cardModel> ();
		cm.cardIndex = cardIndex;
		cm.ToggleFace (faceUp);

		SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer> ();
		if (reverseLayerOrder) {
			spriteRenderer.sortingOrder = 53 - positionalIndex;
		} else {
			spriteRenderer.sortingOrder = positionalIndex;
		}

		fetchedCards.Add (cardIndex, cardCopy);

		//Debug.Log ("Hand Value = " + d.HandValue());
		//Debug.Log ("Hand Color = " + d.HandColor());
	}
}