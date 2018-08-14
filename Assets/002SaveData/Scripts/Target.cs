
/*
 *  Description : 主要逻辑都是在这个脚本
 *  
 *                保存信息 
 *                
 *                读取信息
 *
 *  CreatedBy : guoShuai
 *
 *  DataTime : 2018.08.06
 */
using UnityEngine;
using System.IO;
using LitJson;
using UnityEditor;
using System.Collections.Generic;

public delegate void ShowDelegate(string info);

public class Target : MonoBehaviour
{
    public static Target Instance;
    public Transform[] prefabsArr;   
    public ShowDelegate sd;

    private Dictionary<string, Transform> traDic = new Dictionary<string, Transform>();
    private void Awake()
    {
        Instance = this;
        prefabsArr = Resources.LoadAll<Transform>("Prefabs");
        for (int i = 0; i < prefabsArr.Length; i++)
        {
            traDic.Add(prefabsArr[i].name, prefabsArr[i]);
        }
    }

    /// <summary>
    /// 保存信息
    /// </summary>
    /// <returns></returns>
    public Save SaveInfo()
    {
        Save sa = new Save();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tra = transform.GetChild(i);
            
            Things th = new Things();
            // position
            Vector v = new Vector();
            v.x = tra.localPosition.x.ToString();  
            v.y = tra.localPosition.y.ToString();
            v.z = tra.localPosition.z.ToString();
            th.pos = v;

            // rotation
            Vector vr = new Vector();
            vr.x = tra.localEulerAngles.x.ToString();
            vr.y = tra.localEulerAngles.y.ToString();
            vr.z = tra.localEulerAngles.z.ToString();
            th.rota = vr;

            // scale
            Vector vs = new Vector();
            vs.x = tra.localScale.x.ToString(); 
            vs.y = tra.localScale.y.ToString();
            vs.z = tra.localScale.z.ToString();
            th.scale = vs;

            // color
            Vector vc = new Vector();
            vc.x = tra.GetComponent<MeshRenderer>().material.color.r.ToString();
            vc.y = tra.GetComponent<MeshRenderer>().material.color.g.ToString();
            vc.z = tra.GetComponent<MeshRenderer>().material.color.b.ToString();
            vc.w = tra.GetComponent<MeshRenderer>().material.color.a.ToString();
            th.color = vc;

            // name 
            th.name = tra.name;
            
            sa.intList.Add(th);           
        }

        return sa;
    }

    /// <summary>
    /// JSON保存信息
    /// </summary>
    public void SaveByJSON()
    {
        Save sa = SaveInfo();
         
        string filePath = Application.dataPath + "/Config" + "/Binary.json";
        
        string str = JsonMapper.ToJson(sa);
        if (str == null)
        {          
            sd("保存失败");
            return;
        }
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(str);
        sw.Close();
        AssetDatabase.Refresh();      
        sd("保存成功");
    }

    /// <summary>
    /// JSON读取信息
    /// </summary>
    public void LoadByJSON()
    {
 
        string filePath = Application.dataPath + "/Config" + "/Binary.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string str =  sr.ReadToEnd();
            sr.Close();

            Save sa = JsonMapper.ToObject<Save>(str);
                   
                for (int i = 0; i < transform.childCount; i++)
                {

                    Transform tra = transform.GetChild(i);
                    // position
                    Vector v = sa.intList[i].pos;
                    tra.localPosition = new Vector3(float.Parse(v.x), float.Parse(v.y), float.Parse(v.z));
                    // rotation
                    Vector vr = sa.intList[i].rota;
                    tra.localRotation = Quaternion.Euler(float.Parse(vr.x), float.Parse(vr.y), float.Parse(vr.z));
                    // scale
                    Vector vs = sa.intList[i].scale;
                    tra.localScale = new Vector3(float.Parse(vs.x), float.Parse(vs.y), float.Parse(vs.z));
                    // color
                    Vector vc = sa.intList[i].color;
                    tra.GetComponent<MeshRenderer>().material.color = new Color(float.Parse(vc.x), float.Parse(vc.y), float.Parse(vc.z), float.Parse(vc.w));
                    // name 
                    tra.name = sa.intList[i].name;

                if (transform.childCount != sa.intList.Count) // 数量不相同 , 新增了物体
                {
                    int count = transform.childCount; // 单独记录一下 childCount , 待会childCount会增加
                    int index = sa.intList.Count - count;
                    for (int j = 0; j < index; j++)
                    {
                        Things th = sa.intList[count + j];
                        string name = th.name.Split(new string[] { " (" }, System.StringSplitOptions.None)[0]; // 注意这里： 半边括号左边还有个空格
                        
                        if (traDic.ContainsKey(name))
                        {
                            Transform obj = Instantiate(traDic[name]);
                            obj.SetParent(this.transform);

                            // postion 
                            Vector vp = th.pos;
                            obj.localPosition = new Vector3(float.Parse(vp.x), float.Parse(vp.y), float.Parse(vp.z));
                            // rotation
                            Vector vrAdd = th.rota;
                            obj.localRotation = Quaternion.Euler(float.Parse(vrAdd.x), float.Parse(vrAdd.y), float.Parse(vrAdd.z));
                            // scale
                            Vector vsAdd = th.scale;
                            obj.localScale = new Vector3(float.Parse(vsAdd.x), float.Parse(vsAdd.y), float.Parse(vsAdd.z));
                            // color
                            Vector vcAdd = th.color;
                            obj.GetComponent<MeshRenderer>().material.color = new Color(float.Parse(vcAdd.x), float.Parse(vcAdd.y), float.Parse(vcAdd.z), float.Parse(vcAdd.w));
                            // name 
                            obj.name = th.name;
                        }

                    }

                }                 
            }           
        }
        else
        {
            print("不存在" + filePath);
        }
    }

   
    
}

