namespace OpenFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.Assertions;

    public abstract class GameContext : MonoBehaviour
    {
        public delegate void OnReadyHandler();
        public abstract event OnReadyHandler OnReady;
        protected Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
        public int serviceCount { get { return dictionary.Count; } }
        protected List<IUpdatable> updatables = new List<IUpdatable>();

        public bool ready { get; protected set; }
        protected abstract void OnReadyCallback();
        public abstract void SetupGameContext();

        void Awake()
        {
            SetupGameContext();
        }

        protected void Init(GameContext context)
        {
            ready = false;
            if (Application.isPlaying) DontDestroyOnLoad(this.gameObject);
            StartCoroutine(InitializeServices(context));
        }

        /// <summary>
        /// Initialize all services and assign game context for them.
        /// </summary>
        /// <param name="context">game context for service</param>
        /// <returns></returns>
        private IEnumerator InitializeServices(GameContext context)
        {
            foreach (var service in dictionary.Values)
            {
                if (service is IService)
                {
                    IService _iservice = (IService)service;
                    _iservice.context = context;
                    yield return _iservice.Init();
                    Inject(service);
                }
            }
            Debug.Log("Initializing Services finished.");
            dictionary.Add(typeof(GameContext), this);
            ready = true;
            OnReadyCallback();
            yield return 0;
        }


        /// <summary>
        /// Register new Service
        /// </summary>
        protected IService Register<T1>() where T1 : IService, new()
        {
            IService service = default(IService);
            service = (IService)Activator.CreateInstance(typeof(T1));
            // Debug.Log("Register: " + typeof(T1));
            // service.context = context;
            var interfaces = typeof(T1).GetInterfaces();
            Type itype = interfaces[0];
            // Debug.Log(" _________________________ " + typeof(T1) + " : " + itype);
            dictionary.Add(itype, service);
            if (service is IUpdatable) updatables.Add((IUpdatable)service);
            return service;
        }

        public void Bind<T>(object obj)
        {
            var _type = typeof(T);
            dictionary[_type] = obj;
        }

        public void Inject(object obj)
        {
            var fields = Reflector.Reflect(obj.GetType());
            var fieldsLength = fields.Length;
            for (var i = 0; i < fieldsLength; i++)
            {
                var field = fields[i];
                object _value;
                if (!dictionary.TryGetValue(field.FieldType, out _value))
                {
                    Debug.LogError("Could not get " + field.FieldType.FullName + " from injector");
                }
                field.SetValue(obj, _value);
            }
        }

        // public void Inject<T>()
        // {
        //     var fields = Reflector.Reflect(typeof(T));
        //     var fieldsLength = fields.Length;
        //     for (var i = 0; i < fieldsLength; i++)
        //     {
        //         var field = fields[i];
        //         IService service;
        //         if (!services.TryGetValue(field.FieldType, out service))
        //         {
        //             Debug.LogError("Could not get " + field.FieldType.FullName + " from injector");
        //         }
        //         field.SetValue(obj, service);
        //     }
        // }


        /// <summary>
        /// Get registered sevice
        /// </summary>
        /// <returns>service with type T1</returns>
        [Obsolete("GetService is deprecated, please use [Inject] attribute for your services.")]
        public IService GetService<T1>() where T1 : IService
        {
            // foreach (var s in services)
            // {
            //     Debug.Log(s.Key + " : " + s.Value);
            // }
            object service;
            dictionary.TryGetValue(typeof(T1), out service);
            return (IService)service;
        }

        /// <summary>
        /// Update updatable services
        /// </summary>
        void Update()
        {
            if (!ready) return;
            for (int i = 0; i < updatables.Count; i++)
            {
                updatables[i].IUpdate();
            }
        }

        private static class Reflector
        {
            private static readonly Type _injectAttributeType = typeof(Inject);
            private static readonly Dictionary<Type, FieldInfo[]> cachedFieldInfos = new Dictionary<Type, FieldInfo[]>();
            private static readonly List<FieldInfo> _reusableList = new List<FieldInfo>(1024);

            public static FieldInfo[] Reflect(Type type)
            {
                Assert.AreEqual(0, _reusableList.Count, "Reusable list in Reflector was not empty!");

                FieldInfo[] cachedResult;
                if (cachedFieldInfos.TryGetValue(type, out cachedResult))
                {
                    return cachedResult;
                }

                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                for (var fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
                {
                    var field = fields[fieldIndex];
                    var hasInjectAttribute = field.IsDefined(_injectAttributeType, inherit: true);
                    if (hasInjectAttribute)
                    {
                        _reusableList.Add(field);
                    }
                }
                var resultAsArray = _reusableList.ToArray();
                _reusableList.Clear();
                cachedFieldInfos[type] = resultAsArray;
                return resultAsArray;
            }
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public sealed class Inject : System.Attribute { }

}