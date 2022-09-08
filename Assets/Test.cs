using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeeFramework.Http;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    public Image img;

    private void Start()
    {
        HttpSvc.instance.DownloadSprite("https://img1.baidu.com/it/u=1232362262,712358260&fm=253&fmt=auto&app=138&f=JPEG?w=700&h=394", (value) =>
         {
             HttpSpriteCb spriteCb = value;

             if (!spriteCb.isError)
             {
                 img.sprite = spriteCb.sprite;
             }
             else
             {
                 Debug.LogError(spriteCb.errorMsg);
             }
         });

        HttpSvc.instance.DownloadFile("https://img1.baidu.com/it/u=1232362262,712358260&fm=253&fmt=auto&app=138&f=JPEG?w=700&h=394", (value) =>
        {
            HttpFileCb fileCb = value;

            if (!fileCb.isError)
            {
                File.WriteAllBytes(Application.dataPath + "/../aaa.png", fileCb.data);
            }
            else
            {
                Debug.LogError(fileCb.errorMsg);
            }
        });
    }
}
