/*
 *  Description : UI层, 用于显示信息
 *
 *  CreatedBy : guoShuai
 *
 *  DataTime : 2018.08.06
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class UI : MonoBehaviour
{

    public Button saveBtn;

    public Button loadBtn;

    public Text showText;

    private void Awake()
    {
        Target.Instance.sd = ShowInfo; 
    }

    void Start()
    {
        showText.text = string.Empty;
        saveBtn.onClick.AddListener(OnClickSaveBtn);
        loadBtn.onClick.AddListener(OnClickLoadBtn);
    }

    /// <summary>
    /// 加载
    /// </summary>
    private void OnClickLoadBtn()
    {
        Target.Instance.LoadByJSON();
    }

    /// <summary>
    /// 保存
    /// </summary>
    private void OnClickSaveBtn()
    {
        Target.Instance.SaveByJSON();
    }

    /// <summary>
    /// 提示保存成功
    /// </summary>
    /// <param name="str"></param>
    public void ShowInfo(string str)
    {
        showText.text = str;
    }

    private void OnDestroy()
    {
        showText.text = string.Empty;
    }

}

