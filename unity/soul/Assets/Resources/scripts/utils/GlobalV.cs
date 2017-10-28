using UnityEngine;
/*常用数值
 */
public class GlobalV{
	//常用颜色信息
	public static Color pgOnBase = new Color (192f/255f,57f/255f,82f/255f,1f);
	public static Color pgOnEmission = new Color (0.28f,0.73f,0.82f,1f);
	public static float pgMetallic = 0.0f;
	public static float pgSmooth = 0.78f;


	public static float MIN_MEET_ENEMY_P = 0.2f;
	public static float MAX_MEET_ENEMY_P = 0.99f;

	//最大怒气值
	public static int MAX_POWER = 5;
	//技能消耗
	public static int SKILL_EXPEND = MAX_POWER;
}
