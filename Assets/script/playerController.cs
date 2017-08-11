using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.UI.Button;

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
	
	public Text CountDownText;
	// public Panel PanelDialog;
	public Text PlusSignText;
	public Text TotalText;
	public Text ScoreText;
	public Text BonusText;
	public Text WinText;
	public Text TimeText;

	// public Button ButtonNext;
	public Button ButtonRetry;
	public CanvasGroup CanvasDialog;
	public CanvasGroup CanvasPreparation;

	public int countMax;
	private int countDown;

// initialization
	void Start () {

		// StartCountDown(4);
		// CountDownText.enabled=true;
		StartMainGame();
		// rb2D = GetComponent<Rigidbody2D> ();

	}

	IEnumerator StartCountDown(int seconds){
		int count = seconds;
		while(count>0){
			Debug.Log("hitungan ke - "+count);
			yield return new WaitForSeconds(1);
			count--;
		}
		// if(count<=0) 
		StartMainGame();
	}

	void StartMainGame(){
			Debug.Log("start cuy "); // stop game
		
		rb2D = GetComponent<Rigidbody2D> ();
			// for(countDown=countMax; countMax>0; countDown--){
			// 	CountDownText.text = countDown.toString();
			// 	// yield WaitForSeconds(1);
			// };

		coin = score = 0;
		Time.timeScale=1.0F; // stop game

			// Debug.Log("Time scale : "+Time.timeScale); // stop game
		WinText.text = "";
		Button btn = ButtonRetry.GetComponent<Button>();
		btn.onClick.AddListener(RestartGame);

	}

	public void RestartGame() {
		Application.LoadLevel(SceneManager.GetActiveScene().name);
		// Scene scene = SceneManager.GetActiveScene();
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
		// Debug.Log("Active scene is '" + scene.name + "'.");
	}

// Updating once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal"); // ambil koordinat x 
		float moveVertical = Input.GetAxis ("Vertical"); // ambil koordinat y
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical); // player speed 
		rb2D.AddForce(movement * speed);
		
		if(IsTimeUp() || IsCoinCompleted()) {
			CallDialogBox();
			Time.timeScale=0; // stop game
		}else{
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
		if(IsTimeUp()) TimeText.text ="Time's up";
		else TimeText.text =" " + Mathf.Round(timeLeft)+" s";
	}

// scoring ============
	bool IsCoinCompleted(){
		if(coin>=maxCoin) return true;
		else return false;
	} 

	void CountScore(){
		score=coin*coinCounter; // waktu masih ada 
		SetScoreText();
		// CountBonus();

		if(IsCoinCompleted() && !IsTimeUp()) {
			bonus=((int)Mathf.Round(timeLeft))*bonusCounter;
			BonusText.text= "Bonus : "+bonus;
			PlusSignText.text="___________ +";
			TotalText.text= "Total : "+(score+bonus);
		}

	}

	// void CountBonus(){
	// 	bonus=((int)Mathf.Round(timeLeft))*bonusCounter;
	// 	BonusText.text= "Bonus : "+bonus;
	// 	PlusSignText.text="___________ +";
	// 	TotalText.text= "Total : "+(score+bonus);
	// }

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

	void CallDialogBox(){
		Debug.Log("dialog box appear...");

         //enable the normal ui
	         CanvasDialog.alpha = 1;
	         CanvasDialog.interactable = true;
	         CanvasDialog.blocksRaycasts = true;
	         // ButtonNext.enable=false;

         // //disable the confirmation quit ui
         // CanvasMain.alpha = 0;
         // CanvasMain.interactable = false;
         // CanvasMain.blocksRaycasts = false;
	}

}
