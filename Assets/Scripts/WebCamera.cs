using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamera : MonoBehaviour
{
    WebCamDevice[] _devices;
    void Start()
    {
        _devices = WebCamTexture.devices;

        if (_devices.Length > 0){

            String tmp = "Camera:"+_devices.Length+"\n";
            for(int i=0;i < _devices.Length;i++) {
            Debug.Log(tmp + "ID:"+i+
                " Name:"+ _devices[i].name+
                " isFront:"+ _devices[i].isFrontFacing+"\n");
            }
        }
    }

    // WebCamera
    [SerializeField] private RawImage _rawImage;
    private WebCamTexture _webCamTexture;
    private static int INPUT_SIZE = 256;
    private static int FPS = 30;
    public void StartPreview()
    {
        // Webカメラの開始
        _webCamTexture = new WebCamTexture(_devices[0].name,INPUT_SIZE, INPUT_SIZE, FPS);
        _rawImage.texture = _webCamTexture;
        _webCamTexture.Play();
    }

    public void EndPreview()
    {
        this._webCamTexture.Stop();
        this._rawImage.texture = CreateTempTexture(64, 64, Color.white);
    }

    // 白色の64×64のテクスチャをAssets/に作成する
    /// <summary>
    /// 特定の色で埋めたテクスチャを取得
    /// </summary>
    private static Texture2D CreateTempTexture(int width, int height, Color defaultColor = default)
    {
        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        
        for (int y = 0; y < texture.height; y++)
            for (int x = 0; x < texture.width; x++)
                texture.SetPixel(x, y, defaultColor);
        return texture;
    }

    // Common
    // UI
    public void ReturnMenu()
    {
        EndPreview();
        DisableObj();
    }

    public void NextScene()
    {
        EndPreview();
        DisableObj();
        EnableNextObj();
    }

    public void PrevScene()
    {
        EndPreview();
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
