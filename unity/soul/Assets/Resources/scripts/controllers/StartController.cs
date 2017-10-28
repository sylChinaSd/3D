using UnityEngine;
using System.Collections;

public class StartController : MonoBehaviour {
	private GameObject title;
	private int titleRotation;
	//
	GameObject startBtn;
	GameObject configBtn;
	GameObject exitBtn;
	private bool btnAnimEnable = true;
	private bool soulAnimEnable = true;
	//
	GameObject soulImg;
	// Use this for initialization
	void Start () {
		title = GameObject.Find ("Title");
		startBtn = GameObject.Find ("startBtn");
		startBtn.transform.position = new Vector3 (-640f,-10f,0f);
		configBtn = GameObject.Find ("configBtn");
		configBtn.transform.position = new Vector3 (-590f,-100f,0f);
		exitBtn = GameObject.Find ("exitBtn");
		exitBtn.transform.position = new Vector3 (-540f,-190f,0f);

		soulImg = GameObject.Find ("soulImg");
	}
	
	// Update is called once per frame
	void Update () {
		if(title != null){
			if(titleRotation < 36){
				title.transform.Rotate(Vector3.left*20);
				titleRotation++;
			}else{
				title.transform.localRotation.Set(0f,0f,0f,0f);
			}
		}
		if(btnAnimEnable){
			btnAnimEnable = false;
			StartCoroutine(BtnAnimation());
		}
		if(soulAnimEnable){
			soulAnimEnable = false;
			StartCoroutine(soulAnimation());
		}

	}

	//按钮动画
	IEnumerator BtnAnimation() {
		for(int i =0; i < 120;i++){
			if(i>40&&i<48){
				startBtn.transform.Translate(Vector3.right*(-10));
			}else if(i<=40){
				startBtn.transform.Translate(Vector3.right*20);
			}

			if(i>70&&i<78){
				configBtn.transform.Translate(Vector3.right*(-10));
			}else if(i>28&&i<=70){
				configBtn.transform.Translate(Vector3.right*20);
			}

			if(i>99&&i<105){
				exitBtn.transform.Translate(Vector3.right*(-10));
			}else if(i>57&&i<=99){
				exitBtn.transform.Translate(Vector3.right*20);
			}
			yield return null;
		}
	}
	//
	IEnumerator soulAnimation() {
		int max = 20,speed = 6;
		yield return new WaitForSeconds(1f);
		for(int j = 0; j < 2;j++){
			for(int i =0; i < max;i++){
				if(i < 10){
					soulImg.transform.Translate(Vector3.up*speed);
				}else{
					soulImg.transform.Translate(Vector3.up*(-speed));
				}

				yield return null;
			}
			//动画可重新开始
			if(j == 1){
				soulAnimEnable = true;				
			}
		}
	}
	public void loadGameGUI(){
		Debug.Log ("加载游戏");
		Application.LoadLevel ("main2");
	}
	public void loadCofigGUI(){
		Debug.Log ("加载设置界面");
	}
	public void exitGame(){
		Debug.Log ("退出游戏");
		Application.Quit ();
	}
}
