using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class DownloadMgr
    {
        private Dictionary<string, List<Action<HttpCbBase>>> _CbDict = new Dictionary<string, List<Action<HttpCbBase>>>();


        public void DownloadTexture(string url, Action<HttpCbBase> cb)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("下载地址为null...");
                return;
            }
            if (cb == null)
            {
                Debug.LogErrorFormat("{0}:下载图片回调为null...", url);
                return;
            }

            if (_CbDict.ContainsKey(url))
            {
                _CbDict[url].Add(cb);
            }
            else
            {
                _CbDict.Add(url, new List<Action<HttpCbBase>>() { cb });
            }

            HttpSvc.instance.StartCoroutine(DownloadTexture(url));
        }

        public void DownloadImg(string url, Action<HttpCbBase> cb)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("下载地址为null...");
                return;
            }
            if (cb == null)
            {
                Debug.LogErrorFormat("{0}:下载图片回调为null...", url);
                return;
            }

            if (_CbDict.ContainsKey(url))
            {
                _CbDict[url].Add(cb);
            }
            else
            {
                _CbDict.Add(url, new List<Action<HttpCbBase>>() { cb });
            }

            HttpSvc.instance.StartCoroutine(DownloadImage(url));
        }





        private IEnumerator DownloadTexture(string url)
        {
            Action<HttpTexture2DCb> cb = (texture2D) =>
            {
                HttpTextureCb textureCb = new HttpTextureCb();
                textureCb.isError = texture2D.isError;
                textureCb.errorMsg = texture2D.errorMsg;

                textureCb.texture = texture2D.texture;

                if (_CbDict.ContainsKey(url))
                {
                    foreach (Action<HttpTextureCb> item in _CbDict[url])
                    {
                        item?.Invoke(textureCb);
                    }
                    _CbDict.Remove(url);
                }
            };
            yield return HttpSvc.instance.StartCoroutine(RealDownloadTexture2D(url, cb));
        }

        private IEnumerator DownloadImage(string url)
        {
            Action<HttpTexture2DCb> cb = (texture2D) =>
            {
                HttpSpriteCb imgCb = new HttpSpriteCb();
                imgCb.isError = texture2D.isError;
                imgCb.errorMsg = texture2D.errorMsg;

                imgCb.sprite = Sprite.Create(texture2D.texture, new Rect(0, 0, texture2D.texture.width, texture2D.texture.height), new Vector2(0.5f, 0.5f));

                if (_CbDict.ContainsKey(url))
                {
                    foreach (Action<HttpSpriteCb> item in _CbDict[url])
                    {
                        item?.Invoke(imgCb);
                    }
                    _CbDict.Remove(url);
                }
            };
            yield return HttpSvc.instance.StartCoroutine(RealDownloadTexture2D(url, cb));
        }


        /// <summary>
        /// 真正下载Texture
        /// </summary>
        private IEnumerator RealDownloadTexture2D(string url, Action<HttpTexture2DCb> cb)
        {
            UnityWebRequest request = new UnityWebRequest(url);
            DownloadHandlerTexture textureHandle = new DownloadHandlerTexture();
            request.downloadHandler = textureHandle;

            yield return request.SendWebRequest();

            HttpTexture2DCb textureCb = new HttpTexture2DCb();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                textureCb.isError = true;
                textureCb.errorMsg = request.error.ToString();
                textureCb.texture = null;
            }
            else
            {
                if (textureHandle.texture == null)
                {
                    textureCb.isError = true;
                    textureCb.errorMsg = "接受不到图片信息...";
                    textureCb.texture = null;
                }
                else
                {
                    textureCb.isError = false;
                    textureCb.errorMsg = string.Empty;
                    textureCb.texture = textureHandle.texture;
                }
            }
            cb?.Invoke(textureCb);
        }

    }
}