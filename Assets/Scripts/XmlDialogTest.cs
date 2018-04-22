using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class XmlDialogTest : MonoBehaviour {
	//gameObjects in scence
	private GameObject roleCici,roleHarley,roleName,detail;
	private List<string> dialogList = new List<string> ();//存放对话
	private int dialogCount = 0;//对话条数

	void Start () {
		//获取各物体
		roleCici=GameObject.Find("RoleCici");
		roleHarley = GameObject.Find ("RoleHarley");
		roleName = GameObject.Find ("RoleName");
		detail = GameObject.Find ("Detail");
		roleCici.gameObject.SetActive (false);
		roleHarley.gameObject.SetActive (false);

		ReadXml ();
		LoadDialog (0);

	}
	//读取xml文件
	void ReadXml()
	{
		XmlDocument xmlDialog = new XmlDocument ();//新建一个xml编辑器
		//载入xml文件
		string dialogString=Resources.Load("DialogXML").ToString();
		xmlDialog.LoadXml (dialogString);

		XmlNode xmlNode = xmlDialog.SelectSingleNode ("dialogues");//选择“dialogues”根结点
		XmlNodeList xmlNodeList = xmlNode.ChildNodes;//获取dialogues下的所有子节点dsialogue

//		XmlNodeList xmlNodeList1 = xmlNode.SelectNodes ("dialogue");//获取名为“dialogue”的节点
//		Debug.Log(xmlNodeList2.Count);
		foreach (XmlNode item in xmlNodeList) {//遍历所有dialogue节点
			XmlElement xmlElement = (XmlElement)item;//此处强转可能会报错，报错的原因是xml文件内容有问题，格式化一下xml文件就可以了
			dialogList.Add (xmlElement.ChildNodes.Item (0).InnerText + "=" + xmlElement.ChildNodes.Item (1).InnerText);//将角色名和对话内容加入dialogList，并使用逗号分割
		}
		dialogCount = dialogList.Count;
	
	}
	private int index=0;
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			index++;

			if (index==dialogCount) {
				index = 0;
			}
			LoadDialog (index);
		} 
	}

	//加载对话并显示
	void LoadDialog(int index)
	{
		string[] arrayDialog=dialogList[index].Split('=');
		switch (arrayDialog[0]) {
		case "Cici":
			roleCici.gameObject.SetActive (true);
			roleHarley.gameObject.SetActive (false);
			roleCici.GetComponent<Image> ().sprite = Resources.Load<Sprite>("Textures/cici");
			roleName.GetComponent<Text>().text="Cici:";
			break;
		case "Harley":
			roleCici.gameObject.SetActive (false);
			roleHarley.gameObject.SetActive (true);
			roleHarley.GetComponent<Image> ().sprite = Resources.Load<Sprite>("Textures/harley");
			roleName.GetComponent<Text>().text="Harley:";
			break;
		}
		detail.GetComponent<Text> ().text = arrayDialog [1];
	}
}