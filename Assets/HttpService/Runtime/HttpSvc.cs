using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class HttpSvc : HttpBase<HttpSvc>, IHttp
    {
        private Dictionary<string, Action<HttpCb>> _CallBackDict = new Dictionary<string, Action<HttpCb>>();
        private DownloadMgr _DownloadMgr = new DownloadMgr();


        /// <summary>
        /// Get请求
        /// </summary>
        public void Get(string url, Action<HttpCb> callback)
        {
            if (callback != null)
            {
                _CallBackDict.Add(url, callback);
            }
            GetUrl(url);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        public void Post(string url, Action<HttpCb> callback, string key, string json)
        {
            if (callback != null)
            {
                _CallBackDict.Add(url, callback);
            }
            PostUrl(url, key, json);
        }

        /// <summary>
        /// 下载图片 使用HttpTextureCb
        /// </summary>
        public void DownloadTexture(string url, Action<HttpCbBase> cb)
        {
            _DownloadMgr.DownloadTexture(url, cb);
        }

        /// <summary>
        /// 下载Sprite 使用HttpSpriteCb
        /// </summary>
        public void DownloadSprite(string url, Action<HttpCbBase> cb)
        {
            _DownloadMgr.DownloadImg(url, cb);
        }


        #region Get 和 Post请求
        private void GetUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                StartCoroutine(Get(request));
            }
        }

        private IEnumerator Get(UnityWebRequest request)
        {
            yield return request.SendWebRequest();
            HttpCb httpCb = new HttpCb();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                httpCb.isError = true;
                httpCb.errorMsg = request.error;
                httpCb.json = string.Empty;
                if (_CallBackDict.ContainsKey(request.url))
                {
                    _CallBackDict[request.url]?.Invoke(httpCb);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    httpCb.isError = true;
                    httpCb.errorMsg = "接收不到网络信息...";
                    httpCb.json = string.Empty;
                }
                else
                {
                    httpCb.isError = false;
                    httpCb.errorMsg = string.Empty;
                    httpCb.json = request.downloadHandler.text;
                }
                if (_CallBackDict.ContainsKey(request.url))
                {
                    _CallBackDict[request.url]?.Invoke(httpCb);
                    _CallBackDict.Remove(request.url);
                }
            }
        }

        private void PostUrl(string url, string key, string json)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WWWForm form = new WWWForm();
                form.AddField(key, json);
                UnityWebRequest request = UnityWebRequest.Post(url, form);
                StartCoroutine(Post(request));
            }
        }

        private IEnumerator Post(UnityWebRequest request)
        {
            yield return request.SendWebRequest();
            HttpCb httpCb = new HttpCb();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                httpCb.isError = true;
                httpCb.errorMsg = request.error;
                httpCb.json = string.Empty;
                if (_CallBackDict.ContainsKey(request.url))
                {
                    _CallBackDict[request.url]?.Invoke(httpCb);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(request.downloadHandler.text))
                {
                    httpCb.isError = true;
                    httpCb.errorMsg = "接收不到网络信息...";
                    httpCb.json = string.Empty;
                }
                else
                {
                    httpCb.isError = false;
                    httpCb.errorMsg = string.Empty;
                    httpCb.json = request.downloadHandler.text;
                }
                if (_CallBackDict.ContainsKey(request.url))
                {
                    _CallBackDict[request.url]?.Invoke(httpCb);
                    _CallBackDict.Remove(request.url);
                }
            }
        }
        #endregion
    }
}