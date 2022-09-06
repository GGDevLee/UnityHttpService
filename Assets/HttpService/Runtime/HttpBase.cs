using UnityEngine;

namespace LeeFramework.Http
{
    public class HttpBase<T> : MonoBehaviour where T : MonoBehaviour, new()
    {
        private static T _Instance;
        private static GameObject _GameObject;

        public static GameObject obj => _GameObject;

        public static T instance
        {
            get
            {
                if (_Instance == null)
                {
                    _GameObject = new GameObject(typeof(T).ToString());
                    DontDestroyOnLoad(_GameObject);
                    _Instance = _GameObject.AddComponent<T>();
                }
                return _Instance;
            }
        }

        private void Awake()
        {
            if (_Instance == null)
            {
                this.gameObject.name = typeof(T).ToString();
                DontDestroyOnLoad(this);
                if (this.gameObject.GetComponent<T>())
                {
                    _Instance = this.gameObject.GetComponent<T>();
                }
                else
                {
                    _Instance = this.gameObject.AddComponent<T>();
                }
            }
            else
            {
                Destroy(this);
            }
        }
    } 
}
