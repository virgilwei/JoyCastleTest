using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    private bool pingpong;
    [SerializeField]
    private GameObject o;
    [SerializeField]
    private Vector3 begin;
    [SerializeField]
    private Vector3 end;
    [SerializeField]
    private float time;
    [SerializeField]
    private MoveType moveType;
    /// <summary>
    /// 点击按钮
    /// </summary>
    public void ClickBotton()
    {
        MoveTool.Move(o, begin, end, time, pingpong, moveType);
    }
}
