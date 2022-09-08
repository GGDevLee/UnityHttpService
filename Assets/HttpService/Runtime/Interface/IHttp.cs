using System;

namespace LeeFramework.Http
{
    public interface IHttp
    {
        public void Get(string url, Action<HttpCb> callback);

        public void Post(string url, Action<HttpCb> callback, string key, string json);

        public DownloadFileItem DownloadFile(string url, Action<HttpFileCb> cb);

        public DownloadSpriteItem DownloadSprite(string url, Action<HttpSpriteCb> cb);

        public DownloadFileItem GetDownloadFileItem(string url);

        public DownloadSpriteItem GetDownloadSpriteItem(string url);
    }
}