using UnityEngine;

public static class ArrayTool
{
    /// <summary>
    /// 2.给定一个二维数组 int[,] a = new int[50, 100], 实现在该二维数组内的“剪切”->“粘贴” 功能。
    /// void cut(int[,] a, int beginRow, int rowCount, int beginColumn, int columnCount, int targetBeginRow, int targetBeginColumn){ }
    /// </summary>
    /// <param name="a"></param>
    /// <param name="beginRow"></param>
    /// <param name="rowCount"></param>
    /// <param name="beginColumn"></param>
    /// <param name="columnCount"></param>
    /// <param name="targetBeginRow"></param>
    /// <param name="targetBeginColumn"></param>
    public static void Cut(int[,] a, int beginRow, int rowCount, int beginColumn, int columnCount, int targetBeginRow, int targetBeginColumn)
    {
        string errInfo = string.Empty;
        //校验是否是异常数据
        if (a == null)
        {
            errInfo = "Array is null!";
        }
        else if (beginRow < 0 || beginRow >= rowCount)
        {
            errInfo = "BeginRow out of bounds!";
        }
        else if (beginColumn < 0 || beginColumn >= columnCount)
        {
            errInfo = "BeginColumn out of bounds!";
        }
        else if (targetBeginRow < 0 || targetBeginRow >= rowCount)
        {
            errInfo = "TargetBeginRow out of bounds!";
        }
        else if (targetBeginColumn < 0 || targetBeginColumn >= columnCount)
        {
            errInfo = "TargetBeginColumn out of bounds!";
        }
        else if (a.GetLength(0) != rowCount)
        {
            errInfo = "Array rowCount does not match definition";
        }
        else if (a.GetLength(1) != columnCount)
        {
            errInfo = "Array columnCount does not match definition";
        }
        else if (beginRow == targetBeginRow && beginColumn == targetBeginColumn)
        {
            errInfo = "There is no need to move!";
        }

        //没有异常
        if (string.IsNullOrEmpty(errInfo))
        {
            //方向 左移 还是 右移
            int direction = targetBeginRow * columnCount + targetBeginColumn > beginRow * columnCount + beginColumn ? 1 : -1;
            int curNumber = a[beginRow, beginColumn];

            int index = beginRow * columnCount + beginColumn;

            while (true)
            {
                Vector2Int target = GetVector2Index(index + direction, columnCount);
                Vector2Int cur = GetVector2Index(index, columnCount);
                a[cur.x, cur.y] = a[target.x, target.y];

                if (direction == 1 && index >= targetBeginRow * columnCount + targetBeginColumn)
                {
                    break;
                }
                if (direction == -1 && index <= targetBeginRow * columnCount + targetBeginColumn)
                {
                    break;
                }
                index += direction;
            }

            a[targetBeginRow, targetBeginColumn] = curNumber;

        }
        else//异常输出
        {
            Debug.LogError(errInfo);
        }
    }

    /// <summary>
    /// 根据索引号，换算坐标
    /// </summary>
    /// <param name="index">索引号</param>
    /// <param name="columnCount">每一行个数</param>
    /// <returns></returns>
    private static Vector2Int GetVector2Index(int index, int columnCount)
    {
        Vector2Int vec = Vector2Int.zero;
        vec.y = index % columnCount;
        vec.x = (index - vec.y) / columnCount;
        return vec;
    }
}