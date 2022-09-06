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

    /// <summary>
    /// 下载图片回调
    /// </summary>
    public class HttpTextureCb : HttpCbBase
    {
        /// <summary>
        /// 返回Texture
        /// </summary>
        public Texture texture;
    }

    /// <summary>
    /// 下载图片回调
    /// </summary>
    public class HttpSpriteCb : HttpCbBase
    {
        /// <summary>
        /// 返回Image
        /// </summary>
        public Sprite sprite;
    }


    /// <summary>
    /// 下载图片回调
    /// </summary>
    public class HttpTexture2DCb : HttpCbBase
    {
        /// <summary>
        /// 返回Texture
        /// </summary>
        public Texture2D texture;
    }

}