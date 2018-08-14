/*
 *  Description : 由于 LitJson 不能解析 Vector3 , Quatertion , 还有Color
 *  
 *                再构造一个类 , 记录Vector3 的三个值
 *
 *  CreatedBy : guoShuai
 *
 *  DataTime : 2018.08.06
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

[System.Serializable]
public class Vector  
{

    public string x { get; set; }
    public string y { get; set; }
    public string z { get; set; }
    public string w { get; set; }
    
}

