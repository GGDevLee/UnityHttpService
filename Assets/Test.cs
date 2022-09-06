using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeeFramework.Http;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image img;

    private void Start()
    {
        HttpSvc.instance.DownloadSprite("https://img1.baidu.com/it/u=1232362262,712358260&fm=253&fmt=auto&app=138&f=JPEG?w=700&h=394", (value) =>
         {
             HttpSpriteCb spriteCb = (HttpSpriteCb)value;

             img.sprite = spriteCb.sprite;
         });
    }
}
