using UnityEngine;

namespace LeeFramework.Http
{
    public class HttpCbBase
    {
        /// <summary>
        /// 是否出错
        /// </summary>
        public bool isError;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMsg;
    }

    /// <summary>
    /// Http回调
    /// </summary>
    public class HttpCb : HttpCbBase
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public string json;
    }

    public class HttpFileCb : HttpCbBase
    {
        /// <summary>
        /// 文件
        /// </summary>
        public byte[] data;
    }

    /// <summary>
    /// 下载图片回调
    /// </summary>
    public class HttpSpriteCb : HttpCbBase
    {
        /// <summary>
        /// 返回Texture
        /// </summary>
        public Texture texture;

        /// <summary>
        /// 返回Sprite
        /// </summary>
        public Sprite sprite;
    }

}