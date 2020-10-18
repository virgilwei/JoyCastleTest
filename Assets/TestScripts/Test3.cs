using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private string s;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private List<string> wordSet;

    /// <summary>
    /// 
    /// </summary>
    public void ClickBotton()
    {
        bool suc = StringMatching.StringMatchingWordSet(s, wordSet);
        Debug.Log("匹配结果:" + suc);
    }
}
