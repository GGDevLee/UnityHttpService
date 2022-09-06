# HttpService

**联系作者：419731519（QQ）**

### ============HttpService介绍============
#### 基于UnityWebRequest的基础上，封装了多个Http的异步接口
#### 使得可以非常方便的使用Get,Post，DownloadTexture，DownloadSprite
#### 觉得我的插件能帮助到你，麻烦帮我点个Star支持一下❤️

### =================使用方法=================
- manifest.json中添加插件路径
```json
{
  "dependencies": {
	"com.leeframework.httpservice":"https://e.coding.net/ggdevlee/leeframework/HttpService.git#1.0.1"
  }
}
```

- 引入命名空间
```csharp
using LeeFramework.Http;
```

- Get

```csharp

HttpSvc.instance.Get("http:", (cb) =>
 {
	 if (cb.isError)
	 {
		 Debug.Log(cb.errorMsg);
		 return;
	 }
	 Debug.Log(cb.json);
 });

```

- Post

```csharp

HttpSvc.instance.Post("http:", (cb) =>
{
	if (cb.isError)
	{
		Debug.Log(cb.errorMsg);
		return;
	}
	Debug.Log(cb.json);
}, "key", "json");

```

- DownloadTexture

```csharp

HttpSvc.instance.DownloadTexture("http:", (cb) =>
{
	HttpTextureCb textureCb = cb as HttpTextureCb;

	if (textureCb.isError)
	{
		Debug.Log(textureCb.errorMsg);
		return;
	}
	//Todo
	//textureCb.texture
});

```

- DownloadSprite

```csharp

HttpSvc.instance.DownloadSprite("http:", (cb) =>
 {
	 HttpSpriteCb spriteCb = cb as HttpSpriteCb;

	 if (spriteCb.isError)
	 {
		 Debug.Log(spriteCb.errorMsg);
		 return;
	 }
	//Todo
	//spriteCb.sprite
 });

```