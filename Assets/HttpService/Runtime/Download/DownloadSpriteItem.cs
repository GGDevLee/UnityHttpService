/************************************************************
    文件: DownloadSpriteItem
	作者: 夜猫工作室(Catson)
    邮箱: 419731519@qq.com
    日期: #CreateTime#
	功能: 
*************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class DownloadSpriteItem : DownloadItemBase
    {
        public List<Action<HttpSpriteCb>> allCb => _AllCb;

        private List<Action<HttpSpriteCb>> _AllCb = new List<Action<HttpSpriteCb>>();

        public DownloadSpriteItem(string url, UnityWebRequest request, Action<HttpSpriteCb> callback) : base(url, request)
        {
            _AllCb.Add(callback);
        }

        public override void StartDownload()
        {
            HttpSvc.instance.StartCoroutine(RealDownload());
        }

        private IEnumerator RealDownload()
        {
            _Request.timeout = _Timeout;

            _IsDownloading = true;
            _IsDownloaded = false;

            DownloadHandlerTexture textureHandle = new DownloadHandlerTexture();
            _Request.downloadHandler = textureHandle;

            yield return _Request.SendWebRequest();

            _IsDownloaded = true;
            _IsDownloading = false;

            HttpSpriteCb cb = new HttpSpriteCb();
            if (_Request.result != UnityWebRequest.Result.Success)
            {
                cb.isError = true;
                cb.errorMsg = _Request.error.ToString();
                cb.sprite = null;
            }
            else
            {
                if (textureHandle.texture == null)
                {
                    cb.isError = true;
                    cb.errorMsg = "接受不到图片信息...";
                    cb.sprite = null;
                }
                else
                {
                    cb.isError = false;
                    cb.errorMsg = string.Empty;
                    cb.texture = textureHandle.texture;
                    cb.sprite = Sprite.Create(textureHandle.texture, new Rect(0, 0, textureHandle.texture.width, textureHandle.texture.height), new Vector2(0.5f, 0.5f));
                }
            }
            if (_AllCb != null && _AllCb.Count > 0)
            {
                foreach (Action<HttpSpriteCb> item in _AllCb)
                {
                    item?.Invoke(cb);
                }
            }
        }

    }
}

