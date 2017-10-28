using System.Collections.Generic;

public class Sys {
	private Config config;//设置
	private Pet pet;//宠物
	private List<Item> items;//道具

	public Config getConfig(){
		return config;
	}
	public void setConfig(Config config){
		this.config = config;
	}
	public Pet getPet(){
		return pet;
	}
	public void setPet(Pet pet){
		this.pet = pet;
	}
	public List<Item> getItems(){
		return items;
	}
	public void setItems(List<Item> items){
		this.items = items;
	}

}
