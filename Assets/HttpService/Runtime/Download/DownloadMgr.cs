using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace LeeFramework.Http
{
    public class DownloadMgr
    {
        private Dictionary<string, DownloadItemBase> _AllDownloadHandle = new Dictionary<string, DownloadItemBase>();


        public DownloadMgr()
        {
            DownloadItemBase.onFinish = OnFinish;
        }

        #region 下载文件
        public DownloadFileItem DownloadFile(string url, Action<HttpFileCb> callback = null)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadFileItem)
                {
                    DownloadFileItem fileItem = tmp as DownloadFileItem;
                    fileItem.allCb.Add(callback);

                    return fileItem;
                }
                return null;
            }

            DownloadFileItem item = new DownloadFileItem(url, UnityWebRequest.Get(url), callback);

            _AllDownloadHandle.Add(url, item);

            item.StartDownload();

            return item;
        }

        public DownloadFileItem DownloadFileTask(string url, Action<HttpFileCb> callback = null)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadFileItem)
                {
                    DownloadFileItem fileItem = tmp as DownloadFileItem;
                    fileItem.allCb.Add(callback);
                    return fileItem;
                }
            }

            DownloadFileItem item = new DownloadFileItem(url, UnityWebRequest.Get(url), callback);

            _AllDownloadHandle.Add(url, item);

            return item;
        }

        public DownloadFileItem GetDownloadFileItem(string url)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadFileItem)
                {
                    return tmp as DownloadFileItem;
                }
                return null;
            }

            return null;
        }

        #endregion


        #region 下载图片
        public DownloadSpriteItem DownloadSprite(string url, Action<HttpSpriteCb> callback = null)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadSpriteItem)
                {
                    DownloadSpriteItem spriteItem = tmp as DownloadSpriteItem;
                    spriteItem.allCb.Add(callback);

                    return spriteItem;
                }
                return null;
            }

            DownloadSpriteItem item = new DownloadSpriteItem(url, new UnityWebRequest(url), callback);

            _AllDownloadHandle.Add(url, item);

            item.StartDownload();

            return item;

        }

        public DownloadSpriteItem DownloadSpriteTask(string url, Action<HttpSpriteCb> callback = null)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadSpriteItem)
                {
                    DownloadSpriteItem spriteItem = tmp as DownloadSpriteItem;
                    spriteItem.allCb.Add(callback);

                    return spriteItem;
                }
                return null;
            }

            DownloadSpriteItem item = new DownloadSpriteItem(url, new UnityWebRequest(url), callback);

            _AllDownloadHandle.Add(url, item);

            return item;
        }

        public DownloadSpriteItem GetDownloadSpriteItem(string url)
        {
            DownloadItemBase tmp = null;

            if (_AllDownloadHandle.TryGetValue(url, out tmp) && tmp != null)
            {
                if (tmp is DownloadSpriteItem)
                {
                    return tmp as DownloadSpriteItem;
                }
                return null;
            }
            return null;
        }
        #endregion

        private void OnFinish(string url)
        {
            RemoveHandle(url);
        }


        /// <summary>
        /// 移除下载任务
        /// </summary>
        public bool RemoveHandle(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            if (_AllDownloadHandle.ContainsKey(url))
            {
                _AllDownloadHandle.Remove(url);
                return true;
            }

            return false;
        }
    }
}