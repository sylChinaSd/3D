using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour {

	public int curVal = 0;
	public int maxValue = 100;
	public Color bgColor = Color.white;
	public Color pgColor = Color.green;

	public string bgTagName = "Bg";
	public string pgTagName = "Progress";

	private RectTransform bgTran;
	private Image bgImg;
	private RectTransform pgTran;
	private Image pgImg;
	private float BG_WIDTH;
	// Use this for initialization
	void Start () {
		GameObject tmp = GameObject.Find (bgTagName);
		if(tmp!=null){
			bgTran = tmp.GetComponent<RectTransform>();
			BG_WIDTH = bgTran.rect.width;
			bgImg = tmp.GetComponent<Image>();
			bgImg.color = bgColor;
		}

		tmp = GameObject.Find (pgTagName);
		if(tmp!=null){
			pgTran = tmp.GetComponent<RectTransform>();
			pgImg = tmp.GetComponent<Image>();
			pgImg.color = pgColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float w = 0.0f;
		if (curVal >= maxValue) {
			w = BG_WIDTH;
		} else {
			w = BG_WIDTH * curVal / maxValue;
			//curVal++;
		}
		//Debug.Log (w);
		pgTran.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal,w);
		//pgTran.rect.Set(pgTran.rect.position.x,pgTran.rect.position.y,w,pgTran.rect.height);
	}
}
