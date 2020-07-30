using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {
    public static ObjectPooling op;

    public struct Data {
        public GameObject go;
        public Queue<GameObject> qu;
    }


    static Dictionary<string, Data> dict = null;

    // Start is called before the first frame update
    void Awake()
    {
        op = this;
        if (dict == null)
            dict = new Dictionary<string, Data>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static ObjectPooling GetInstance()
    {
        if (op) return op;
        else
        {
            GameObject go = new GameObject("GameObject Pooling");
            op = go.AddComponent<ObjectPooling>();
            dict = new Dictionary<string, Data>();
            return op;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns> false: Already exist</returns>
    public bool Initialize(GameObject prefab, int count)
    {
        prefab.SetActive(false);
        GameObject go = Instantiate<GameObject>(prefab, transform);
        go.name = prefab.name;
        go.SetActive(false);
        if (dict.TryGetValue(go.name, out Data data))
        {
            return false;
        }
        else
        {
            NewObject(go, count);
            return true;
        }
    }
    public bool Initialize(GameObject prefab)
    {
        return Initialize(prefab, 100);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns> null: Not exist</returns>
    public GameObject Enable(string name)
    {
        if (dict.TryGetValue(name, out Data data))
        {
            if (data.qu.Count == 0)
            {
                AddObject(data);
            }
            GameObject go = data.qu.Dequeue();
            go.SetActive(true);
            return go;
        }
        else
            return null;
    }

    public void Disable(GameObject go)
    {
        go.SetActive(false);
        if (dict.TryGetValue(go.name, out Data data))
        {
            data.qu.Enqueue(go);
        }
        else
        {
            Initialize(go);
        }
    }

    void NewObject(GameObject go, int count)
    {
        Data data = new Data
        {
            go = go,
            qu = new Queue<GameObject>()
        };
        dict.Add(go.name, data);
        AddObject(data, count);
    }

    void NewObject(GameObject go)
    {
        NewObject(go, 100);
    }

    void AddObject(Data data, int count = 100)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate<GameObject>(data.go, transform);
            go.name = data.go.name;
            go.SetActive(false);
            data.qu.Enqueue(go);
        }
    }
}
