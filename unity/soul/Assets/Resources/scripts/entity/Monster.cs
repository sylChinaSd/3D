using System;

public class Monster:IMonster {
	public Monster(){
		this.nickname = "虚";
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

	//复制当前对象
	public Monster copy(){
		Monster p = new Monster ();
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

	public void reset(){
		this.power = 5;
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
		return this.power == GlobalV.MAX_POWER;
	}

	public void useSkill(){
		if(skillEnable(0)){
			this.power = 0;
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
