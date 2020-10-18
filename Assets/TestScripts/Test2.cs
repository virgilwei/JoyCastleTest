using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    private int beginRow;
    [SerializeField]
    private int beginColumn;
    [SerializeField]
    private int rowCount;
    [SerializeField]
    private int columnCount;
    [SerializeField]
    private int targetBeginRow;
    [SerializeField]
    private int targetBeginColumn;

    /// <summary>
    /// 点击按钮
    /// </summary>
    public void ClickBotton()
    {
        int[,] a = new int[rowCount, columnCount];
        int index = 0;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                a[i, j] = index++;
            }
        }

        ArrayTool.Cut(a, beginRow, rowCount, beginColumn, columnCount, targetBeginRow, targetBeginColumn);

        string info = "";
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                info += a[i, j] + "\t";
            }
            info += "\r\n";
        }
        Debug.Log(info);
    }
}
