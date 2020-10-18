using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTool
{
    /// <summary>
    /// mono类用于动画控制
    /// </summary>
    private static MonoBehaviour monoBehaviour;

    /// <summary>
    /// 移动字典
    /// </summary>
    private static Dictionary<GameObject, Coroutine> moveDic = new Dictionary<GameObject, Coroutine>();

    /// <summary>
    /// 1. 1)实现 move(GameObjct gameObject, Vector3 begin, Vector3 end, float time, bool pingpong){ }
    /// 使 gameObject 在 time 秒内，从 begin 移动到 end，若 pingpong 为 true，则在结束时 使 gameObject 在 time 秒内从 end 移动到 begin，如此往复。
    /// </summary>
    /// <param name="gameObject">移动的物体</param>
    /// <param name="begin">开始坐标</param>
    /// <param name="end">结束坐标</param>
    /// <param name="time">移动时间</param>
    /// <param name="pingpong">是否循环</param>
    public static void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, MoveType moveType)
    {
        //进行异常检测，防止输入的信息错误
        if (gameObject == null)
        {
            Debug.LogError("error:GameObject is null!");
            return;
        }

        if (time <= 0 && pingpong)
        {
            Debug.LogError("error:Time less than or equal to 0!");
            return;
        }

        if (time < 0)
        {
            gameObject.transform.position = end;
            Debug.LogError("error:Time less than 0!");
            return;
        }

        //空异常检测
        if (monoBehaviour == null)
        {
            GameObject o = new GameObject();
            o.name = "MoveTool";
            if (o != null)
            {
                monoBehaviour = o.AddComponent<MonoStub>();
            }

            if (monoBehaviour != null)
            {
                Object.DontDestroyOnLoad(o);
            }
            moveDic.Clear();
        }
        gameObject.transform.position = begin;
        //执行移动动画
        if (monoBehaviour != null)
        {
            Stop(gameObject);
            Coroutine coroutine = monoBehaviour.StartCoroutine(ToMoveTarget(gameObject, begin, end, time, pingpong, moveType));
            moveDic.Add(gameObject, coroutine);
        }
        else
        {
            Debug.LogError("error:Internal exception!");
        }
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Stop(GameObject gameObject)
    {
        if (monoBehaviour != null && gameObject != null && moveDic.ContainsKey(gameObject))
        {
            Coroutine stopCoroutine = moveDic[gameObject];
            if (stopCoroutine != null)
            {
                monoBehaviour.StopCoroutine(stopCoroutine);
                moveDic.Remove(gameObject);
            }
            else
            {
                moveDic.Remove(gameObject);
            }
        }
    }

    /// <summary>
    /// 移动协程
    /// </summary>
    /// <param name="gameObject">移动的物体</param>
    /// <param name="begin">开始坐标</param>
    /// <param name="end">结束坐标</param>
    /// <param name="time">移动时间</param>
    /// <param name="pingpong">是否循环</param>
    /// <param name="moveType">移动类型</param>
    /// <returns></returns>
    private static IEnumerator ToMoveTarget(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, MoveType moveType)
    {
        float moveTimer = 0;
        if (pingpong)
        {
            while (true)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer > 2f * time)
                {
                    moveTimer -= 2f * time;
                }

                if (moveTimer < time)
                {
                    gameObject.transform.position = GetMovePosition(begin, end, moveTimer, time, moveType);
                }
                else
                {
                    gameObject.transform.position = GetMovePosition(end, begin, moveTimer - time, time, moveType);
                }
                yield return null;
            }
        }
        else
        {
            while (moveTimer < time)
            {
                moveTimer += Time.deltaTime;
                gameObject.transform.position = GetMovePosition(begin, end, moveTimer, time, moveType);
                yield return null;
            }
            gameObject.transform.position = end;
        }
    }

    /// <summary>
    /// 根据移动类型获取坐标
    /// </summary>
    /// <param name="begin">开始点</param>
    /// <param name="end">结束</param>
    /// <param name="curTime">当前时间点</param>
    /// <param name="durariontime">运动时间</param>
    /// <param name="moveType">移动类型</param>
    /// <returns></returns>
    private static Vector3 GetMovePosition(Vector3 begin, Vector3 end, float curTime, float durariontime, MoveType moveType)
    {
        Vector3 targetPosition = Vector3.zero;
        switch (moveType)
        {
            case MoveType.Linear:
                {
                    targetPosition = begin + (end - begin) * curTime / durariontime;
                }
                break;
            case MoveType.EaseIn:
                {
                    float process = curTime / durariontime;
                    targetPosition = (end - begin) * process * process + begin;
                }
                break;
            case MoveType.EaseInOut:
                {
                    float process = curTime / durariontime * 2;
                    if (process < 1)
                    {
                        targetPosition = (end - begin) / 2 * process * process + begin;
                    }
                    else
                    {
                        process--;
                        targetPosition = -(end - begin) / 2 * (process * (process - 2) - 1) + begin;
                    }
                }
                break;
            case MoveType.EaseOut:
                {
                    float process = curTime / durariontime;
                    targetPosition = -(end - begin) * process * (process - 2) + begin;
                }
                break;
            default:
                break;
        }
        return targetPosition;
    }
}