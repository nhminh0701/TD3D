using UnityEngine;
using System.Collections.Generic;

public static class SimplePool
{
    // You can avoid resizing of the Stack's internal data by
    // setting this to a number equal to or greater to what you
    // expect most of your pool sizes to be.
    // Note, you can also use Preload() to set the initial size
    // of a pool -- this can be handy if only some of your pools
    // are going to be exceptionally large (for example, your bullets.)
    private const int DEFAULT_POOL_SIZE = 3;

    /// <summary>
    /// The Pool class represents the pool for a particular prefab.
    /// </summary>
    public class Pool
    {
        // We append an id to the name of anything we instantiate.
        // This is purely cosmetic.
        private int _nextId = 1;

        // The structure containing our inactive objects.
        // Why a Stack and not a List? Because we'll never need to
        // pluck an object from the start or middle of the array.
        // We'll always just grab the last one, which eliminates
        // any need to shuffle the objects around in memory.
        private readonly Queue<GameObject> _inactive;

        //A Hashset which contains all GetInstanceIDs from the instantiated GameObjects 
        //so we know which GameObject is a member of this pool.
        public readonly HashSet<int> MemberIDs;

        // The prefab that we are pooling
        private readonly GameObject _prefab;

        public int StackCount
        {
            get { return _inactive.Count; }
        }

        // Constructor
        public Pool(GameObject prefab, int initialQty)
        {
            _prefab = prefab;
            // If Stack uses a linked list internally, then this
            // whole initialQty thing is a placebo that we could
            // strip out for more minimal code. But it can't *hurt*.
            _inactive = new Queue<GameObject>(initialQty);
            MemberIDs = new HashSet<int>();
        }

        public void Preload(int initialQty, Transform parent = null)
        {
            for (int i = 0; i < initialQty; i++)
            {
                // instantiate a whole new object.
                var obj = GameObject.Instantiate(_prefab, parent);
                obj.name = string.Format("{0} ({1})", _prefab.name, _nextId++);

                // Add the unique GameObject ID to our MemberHashset so we know this GO belongs to us.
                MemberIDs.Add(obj.GetInstanceID());

                obj.SetActive(false);

                _inactive.Enqueue(obj);
            }
        }

        // Spawn an object from our pool
        public GameObject Spawn(Vector3 pos, Quaternion rot)
        {
            while (true)
            {
                GameObject obj;
                if (_inactive.Count == 0)
                {
                    // We don't have an object in our pool, so we
                    // instantiate a whole new object.
                    obj = GameObject.Instantiate(_prefab, pos, rot);
                    obj.name = string.Format("{0} ({1})", _prefab.name, _nextId++);

                    // Add the unique GameObject ID to our MemberHashset so we know this GO belongs to us.
                    MemberIDs.Add(obj.GetInstanceID());
                }
                else
                {
                    // Grab the last object in the inactive array
                    obj = _inactive.Dequeue();

                    if (obj == null)
                    {
                        // The inactive object we expected to find no longer exists.
                        // The most likely causes are:
                        //   - Someone calling Destroy() on our object
                        //   - A scene change (which will destroy all our objects).
                        //     NOTE: This could be prevented with a DontDestroyOnLoad
                        //	   if you really don't want this.
                        // No worries -- we'll just try the next one in our sequence.

                        continue;
                    }
                }
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
                return obj;
            }
        }

        public T Spawn<T>(Vector3 pos, Quaternion rot)
        {
            return Spawn(pos, rot).GetComponent<T>();
        }

        //public void ManuelPush(GameObject obj)
        //{
        //    inactive.Push(obj);
        //}
        // Return an object to the inactive pool.
        public void Despawn(GameObject obj)
        {
            if (!obj.activeSelf)
                return;
            obj.SetActive(false);
            // Since Stack doesn't have a Capacity member, we can't control
            // the growth factor if it does have to expand an internal array.
            // On the other hand, it might simply be using a linked list 
            // internally.  But then, why does it allow us to specify a size
            // in the constructor? Maybe it's a placebo? Stack is weird.
            _inactive.Enqueue(obj);
        }
    }

    // All of our pools
    public static Dictionary<int, Pool> _pools;

    /// <summary>
    /// Initialize our dictionary.
    /// </summary>
    private static void Init(GameObject prefab = null, int qty = DEFAULT_POOL_SIZE)
    {
        if (_pools == null)
            _pools = new Dictionary<int, Pool>();

        if (prefab != null)
        {
            //changed from (prefab, Pool) to (int, Pool) which should be faster if we have 
            //many different prefabs.
            var prefabID = prefab.GetInstanceID();
            if (!_pools.ContainsKey(prefabID))
                _pools[prefabID] = new Pool(prefab, qty);
        }
    }

    public static void PoolPreLoad(GameObject prefab, int qty, Transform newParent = null)
    {
        Init(prefab, 1);
        _pools[prefab.GetInstanceID()].Preload(qty, newParent);
    }

    /// <summary>
    /// If you want to preload a few copies of an object at the start
    /// of a scene, you can use this. Really not needed unless you're
    /// going to go from zero instances to 100+ very quickly.
    /// Could technically be optimized more, but in practice the
    /// Spawn/Despawn sequence is going to be pretty darn quick and
    /// this avoids code duplication.
    /// </summary>
    public static GameObject[] Preload(GameObject prefab, int qty = 1, Transform newParent = null)
    {
        Init(prefab, qty);
        // Make an array to grab the objects we're about to pre-spawn.
        var obs = new GameObject[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
            if (newParent != null)
                obs[i].transform.SetParent(newParent);
        }

        // Now despawn them all.
        for (int i = 0; i < qty; i++)
            Despawn(obs[i]);
        return obs;
    }

    /// <summary>
    /// Spawns a copy of the specified prefab (instantiating one if required).
    /// NOTE: Remember that Awake() or Start() will only run on the very first
    /// spawn and that member variables won't get reset.  OnEnable will run
    /// after spawning -- but remember that toggling IsActive will also
    /// call that function.
    /// </summary>
    /// 
    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        Init(prefab);

        return _pools[prefab.GetInstanceID()].Spawn(pos, rot);
    }

    public static GameObject Spawn(GameObject prefab)
    {
        return Spawn(prefab, Vector3.zero, Quaternion.identity);
    }

    public static T Spawn<T>(T prefab) where T : Component
    {
        return Spawn(prefab, Vector3.zero, Quaternion.identity);
    }

    public static T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : Component
    {
        Init(prefab.gameObject);
        return _pools[prefab.gameObject.GetInstanceID()].Spawn<T>(pos, rot);
    }

    /// <summary>
    /// Despawn the specified gameobject back into its pool.
    /// </summary>
    public static void Despawn(GameObject obj)
    {
        Pool p = null;
        foreach (var pool in _pools.Values)
        {
            if (pool.MemberIDs.Contains(obj.GetInstanceID()))
            {
                p = pool;
                break;
            }
        }

        if (p == null)
        {
            Debug.LogFormat("Object '{0}' wasn't spawned from a pool. Destroying it instead.", obj.name);
            Object.Destroy(obj);
        }
        else
        {
            p.Despawn(obj);
        }
    }

    public static int GetStackCount(GameObject prefab)
    {
        if (_pools == null)
            _pools = new Dictionary<int, Pool>();
        if (prefab == null) return 0;
        return _pools.ContainsKey(prefab.GetInstanceID()) ? _pools[prefab.GetInstanceID()].StackCount : 0;
    }

    public static void ClearPool()
    {
        if (_pools != null)
        {
            _pools.Clear();
        }
    }
}