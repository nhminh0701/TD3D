using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    static T _instance;

    public static T instance
    {
        get
        {

            var _instances = (T[])GameObject.FindObjectsOfType<T>();

            if (_instances.Length > 0)
                {
                    _instance = _instances[0];
                }

            if (_instances.Length > 1)
            {
                    Debug.LogError("More than 1 object of type" + typeof(T).Name);

                for (var index = 1; index < _instances.Length; index++)
                {
                    GameObject.Destroy(_instances[index]);
                }
            }

            else
            {
                    _instance = new GameObject().AddComponent<T>();
            }

            return _instance;
        }
    }  
}
