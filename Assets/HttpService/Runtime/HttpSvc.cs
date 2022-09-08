using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class HttpSvc : HttpBase<HttpSvc>, IHttp
    {
        private Dictionary<string, Action<HttpCb>> _CallbackDict = new Dictionary<string, Action<HttpCb>>();
        private DownloadMgr _DownloadMgr = new DownloadMgr();


        /// <summary>
        /// Get请求
        /// </summary>
        public void Get(string url, Action<HttpCb> callback)
        {
            if (callback != null)
            {
                _CallbackDict.Add(url, callback);
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
                _CallbackDict.Add(url, callback);
            }
            PostUrl(url, key, json);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public DownloadFileItem DownloadFile(string url, Action<HttpFileCb> cb)
        {
            return _DownloadMgr.DownloadFile(url, cb);
        }

        public DownloadFileItem GetDownloadFileItem(string url)
        {
            return _DownloadMgr.GetDownloadFileItem(url);
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        public DownloadSpriteItem DownloadSprite(string url, Action<HttpSpriteCb> cb)
        {
            return _DownloadMgr.DownloadSprite(url, cb);
        }

        public DownloadSpriteItem GetDownloadSpriteItem(string url)
        {
            return _DownloadMgr.GetDownloadSpriteItem(url);
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
                if (_CallbackDict.ContainsKey(request.url))
                {
                    _CallbackDict[request.url]?.Invoke(httpCb);
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
                if (_CallbackDict.ContainsKey(request.url))
                {
                    _CallbackDict[request.url]?.Invoke(httpCb);
                    _CallbackDict.Remove(request.url);
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
                if (_CallbackDict.ContainsKey(request.url))
                {
                    _CallbackDict[request.url]?.Invoke(httpCb);
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
                if (_CallbackDict.ContainsKey(request.url))
                {
                    _CallbackDict[request.url]?.Invoke(httpCb);
                    _CallbackDict.Remove(request.url);
                }
            }
        }
        #endregion
    }
}