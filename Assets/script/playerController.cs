using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {
	public float speed; // biar keliatan di opsinya
	private Rigidbody2D rb2D; // biar gak keliatan tapi sebenarnya ada 

	private int count;
	public Text CountText;
	public Text WinText;

	// Use this for initialization
	void Start () {
		// Debug.Log("1. start =>"+count,gameObject);

		rb2D = GetComponent<Rigidbody2D> ();

		Debug.Log("1.5 start =>"+rb2D,gameObject);
		count = 0;
		SetCountText();
		WinText.text = "";
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Debug.Log("3. fixed update =>"+count,gameObject);
		
		float moveHorizontal = Input.GetAxis ("Horizontal"); // ambil koordinat x 
		float moveVertical = Input.GetAxis ("Vertical"); // ambil koordinat y
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); // player speed 
		rb2D.AddForce(movement * speed);
	}

	// tambahan ===================================================
	void OnTriggerEnter2D(Collider2D other){ // nabrak pick up
		// if (other.gameObject.tag=="PickUp"){
		
		if (other.gameObject.CompareTag ("PickUp")){ // nabrak 
			Debug.Log("nabrak coin => "+other.gameObject.tag,gameObject); // cek tabrak coin
			
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}else {
			Debug.Log("nabrak dinding => "+other.gameObject.tag,gameObject); // cek tabrak dinding
			other.gameObject.SetActive (true);
		}
	}


	void SetCountText(){ // hitung score 
		// Debug.Log("2. set count text =>"+count,gameObject);

		CountText.text ="Count : " + count.ToString();
		if (count >=12) { 
			WinText.text = "YOU WIN!";
		}
	}
}
