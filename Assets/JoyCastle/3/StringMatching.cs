using System.Collections.Generic;
using UnityEngine;

public static class StringMatching
{
    /// <summary>
    /// 检测s是否全部由wordSet集合组成
    /// </summary>
    /// <param name="s">需要检测都字符串</param>
    /// <param name="wordSet">单词合计</param>
    /// <returns></returns>
    public static bool StringMatchingWordSet(string s, List<string> wordSet)
    {
        if (string.IsNullOrEmpty(s) || wordSet == null || wordSet.Count == 0)
        {
            Debug.LogError("Input Info error!");
            return false;
        }
        return StringMatchingAllWordSet(s, wordSet, "", 0);
    }

    /// <summary>
    /// 检测s是否全部由wordSet集合组成
    /// </summary>
    /// <param name="s">需要检测都字符串</param>
    /// <param name="wordSet">单词合计</param>
    /// <param name="curWordString">当前拆分结果</param>
    /// <param name="curIndex">当前s序号索引</param>
    /// <returns></returns>
    private static bool StringMatchingAllWordSet(string s, List<string> wordSet, string curWordString, int curIndex)
    {
        if (string.IsNullOrEmpty(s) || wordSet == null || wordSet.Count == 0)
        {
            Debug.LogError("Input Info error!");
            return false;
        }
        //进行单词匹配
        for (int wordIndex = 0; wordIndex < wordSet.Count; wordIndex++)
        {
            bool suc = true;
            string curWord = wordSet[wordIndex];
            //进行单词匹配
            for (int i = 0; i < curWord.Length; i++)
            {
                if (s.Length < curIndex + curWord.Length)
                {
                    suc = false;
                    break;
                }
                else if (curWord[i] != s[i + curIndex])
                {
                    suc = false;
                    break;
                }
            }

            //匹配成功
            if (suc)
            {
                //匹配到了最后一个字母
                if (s.Length == curIndex + wordSet[wordIndex].Length)
                {
                    curWordString = curWordString + " " + curWord;
                    Debug.Log(curWordString);
                    return true;
                    //如果需要判断给定单词集中每个单词都需要用到则走下面逻辑

                    //比较下是否当前词是否包含所有给定的单词
                    int matchingAllWordCount = 0;
                    string[] curWordSet = curWordString.Split(' ');
                    for (int i = 0; i < wordSet.Count; i++)
                    {
                        for (int j = 0; j < curWordSet.Length; j++)
                        {
                            if (wordSet[i] == curWordSet[j])
                            {
                                matchingAllWordCount++;
                                break;
                            }
                        }
                    }

                    if (matchingAllWordCount == wordSet.Count)
                    {
                        Debug.Log(curWordString);
                        return true;
                    }

                }
                //成功匹配进入下一次递归
                else if (StringMatchingAllWordSet(s, wordSet, curWordString + ' ' + curWord, curIndex + curWord.Length))
                {
                    return true;
                }
            }
        }
        return false;
    }
}