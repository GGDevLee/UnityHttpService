using System;

namespace LeeFramework.Http
{
    public interface IHttp
    {
        public void Get(string url, Action<HttpCb> callback);

        public void Post(string url, Action<HttpCb> callback, string key, string json);

        public void DownloadTexture(string url, Action<HttpCbBase> cb);

        public void DownloadSprite(string url, Action<HttpCbBase> cb);
    }
}