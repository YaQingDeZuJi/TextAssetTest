using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class CsvTest : MonoBehaviour {

	TextAsset textAsset;
	List<List<string>> objectCollection;
	int max_column=0;//二维列表中最大列数
	void Start () {
		ReadData ();
		Create ();
	}

	void ReadData()
	{
		textAsset = Resources.Load<TextAsset> ("CSVConfig");//载入文件

		string[] rowStr=textAsset.text.Trim().Split('\n');//清除文件前前后后的换行、空格之类的，并按换行符分割行
		Debug.Log(rowStr.Length);
		objectCollection=new List<List<string>>();//设置一个容器

		for (int i = 0; i < rowStr.Length; i++) {//读取每一行数据
			List<string> row=new List<string>(rowStr[i].Split(','));//按逗号分割每一个单元格，存入List（数组转List）
			//List<string> row=rowStr[i].Split(',').ToList<string>();//将数组转List，此方法需要添加System.Linq命名空间

			/*
			 * //List转数组
			string[] str=row.ToArray();
			for (int j = 0; j < str.Length; j++) {
				print (str[j]);
			}
			*/

			if (row.Count>max_column) {
				max_column = row.Count;
			}
			objectCollection.Add (row);
		}
	}

	void Create()
	{
		//生成Plane，保证所有gameObject在平面内
		GameObject plane=GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.position = Vector3.zero;
//		plane.transform.localScale = Vector3.one * 0.5f;
//		print(plane.GetComponent<MeshRenderer> ().bounds.size.x);//真实反应有MeshRenderer这个组件的模型的尺寸，受scale影响
		float planeOriginalX = plane.GetComponent<MeshFilter> ().mesh.bounds.size.x;//原始mesh尺寸，不受scale影响，要获取实际尺寸需要乘以localScale.x
		float planeOriginalZ = plane.GetComponent<MeshFilter> ().mesh.bounds.size.z;
		print (planeOriginalX+"---"+planeOriginalZ);

		//缩放plane，使plane大小与二维表匹配
		plane.transform.localScale=new Vector3(objectCollection.Count/planeOriginalX,1,max_column/planeOriginalZ);
		for (int i = 0; i < objectCollection.Count; i++) {
			for (int j = 0; j < objectCollection[i].Count; j++) {
				int objectCount = int.Parse (objectCollection [i] [j]);
				for (int k = 0; k < objectCount; k++) {
					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Cube);
					sphere.transform.position = new Vector3 (-max_column/2+j,0.5f+k,objectCollection.Count/2-i);
				}
			}
		}

	}

}
