using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        DisableObj();
        EnableNextObj();
    }

    public void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    public void EnableObj()
    {
        gameObject.SetActive(true);
    }

    public void DisableObj()
    {
        gameObject.SetActive(false);
    }

    [SerializeField] GameObject _nextObj;
    public void EnableNextObj()
    {
        _nextObj.SetActive(true);
    }
}
