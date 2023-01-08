using UnityEngine;

namespace GameLogic
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool dontDestroy;
    
        private static T _instance;
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        _instance = singleton.AddComponent<T>();
                    }
                
                }

                return _instance;
            }
        }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroy)
                {
                    transform.parent = null;
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else Destroy(gameObject);
        }
    }
}