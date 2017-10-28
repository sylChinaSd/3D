using UnityEngine;
using System.Collections;

public class SoulController : MonoBehaviour {

	private Vector3 p1;
	private Vector3 p2;

	private bool isMoving = false;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		Event e = Event.current;
		if (e.button == 0 && e.isMouse&&!isMoving) {
			Vector3 mPos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(mPos);
			RaycastHit rayHit;
			if(Physics.Raycast(ray,out rayHit)){
				//Debug.Log(rayHit.collider.gameObject.name);
				if(rayHit.collider.gameObject.name == "Prefab_soil"){
					p2 = rayHit.point;
					//Debug.Log(p2);
					StartCoroutine("anim");
				}
			}
		}
	}

	IEnumerator anim(){
		if (isMoving) {
		} else {
			isMoving = true;
			animator.SetInteger("param",1);
			yield return new WaitForSeconds (1f);
			int factor = 1;
			p1 = this.gameObject.transform.position;
			if(p1.x > p2.x){
				factor = -1;
			}
			//转向
			this.gameObject.transform.Rotate(-Vector3.up*60*factor);
			//移动
			for(int i = 0;i < 30;i++){
				this.gameObject.transform.Translate(Vector3.right*factor,Space.World);
				yield return new WaitForSeconds (.1f);
			}
			//转向
			this.gameObject.transform.Rotate(Vector3.up*60*factor);

			isMoving = false;
			animator.SetInteger("param",0);
		}
	}
}
