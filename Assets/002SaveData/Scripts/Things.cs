/*
 *  Description : 构造的类 , 主要记录物体的 位置 旋转 大小 颜色等
 *
 *  CreatedBy : guoShuai
 *
 *  DataTime : 2018.08.06
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using System;

[System.Serializable]
public class Things  
{

    public Vector pos { get; set; }// 位置

    public Vector rota { get; set; } // 旋转

    public Vector scale { get; set; } // 大小

    public Vector color { get; set; } // 颜色

    public string  name { get; set; } // 名字
   
}

