using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GifController : MonoBehaviour {
	public int count = 0;
	public float interval = 0.1f;
	public int frames = 32;
	public string basePath = "";
	public string prefix = "";
	
	//private bool gifEnable = true;
	private bool isStopped = true;
	private bool isPlaying = false;

	private List<Sprite> sprites;//动画序列
	private Image image;
	// Use this for initialization
	void Start () {
		sprites = new List<Sprite> ();
		for(int i = 1; i <= this.count;i++){
			string name = basePath+prefix+i;
			//Debug.Log(name);
			//name = "images/warning/warning1";
			//Debug.Log(name);
			Sprite s = Resources.Load<Sprite>(name);
			//Debug.Log(s);
			sprites.Add(s);
		}
		image = this.gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	//gif动画
	IEnumerator gif(){
		isPlaying = true;
		isStopped = false;
		int i = 0;
		while(i < frames){
			int index = i%sprites.Count;
			//Debug.Log(index);
			image.sprite = sprites[index];
			i++;
			yield return new WaitForSeconds (interval);
		}
		image.sprite = null;
		isPlaying = false;
		isStopped = true;
	}

	//停止
	public void stop(){
		if(isPlaying&&!isStopped){
			StopCoroutine(gif ());
			isPlaying = false;
			isStopped = true;
		}
	}
	//开始
	public void start(){
		if(!isPlaying&&isStopped){
			StartCoroutine(gif());
		}
	}
}
