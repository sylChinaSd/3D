using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

public class CommonUtil {
	/**
	 * 加载
	 * */
	public static Sys load(){
		Sys sys = new Sys ();
		try 
		{
			String path = "./sys.dat";
			if (File.Exists(path)){			
				using (StreamReader sr = new StreamReader(path)){
					String line = null;
					//pet信息
					line = sr.ReadLine();
					if (line != null && !line.Equals("null")){
						//Debug.Log(line);
						Pet p = JsonConvert.DeserializeObject<Pet>(line);
						sys.setPet(p);
					}
					//设置信息
					line = sr.ReadLine();
					if (line != null&& !line.Equals("null")){
						Config c = JsonConvert.DeserializeObject<Config>(line);
						sys.setConfig(c);
					}
					//道具
					line = sr.ReadLine();
					if (line != null&& !line.Equals("null")){
						List<Item> items = JsonConvert.DeserializeObject<List<Item>>(line);
						sys.setItems(items);
					}
				}
			}
		}catch (Exception e){
			Console.WriteLine("The process failed: {0}", e.ToString());
		}
		return sys;
	}
	/**
	 * 保存
	 * */
	public static void save(Sys sys){
		try 
		{
			String path = "./sys.dat";
			using (StreamWriter sw = new StreamWriter(path)){
				string jsonData = JsonConvert.SerializeObject(sys.getPet());
				sw.WriteLine(jsonData);
				jsonData = JsonConvert.SerializeObject(sys.getConfig());
				sw.WriteLine(jsonData);
				jsonData = JsonConvert.SerializeObject(sys.getItems());
				sw.WriteLine(jsonData);
			}
		}catch (Exception e){
			Console.WriteLine("The process failed: {0}", e.ToString());
		}

	}

	/*移动照相机*/
	public static void shockScreen(Camera c,Vector3 v){
		c.transform.Translate (v);
	}


}
