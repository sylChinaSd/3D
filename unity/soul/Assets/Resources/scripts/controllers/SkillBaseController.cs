using UnityEngine;
using System.Collections;

public class SkillBaseController : MonoBehaviour {
	protected bool skillAnimEnable = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	//技能动画
	protected IEnumerator skillAnim(){
		//StopCoroutine (cameraAnim ());
		yield return null;
	}
	//屏幕震动
	protected IEnumerator cameraAnim(int max){
		float t = 0.001f,factor = 2.0f;
		Camera c = Camera.main;
		Vector3 v3 = Vector3.one;
		System.Random ran = new System.Random ();
		int count = 0;
		while(count++<max){
			//Debug.Log(count);
			if(ran.NextDouble()>0.36){
				for(int i = 0;i < 40;i++){
					if(i< 10){
						v3 = Vector3.up;
					}else if(i < 30){
						v3 = Vector3.down;
					}else{
						v3 = Vector3.up;
					}
					v3 = v3*factor;
					CommonUtil.shockScreen(c,v3);
					yield return new WaitForSeconds(t);
				}
			}else{
				for(int i = 0;i < 40;i++){
					if(i< 10){
						v3 = Vector3.right;
					}else if(i < 30){
						v3 = Vector3.left;
					}else{
						v3 = Vector3.right;
					}
					v3 = v3*factor;
					CommonUtil.shockScreen(c,v3);
					yield return new WaitForSeconds(t);
				}
			}
		}
	}
}
