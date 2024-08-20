using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjGene : MonoBehaviour
{
    // Main
    public void GeneObj()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Common
    // UI
    public void ReturnMenu()
    {
        DisableObj();
    }

    public void NextScene()
    {
        DisableObj();
        EnableNextObj();
    }

    public void PrevScene()
    {
        DisableObj();
        EnablePrevObj();
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

    [SerializeField] GameObject _prevObj;
    public void EnablePrevObj()
    {
        _prevObj.SetActive(true);
    }
}
