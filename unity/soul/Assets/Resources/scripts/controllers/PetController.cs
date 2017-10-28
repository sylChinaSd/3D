using UnityEngine;
using System.Collections;

public class PetController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//Debug.Log("Current event detected: " + Event.current.type);
		if(Event.current.type == EventType.mouseDrag){
			Event e = Event.current;
			//Debug.Log(e.delta.x);
			if(e.delta.x >= 0){//逆时针旋转
				this.gameObject.transform.Rotate(Vector3.back * Time.deltaTime*180);
			}else{//顺时针旋转
				this.gameObject.transform.Rotate(Vector3.forward * Time.deltaTime*180);
			}
		}
	}

	public void onBeginDrag(){
		Debug.Log ("开始拖拽");
	}
	public void onEndDrag(){
		Debug.Log ("结束拖拽");
	}
}
