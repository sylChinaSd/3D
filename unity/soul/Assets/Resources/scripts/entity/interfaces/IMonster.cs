public interface IMonster{
	void reset();
	bool isDead();
	void addPower();
	bool skillEnable(int index);
	void useSkill ();
	void damage(int damage);
}
