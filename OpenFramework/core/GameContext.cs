namespace OpenFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Assertions;

    public abstract class GameContext : MonoBehaviour
    {
        public delegate void OnReadyDelegate();
        public abstract event OnReadyDelegate OnReady;
        protected Dictionary<Type, IService> services = new Dictionary<Type, IService>();
        public int serviceCount { get { return services.Count; } }
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
            MonoService.context = context;
            foreach (var service in services.Values)
            {
                service.context = context;
            }
            StartCoroutine(InitializeServices());
        }

        /// <summary>
        /// Initialize all services and assign game context for them.
        /// </summary>
        /// <param name="context">game context for service</param>
        /// <returns></returns>
        protected IEnumerator InitializeServices()
        {
            foreach (var service in services.Values)
            {
                yield return service.Init();
            }
            Debug.Log("Initializing Services finished.");
            ready = true;
            OnReadyCallback();
            yield return 0;
        }


        /// <summary>
        /// Register new Service
        /// </summary>
        protected void Register<T1>() where T1 : IService, new()
        {
            IService service = default(IService);
            service = (IService)Activator.CreateInstance(typeof(T1));
            // Debug.Log("Register: " + typeof(T1));
            // service.context = context;
            var interfaces = typeof(T1).GetInterfaces();
            Type itype = interfaces[0];
            // Debug.Log(" _________________________ " + typeof(T1) + " : " + itype);
            services.Add(itype, service);
            if (service is IUpdatable) updatables.Add((IUpdatable)service);
        }

        /// <summary>
        /// Get registered sevice
        /// </summary>
        /// <returns>service with type T1</returns>
        public IService GetService<T1>() where T1 : IService
        {
            // foreach (var s in services)
            // {
            //     Debug.Log(s.Key + " : " + s.Value);
            // }
            IService service;
            services.TryGetValue(typeof(T1), out service);
            return service;
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
    }
}