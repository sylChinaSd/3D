
using UnityEngine;
using UnityEngine.UI;
//using Events.UnityEvent;
using System.Collections;

public class PatrolController : MonoBehaviour {
	private GameObject canvas;
	private GameObject canvas2;
	private GameObject warningGif;
	private GameObject patrolProgress;
	//
	private Patrol patrol = new Patrol();
	private bool canForward = true;
	//切换站相机位置
	private Vector3 canvasPosition1 = new Vector3 (1000f,65.4f,120f);
	private Vector3 progressPosition1 = new Vector3 (1000f,80f,200f);
	//战斗数据存储
	Pet pet;
	Monster monster;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
		canvas2 = GameObject.Find ("Canvas2");
		warningGif = GameObject.Find ("/Canvas2/Image");
		patrolProgress = GameObject.Find ("Patrol_progress");
		initSearch ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//前进
	public void forward(){
		//Debug.Log ("前进");
		if (canForward&&!patrol.isPatrolEnd()) {
			canForward = false;
			patrol.addProgress();
			StartCoroutine(forwardAnim());
		}
	}
	//前进动画
	IEnumerator forwardAnim(){
		int c = 5;
		float t = 0.06f,factor = 2.0f;
		for(int m = 0; m < 2;m++){
			//前上
			for(int i =0; i < c;i++){
				Vector3 v3 = (Vector3.forward+Vector3.up)*factor;
				Camera.main.transform.Translate (v3);
				canvas.transform.Translate (v3);
				patrolProgress.transform.Translate (v3);
				yield return new WaitForSeconds(t);
			}
			//前下
			for(int i =0; i < c;i++){
				Vector3 v3 = (Vector3.forward+Vector3.down)*factor;
				Camera.main.transform.Translate (v3);
				canvas.transform.Translate (v3);
				patrolProgress.transform.Translate (v3);
				yield return new WaitForSeconds(t);
			}
		}
		//点亮进度球
		lightProgress ("/Patrol_progress/Sphere_"+patrol.getProgress());
		//判断是否遇到敌人
		if(meetEnemy()){
			yield return new WaitForSeconds(0.5f);
			initBattle();
		}	
		canForward = true;
	}
	//返回主界面
	public void backMainScence(){
		balance ();
		Application.LoadLevel ("main");
	}

	/*探索完成结算*/
	private void balance(){

	}

	//点亮进度条
	private void lightProgress(string name){
		//Debug.Log (name);
		/*MeshRenderer render = GameObject.Find (name).GetComponent<MeshRenderer>();
		render.material.EnableKeyword ("_Color");
		render.material.EnableKeyword ("_EMISSION");
		render.material.EnableKeyword ("_Metallic");
		render.material.EnableKeyword ("_Glossiness");

		render.material.SetColor ("_Color",GlobalV.pgOnBase);
		render.material.SetColor ("_EmissionColor",GlobalV.pgOnEmission);
		render.material.SetFloat ("_Metallic",GlobalV.pgMetallic);
		render.material.SetFloat ("_Glossiness",GlobalV.pgSmooth);*/

		ParticleSystem ps = GameObject.Find (name).GetComponent<ParticleSystem> ();
		ps.Play ();
	}
	//是否遭遇敌人
	private bool meetEnemy(){
		System.Random r = new System.Random ();
		float f = (float)r.NextDouble();
		return f > GlobalV.MIN_MEET_ENEMY_P && f < GlobalV.MAX_MEET_ENEMY_P;
	}

	//初始化战斗场景
	private void initBattle(){
		storePostion();
		//移动搜索画面使其不再可视区域
		canvas.transform.position = new Vector3 (1000f,65.4f,-600f);
		patrolProgress.transform.position = new Vector3 (1000f,80f,-600f);

		//开始自动战斗动画
		StartCoroutine (battleAnim());
	}

	//初始化探索场景
	private void initSearch(){
		canvas.transform.position = canvasPosition1;
		patrolProgress.transform.position = progressPosition1;
	}
	//战斗动画
	IEnumerator battleAnim(){
		Debug.Log ("battleAnim");
		//添加遇敌警告
		canvas2.transform.position = Camera.main.transform.position+Vector3.forward*500;
		GifController gifC = warningGif.GetComponent<GifController>();
		gifC.start();	
		yield return new WaitForSeconds (gifC.frames*gifC.interval);
		//移出可视区域
		canvas2.transform.position = Camera.main.transform.position-Vector3.forward*500;
		//添加敌人
		GameObject enemy = GameObject.Instantiate (Resources.Load<GameObject>("Prefabs/prefab_boss"));
		enemy.transform.position = Camera.main.transform.position + Vector3.forward * 500;
		//战斗

		//战斗初始界面
		Vector3 battleUIOffset = Vector3.up * 600;
		GameObject battleUI = GameObject.Instantiate (Resources.Load ("Prefabs/prefab_battle") as GameObject);
		battleUI.transform.position = Camera.main.transform.position + Vector3.up * 70 + Vector3.forward * 135;
		//ButtonClickedEvent
		//绑定技能按钮事件
		Button btn1 = GameObject.Find (battleUI.name+"/Btn1").GetComponent<Button>();
		Button btn2 = GameObject.Find (battleUI.name+"/Btn2").GetComponent<Button>();

		UnityEngine.UI.Button.ButtonClickedEvent e1 = new UnityEngine.UI.Button.ButtonClickedEvent ();
		e1.AddListener (petSkill1);
		btn1.onClick = e1;
		UnityEngine.UI.Button.ButtonClickedEvent e2 = new UnityEngine.UI.Button.ButtonClickedEvent ();
		e2.AddListener (petSkill2);
		btn2.onClick = e2;
		//初始数据参数
		monster = new Monster ();
		pet = CommonUtil.load ().getPet ();
		monster.reset ();
		pet.reset ();
		//Debug.Log (pet);
		////获取战斗相关的对象
		RectTransform monster_fg1 = GameObject.Find (battleUI.name+"/Col_Enemy/Fg1").GetComponent<RectTransform>();
		RectTransform monster_bg1 = GameObject.Find (battleUI.name+"/Col_Enemy/Bg1").GetComponent<RectTransform>();
		RectTransform monster_fg2 = GameObject.Find (battleUI.name+"/Col_Enemy/Fg2").GetComponent<RectTransform>();

		RectTransform pet_fg = GameObject.Find (battleUI.name+"/Col_Pet/Fg").GetComponent<RectTransform>();
		RectTransform pet_bg = GameObject.Find (battleUI.name+"/Col_Pet/Bg").GetComponent<RectTransform>();
		//能量粒子系统
		ParticleSystem[] energes = GameObject.Find (battleUI.name + "/Energy").GetComponentsInChildren<ParticleSystem> ();
		//战斗结果图片
		Image resultImg = GameObject.Find (battleUI.name +"/Img_Result").GetComponent<Image>();
		//确定先手 true-pet ,false-monster
		bool hand = false;
		hand = pet.speed>=monster.speed;
		//Debug.Log (hand);
		//Debug.Log("-----"+pet.power);
		//移出可视区域
		battleUI.transform.Translate(-battleUIOffset);
		while(true){
			//设置血量、怒气的显示
			if(hand){
				//pet攻击
				//Debug.Log("__"+pet.power);
				pet.addPower();
				//Debug.Log(pet.power);
				refreshEnergy(energes,pet.power);
				refreshSkillBtn(btn1,btn2,hand);
				//移入可视区域
				battleUI.transform.Translate(battleUIOffset);
				yield return new WaitForSeconds(2f);
				while(true){				
					if(pet.skillEnable(Pet.PET_SKILL_2)){
						//移出可视区域
						battleUI.transform.Translate(-battleUIOffset);
						refreshSkillBtn(btn1,btn2,false);
						pet.useSkill();
						refreshEnergy(energes,pet.power);
						monster.damage(pet.mana+pet.atk);
						for(int c = 0; c < 3;c++){
							GameObject skill = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/prefab_skill_soul_1"));
							skill.name = "skill_"+c;
							skill.transform.position = Camera.main.transform.position+new Vector3(170f-140f*c,-76f+c*5f,260f);
							skill.transform.Rotate(Vector3.up*30*c);
							yield return new WaitForSeconds(0.5f);
						}
						yield return new WaitForSeconds(5f);
						break;
					}else if(pet.skillEnable(Pet.PET_SKILL_1)){
						//移出可视区域
						battleUI.transform.Translate(-battleUIOffset);
						refreshSkillBtn(btn1,btn2,false);
						pet.useSkill();
						monster.damage(pet.atk);
						GameObject skill = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/prefab_skill_soul_1"));
						skill.transform.position = Camera.main.transform.position+new Vector3(155f,-76f,260f);
						yield return new WaitForSeconds(7f);
						break;
					}
					yield return new WaitForSeconds(0.1f);
				}
			}else{
				refreshSkillBtn(btn1,btn2,hand);
				//增加怒气
				monster.addPower();
				//移入可视区域
				battleUI.transform.Translate(battleUIOffset);
				yield return new WaitForSeconds(2f);
				//monster攻击
				if(monster.skillEnable(-1)){
					battleUI.transform.Translate(-battleUIOffset);
					monster.useSkill();
					GameObject skill = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/prefab_skill_enemy"));
					skill.transform.position = Camera.main.transform.position+Vector3.forward*430;
					pet.damage(monster.mana+monster.atk);
					yield return new WaitForSeconds(9f);
					battleUI.transform.Translate(battleUIOffset);
				}else{
					pet.damage(monster.atk);
				}
				//移出可视区域
				battleUI.transform.Translate(-battleUIOffset);
			}
			hand = !hand;
			//更新血量、怒气的显示
			float w = 1f*pet.hp/pet.hp1*pet_bg.rect.width;
			pet_fg.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,w);
			w = 1f*monster.hp/monster.hp1*monster_bg1.rect.width;
			monster_fg1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,w);
			w = 1f*monster.power/monster.power1*monster_bg1.rect.width;
			monster_fg2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,w);
			yield return new WaitForSeconds(2f);
			//判断战斗是否结束
			if(pet.isDead()){
				//移入可视区域
				battleUI.transform.Translate(battleUIOffset);
				patrol.addScore(10);
				resultImg.color = new Color(1f,1f,1f,1f);
				resultImg.sprite = Resources.Load<Sprite>("images/battle/failed");
				break;
			}else if(monster.isDead()){
				//移入可视区域
				battleUI.transform.Translate(battleUIOffset);
				patrol.addScore(100);
				resultImg.color = new Color(1f,1f,1f,1f);
				resultImg.sprite = Resources.Load<Sprite>("images/battle/victory");
				break;
			}
		}
		yield return new WaitForSeconds(5f);
		GameObject.DestroyImmediate (enemy);
		GameObject.DestroyImmediate (battleUI);
		initSearch ();
	}
	private void refreshEnergy(ParticleSystem[] energies,int c){
		Debug.Log (c);
		for(int i = 0; i < energies.Length;i++){
			if(i < c){
				if(!energies[i].isPlaying){
					energies[i].Play();
				}
			}else{
				if(!energies[i].isStopped){
					energies[i].Stop();
				}
			}
		}

	}

	private void refreshSkillBtn(Button skill1,Button skill2,bool hand){
		if (hand) {
			skill1.enabled = true;
			skill2.enabled = pet.power == GlobalV.MAX_POWER;
		} else {
			skill1.enabled = false;
			skill2.enabled = false;
		}

	}
	//存储照相机的位置
	private void storePostion(){
		//cameraPosition1 = c.transform.position;
		canvasPosition1 = canvas.transform.position;
		progressPosition1 = patrolProgress.transform.position;
	}

	//宠物技能1
	public void petSkill1(){
		Debug.Log ("petSkill1");
		pet.petSkillIndex = Pet.PET_SKILL_1;
	}
	//宠物技能2
	public void petSkill2(){
		Debug.Log ("petSkill2");
		pet.petSkillIndex = Pet.PET_SKILL_2;
	}
}
