/**
 * 宠物类
 * 
 **/
using System;

[System.Serializable]
public class Pet:IMonster {
	//喂养的基础经验
	private const int FEED_EXP = 7;
	private const int GROW_SEED = 5;

	//宠物技能序号
	public static int PET_SKILL_NONE = 0;
	public static int PET_SKILL_1 = 1;
	public static int PET_SKILL_2 = PET_SKILL_1+1;

	public Pet(){
		this.nickname = "魂";
		this.level = 1;
		this.exp = 0;
		this.atk = 10;
		this.hp = 35;
		this.def = 4;
		this.charm = 0;
		this.strength = 0;
		this.power = 0;
		this.speed = 4;
		this.mana = 3;

		this.hp1 = this.hp;
		this.power1 = this.power;

		this.petSkillIndex = PET_SKILL_NONE;
	}

	public string nickname { get; set; }//昵称
	public int level { get; set; }//等级
	public int exp { get; set; }//经验
	public int atk { get; set; }//攻击力
	public int mana { get; set; }//灵力
	public int speed { get; set; }//速度
	public int hp { get; set; }//生命
	public int charm { get; set; }//魅力
	public int def { get; set; }//防御
	public int strength { get; set; }//体力
	public int power { get; set; }//怒气

	public int hp1;//记录原值，方便比对
	public int power1;
	public int petSkillIndex = 0;//宠物技能序号

	//获取当前等级需要的经验
	public int getLevelExp(){
		return this.level * this.level * 5 + 50;
	}

	//复制当前对象
	public Pet copy(){
		Pet p = new Pet ();
		p.nickname = this.nickname;
		p.level = this.level;
		p.exp = this.exp;
		p.atk = this.atk;
		p.mana = this.mana;
		p.hp = this.hp;
		p.def = this.def;
		p.charm = this.charm;
		p.strength = this.strength;
		p.speed = this.speed;
		p.power = this.power;
		p.power1 = this.power;
		p.hp1 = this.hp;
		return p;
	}

	//喂养
	public string feed(){
		string str = null;
		Random r = new Random();
		int exp = this.exp + r.Next(FEED_EXP)+1;
		//可升级
		if (exp >= this.getLevelExp ()) {
			Pet p = this.copy();
			this.exp = exp - this.getLevelExp();
			this.level++;
			//随机提升其他属性
			bool b = false;
			if(r.Next(GROW_SEED) == 2){
				this.atk+=2;
				b = true;
			}
			if(r.Next(GROW_SEED) == 2){
				this.mana+=1;
				b = true;
			}
			if(r.Next(GROW_SEED) == 3){
				this.hp+=2;
				b = true;
			}
			if(r.Next(GROW_SEED) == 4){
				this.def++;
				b = true;
			}
			if(r.Next(GROW_SEED) == 4){
				this.speed+=1;
				b = true;
			}
			if(r.Next(GROW_SEED) == 0){
				this.charm++;
				b = true;
			}
			//升级的保底奖励
			if(!b){
				this.atk++;
				this.hp++;
			}
			//生成升级对比信息
			str+=string.Format("等级:{0:D}=>{1:D} {2}\r\n",p.level,this.level,p.level<this.level?"up":"");
			str+=string.Format("攻击:{0:D}=>{1:D} {2}\r\n",p.atk,this.atk,p.atk<this.atk?"up":"");
			str+=string.Format("灵力:{0:D}=>{1:D} {2}\r\n",p.mana,this.mana,p.mana<this.mana?"up":"");
			str+=string.Format("生命:{0:D}=>{1:D} {2}\r\n",p.hp,this.hp,p.hp<this.hp?"up":"");
			str+=string.Format("防御:{0:D}=>{1:D} {2}\r\n",p.def,this.def,p.def<this.def?"up":"");
			str+=string.Format("速度:{0:D}=>{1:D} {2}\r\n",p.speed,this.speed,p.speed<this.speed?"up":"");
			str+=string.Format("魅力:{0:D}=>{1:D} {2}",p.charm,this.charm,p.charm<this.charm?"up":"");
		} else {
			this.exp = exp;
		}
		return str;
	}

	public void reset(){
		this.power = 3;
		this.power1 = GlobalV.MAX_POWER;
		this.hp1 = this.hp;
	}

	public bool isDead(){
		return this.hp == 0;
	}

	public void addPower(){
		if(this.power+1<=GlobalV.MAX_POWER){
			this.power++;
		}
	}

	public bool skillEnable(int index){
		//return this.power == GlobalV.MAX_POWER;
		bool b = false;
		if(index == PET_SKILL_1 && petSkillIndex == PET_SKILL_1){
			b = true;
		}else if(index == PET_SKILL_2 && petSkillIndex == PET_SKILL_2){
			b = this.power==GlobalV.MAX_POWER;
		}
		return b;
	}

	public void useSkill(){
		if(petSkillIndex == PET_SKILL_1){
			petSkillIndex = PET_SKILL_NONE;
		}else if(petSkillIndex == PET_SKILL_2){
			this.power = 0;	
			petSkillIndex = PET_SKILL_NONE;
		}
	}

	public void damage(int damage){
		if(damage<=this.def){
			this.hp -= 1;
		}else{
			this.hp -= (damage - this.def);
		}
		if(this.hp < 0){
			this.hp = 0;
		}
	}
}
