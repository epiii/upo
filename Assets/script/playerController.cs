using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {
	public float speed; 
	private Rigidbody2D rb2D; 

	private int coin;
	private int maxCoin=10;
	private int score;
	private int coinCounter=10;
	private int bonus;
	private int bonusCounter=2;
	private float timeLeft = 15.0f;
	
	public Text PlusSignText;
	public Text TotalText;
	public Text ScoreText;
	public Text BonusText;
	public Text WinText;
	public Text TimeText;
	

// initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		coin = score = 0;
		WinText.text = "";
	}

// Updating once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal"); // ambil koordinat x 
		float moveVertical = Input.GetAxis ("Vertical"); // ambil koordinat y
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); // player speed 
		rb2D.AddForce(movement * speed);
		
		if(!IsTimeUp()) {
			CountTime();
			CountScore();
		}
	}

// timing  ============
	bool IsTimeUp(){
		if(timeLeft <= 0) return true;
		else return false;
	}

	void CountTime(){
		if(!IsCoinCompleted()) {
			timeLeft-=Time.deltaTime;
			SetCountTimeText();
		}// Debug.Log("counting down . . .");
	}

	void SetCountTimeText(){
		if(IsTimeUp()) TimeText.text ="Time is Up";
		else TimeText.text ="Time Left : " + Mathf.Round(timeLeft)+" s";
	}

// scoring ============
	bool IsCoinCompleted(){
		if(coin>=maxCoin) return true;
		else return false;
	} 

	void CountScore(){
		score=coin*coinCounter; // waktu masih ada 
		SetScoreText();
		
		if(!IsTimeUp() && IsCoinCompleted()){
			CountBonus(); // waktu habis / coin habis			
		} 
	}

	void CountBonus(){
		bonus=((int)timeLeft)*bonusCounter;
		BonusText.text= "Bonus : "+bonus;
		PlusSignText.text="___________ +";
		TotalText.text= "Total : "+(score+bonus);
	}

	void SetScoreText(){ // hitung score 
		// Debug.Log("2. set count text =>"+count,gameObject);

		ScoreText.text ="Score : " + score.ToString();
		if(IsCoinCompleted()){ // coin lengkap 
			WinText.text = "Mission Complete !!";
		} else {
			if(IsTimeUp()) WinText.text = "Not Bad,you may try again ..";
			else WinText.text = "Coin Left : "+(maxCoin-coin);
		}
	}

// Action ===================================================
	void OnTriggerEnter2D(Collider2D other){ // nabrak pick up
		// if (other.gameObject.CompareTag ("PickUp")){ // nabrak 
		if (other.gameObject.tag=="PickUp" && !IsTimeUp()){
			other.gameObject.SetActive (false);
			coin++;
			CountScore();
			// Debug.Log("nabrak coin => "+other.gameObject.tag,gameObject); // cek tabrak coin
		}
	}

// restart game  ==========================================
	// void ResetCount(){
	// 	to do 
	// }

	// void RestartGame(){
	// 	// to do 	
	// }

}
