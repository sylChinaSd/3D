/*记录当前巡逻的状态*/
public class Patrol{
	private const int BASE_SOCRE = 100;//基础分数
	private const int MAX_PROGRESS = 8;//基础分数

	private int progress;//进度
	private int score;//巡逻得分


	public Patrol(){
		this.progress = 0;
		this.score = 0;
	}

	public int getProgress(){
		return this.progress;
	}

	public void setProgress(int p){
		this.progress = p;
	}

	public void addProgress(){
		if(this.progress<MAX_PROGRESS){
			this.progress++;
		}
	}

	public void addScore(int s){
		this.score += s;
	}

	public int getScore(){
		return this.score;
	}

	public bool isPatrolEnd(){
		return this.progress == MAX_PROGRESS;
	}
}
