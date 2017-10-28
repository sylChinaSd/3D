using UnityEngine;
using System.Collections;

public class SkillEnemyController : SkillBaseController {

	//private GameObject cube;
	private ParticleSystem ps1,ps2,ps3,ps4;

	// Use this for initialization
	void Start () {
		//Debug.Log ("start");
		//cube = GameObject.Find ("Cube");
		ps1 = GameObject.Find ("PS_1").GetComponent<ParticleSystem>();
		ps2 = GameObject.Find ("PS_2").GetComponent<ParticleSystem>();
		ps3 = GameObject.Find ("PS_3").GetComponent<ParticleSystem>();
		ps4 = GameObject.Find ("PS_4").GetComponent<ParticleSystem>();
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

	//技能动画
	IEnumerator skillAnim(){
		float t = 0.1f;
		const int flag = 25;
		//Vector3 scale = Vector3.one * 1f;
		Vector3 position1 = Camera.main.transform.position;
		Vector3 position2 = position1+Vector3.right*500+Vector3.forward*200;
		Quaternion rotaion1 = Quaternion.LookRotation(Vector3.forward);	
		Quaternion rotaion2 = Quaternion.LookRotation(Vector3.left);
		for(int i = 0;i < flag+60;i++){
			if(i<flag){
				//cube.transform.Rotate (Vector3.up*24);
				//cube.transform.localScale += scale;
			}else if(i == flag){
				//GameObject.DestroyImmediate(cube);
			}else{
				ps2.emissionRate -=8;
				if(ps2.emissionRate < 40){
					//cube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
					ps3.Stop ();
					ps4.Stop();
					StopCoroutine ("cameraAnim");
				}else{
					//cube.transform.localScale -= scale;
					//cube.transform.Rotate (Vector3.down*32);
				}
			}
			switch(i){
			case 0://开始粒子1
				ps1.Play();
				break;
			case flag://结束并清空粒子1
				ps1.Stop();
				break;
			case flag+1:
				Camera.main.transform.position = position2;
				Camera.main.transform.rotation = rotaion2;
				StartCoroutine (cameraAnim(4));
				ps2.Play();
				ps3.Play();
				ps4.Play();
				break;
			default:
				break;					
			}				
			yield return new WaitForSeconds(t);
		}
		Camera.main.transform.position = position1;
		Camera.main.transform.rotation = rotaion1;

		GameObject.DestroyImmediate (this.gameObject);
	}


}
