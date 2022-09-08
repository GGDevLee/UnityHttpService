using System;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public abstract class DownloadItemBase : IDisposable
    {
        public static Action<string> onFinish = null;

        public UnityWebRequest request => _Request;
        public string url => _Url;
        public int timeout => _Timeout;
        public bool isDownloading => _IsDownloading;
        public bool isDownloaded => _IsDownloaded;



        protected UnityWebRequest _Request;
        protected string _Url;
        protected int _Timeout = 30000;
        protected bool _IsDownloading = false;
        protected bool _IsDownloaded = false;




        public DownloadItemBase(string url, UnityWebRequest request)
        {
            _Url = url;
            _Request = request;
        }

        public void SetTimeout(int timeout)
        {
            _Timeout = timeout;
        }

        public float GetProgress()
        {
            if (_Request != null)
            {
                return _Request.downloadProgress;
            }
            return 0;
        }

        public ulong GetDownloadedSize()
        {
            if (_Request != null)
            {
                return _Request.downloadedBytes;
            }
            return 0;
        }




        public abstract void StartDownload();

        public virtual void Dispose()
        {

        }
    }

}