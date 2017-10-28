using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MainController : MonoBehaviour {
	//照相机
	//
	private Text petLevel;
	private Text petAttr;
	private GameObject petObj;

	//对话框的属性
	private Text msgInfo;
	private GameObject msgPanel;
	private ParticleSystem msgPs;
	//喂养特效
	private ParticleSystem feedPs;

	private Sys sys;
	//Exp经验条
	private ProgressController pgController;

	private bool feedBAnimEnable = true;

	// Use this for initialization
	void Start () {
		/**/
		Sys s = new Sys ();
		s.setPet (new Pet());
		CommonUtil.save (s);

		sys = CommonUtil.load ();
		//宠物初始化
		//petObj = Instantiate (Resources.Load("prefabs/prefab_pet") as GameObject);
		petObj = Instantiate (Resources.Load("Blender/soul/soul") as GameObject);
		petObj.transform.Rotate(Vector3.forward*180);
		petObj.transform.Translate(new Vector3(200,0,0));
		petObj.AddComponent<PetController> ();
		//宠物装扮
		GameObject tmp = null;
		//宠物等级
		tmp = GameObject.Find ("txt_level");
		petLevel = (Text)tmp.GetComponent ("Text");
		petLevel.text = getLevelDesc(sys);
		//宠物属性
		tmp = GameObject.Find ("txt_attr");
		petAttr = (Text)tmp.GetComponent ("Text");
		petAttr.text = getAttrDesc(sys);


		msgPanel = GameObject.Find ("Panel_msgbox");
		tmp = GameObject.Find ("Text_levelup");
		msgInfo = (Text)tmp.GetComponent ("Text");
		tmp = GameObject.Find ("Ps_msgbox");
		msgPs = tmp.GetComponent<ParticleSystem>();
		//Debug.Log (txtLevel.text);
		pgController = GameObject.Find ("Col_Exp").GetComponent<ProgressController>();
		pgController.maxValue = sys.getPet ().getLevelExp ();
		//
		feedPs = GameObject.Find ("Effect_Feed").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		updatePetAttr ();
		if(feedBAnimEnable){
			feedBAnimEnable = false;
			StartCoroutine (feedBtnAnim());
		}
	}

	//
	void OnGUI(){		
	}

	//更新宠物文字描述
	private void updatePetAttr(){
		petLevel.text = getLevelDesc(sys);
		petAttr.text = getAttrDesc(sys);
	}

	//获取属性描述
	private string getAttrDesc(Sys sys){
		string str = "攻击:{0:D}\r\n灵力:{1:D}\r\n防御:{2:D}\r\n生命:{3:D}\r\n速度:{4:D}\r\n魅力:{5:D}";
		if (sys != null && sys.getPet () != null) {
			Pet pet = sys.getPet ();
			str = string.Format (str, pet.atk,pet.mana, pet.def, pet.hp,pet.speed, pet.charm);
		} else {
			str = string.Format(str,0,0,0,0);
		}
		return str;
	}
	//获取等级描述
	private string getLevelDesc(Sys sys){
		string str = "等级{0:D}({1:D}/{2:D})";
		if (sys != null && sys.getPet () != null) {
			Pet pet = sys.getPet ();
			str = string.Format (str, pet.level, pet.exp, pet.getLevelExp());
		} else {
			str = string.Format(str,0,0,0);
		}
		return str;
	}

	//打开信息框
	public void openMsgBox(string s){
		//Debug.Log ("打开对话框");
		//msgPanel.transform.Translate (Vector3.left*800);
		Camera.main.transform.Translate (Vector3.right*800);
		msgInfo.text = s;
		msgPs.Play ();
	}

	//关闭信息框
	public void closeMsgBox(){
		//Debug.Log ("关闭对话框");
		//msgPanel.transform.Translate (Vector3.right*800);
		Camera.main.transform.Translate (Vector3.left*800);
	}

	//ui事件处理
	public void ui_btn_item(){
		Application.LoadLevel ("item");	
	}
	public void ui_btn_patrol(){
		Application.LoadLevel ("patrol");
	}
	public void ui_btn_config(){
		Application.LoadLevel ("config");
	}
	public void ui_btn_exit(){
		Application.Quit ();
	}
	//喂食
	public void ui_btn_feed(){
		//Debug.Log ("喂食");
		//Debug.Log (feedPs.IsAlive());
		if(feedPs.isPlaying){
			feedPs.Stop();
		}
		if(feedPs.isStopped){
			feedPs.Play();
		}
		string s = sys.getPet ().feed ();
		//设置进度条的值
		pgController.curVal = sys.getPet ().exp;
		pgController.maxValue = sys.getPet ().getLevelExp ();
		CommonUtil.save (sys);
		if(s != null){
			openMsgBox(s);
		}
	}
	//喂食按钮动画
	IEnumerator feedBtnAnim(){
		GameObject feedBtn = GameObject.Find ("btn_feed");
		float f = 0.05f;
		float y = feedBtn.transform.localPosition.y;
		while(true){
			//左摇
			while(true){
				feedBtn.transform.Rotate(Vector3.forward*10);
				if(feedBtn.transform.rotation.z>0.33){
					break;
				}
				yield return new WaitForSeconds(f);
			}
			//右摇
			while(true){
				feedBtn.transform.Rotate(Vector3.forward*(-10));
				if(feedBtn.transform.rotation.z<-0.33){
					break;
				}
				yield return new WaitForSeconds(f);
			}
			//摆正
			while(true){
				feedBtn.transform.Rotate(Vector3.forward*10);
				if(System.Math.Abs(feedBtn.transform.rotation.z)<0.01){
					feedBtn.transform.rotation.Set(0f,0f,0f,0f);
					break;
				}
				yield return new WaitForSeconds(f);
			}
			for(int i =0; i < 2;i++){
				//上移
				while(true){
					//Debug.Log(feedBtn.transform.localPosition);
					if(feedBtn.transform.localPosition.y>y+10){
						break;
					}
					feedBtn.transform.Translate(Vector3.up*2);
					yield return new WaitForSeconds(f/2);
				}
				//下移
				while(true){							
					if(feedBtn.transform.localPosition.y==y){
						break;
					}
					feedBtn.transform.Translate(Vector3.down*2);	
					yield return new WaitForSeconds(f/2);
				}
			}
			yield return new WaitForSeconds(1.8f);
		}
	}


}
