using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class DownloadFileItem : DownloadItemBase
    {
        public List<Action<HttpFileCb>> allCb => _AllCb;

        private List<Action<HttpFileCb>> _AllCb = new List<Action<HttpFileCb>>();


        public DownloadFileItem(string url, UnityWebRequest request, Action<HttpFileCb> callback) : base(url, request)
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

            yield return _Request.SendWebRequest();

            _IsDownloaded = true;
            _IsDownloading = false;

            HttpFileCb cb = new HttpFileCb();
            if (_Request.result != UnityWebRequest.Result.Success)
            {
                cb.isError = true;
                cb.errorMsg = _Request.error.ToString();
                cb.data = null;
            }
            else
            {
                byte[] data = _Request.downloadHandler.data;
                if (data != null && data.Length > 0)
                {
                    cb.isError = false;
                    cb.errorMsg = string.Empty;
                    cb.data = data;
                }
                else
                {
                    cb.isError = true;
                    cb.errorMsg = "File为null...";
                    cb.data = null;
                }
            }
            if (_AllCb != null && _AllCb.Count > 0)
            {
                foreach (Action<HttpFileCb> item in _AllCb)
                {
                    item?.Invoke(cb);
                }
            }
            onFinish?.Invoke(_Url);
        }

        public override void Dispose()
        {
            _AllCb.Clear();
            _AllCb = null;
            _Request = null;
        }
    }
}
