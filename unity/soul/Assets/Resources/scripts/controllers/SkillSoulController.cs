using UnityEngine;
using System.Collections;
public class SkillSoulController : SkillBaseController {
	
	private ParticleSystem ps1,ps2,psEnd;
	// Use this for initialization
	void Start () {
		string name = "/"+this.gameObject.name+"/";
		//Debug.Log (name);
		ps1 = GameObject.Find (name+"Ps_1").GetComponent<ParticleSystem>();
		ps2 = GameObject.Find (name+"Ps_2").GetComponent<ParticleSystem>();
		psEnd = GameObject.Find (name+"Ps_end").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		_update ();
	}
	private void _update(){
		if(skillAnimEnable){
			skillAnimEnable = false;
			StartCoroutine (skillAnim());
		}
	}

	IEnumerator skillAnim(){
		float t = 0.04f,one = 1.0f;
		int flag = 30;
		ps1.Play ();
		/*System.Random r = new System.Random ();
		float one = (float)r.NextDouble ()*2;
		Debug.Log (one);*/
		yield return new WaitForSeconds (one);
		float f1 = 0f, f2 = 0f;
		f1 = (float)System.Math.Sin (System.Math.PI / 3)*10;
		f2 = (float)System.Math.Cos(System.Math.PI/3)*10;
		Vector3 speed =f1*Vector3.forward + f2*Vector3.left;
		for(int i = 0;i < flag + 80;i++){
			if(i < flag){
				this.gameObject.transform.Translate (speed);
			}else if(i == flag){
				ps2.Play ();
				psEnd.Play();
				ps1.Stop();
				//StartCoroutine (cameraAnim(2));
				//ps1.Clear();
			}
			yield return new WaitForSeconds (t);
		}
		//StopCoroutine ("cameraAnim");
		GameObject.DestroyImmediate (this.gameObject);
	}
}
