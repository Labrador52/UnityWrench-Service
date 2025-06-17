using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

namespace Labrador.UnityWrench.Service
{
    public static class Service
    {
        /// <summary>
        /// 储存已注册服务实例的字典，以服务类型为键，服务实例为值。<br/>
        /// A dictionary that stores registered service instances, with the service type as the key and the service instance as the value.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> serviceRegistry = new();

        /// <summary>
        /// 注册服务实例<br/>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="T">注册服务类型。<br/>The type of the service to register. Must implement IService.</typeparam>
        /// <param name="service">要注册的服务实例。<br/>The service instance to register.</param>
        public static void Register<T>(T service, bool force = false) where T : class
        {
            Register(service, out _, force);
        }

        /// <summary>
        /// 注册服务实例，并提供先前注册的服务（如果有）。<br/>
        /// Registers a service instance and provides the previously registered service (if any).
        /// </summary>
        /// <typeparam name="T">注册服务类型。<br/>The type of the service to register. Must implement IService.</typeparam>
        /// <param name="service">要注册的服务实例。<br/>The service instance to register.</param>
        /// <param name="previousService">返回先前注册的同类型服务实例，若为空则返回默认值。<br/>Output parameter that receives the previously registered service of the same type, or default if no service was previously registered.</param>
        public static void Register<T>(T service, out T previousService, bool force = false) where T : class
        {
            Type ServiceType = typeof(T);

            if (serviceRegistry.ContainsKey(ServiceType))
            {
                Debug.LogWarning($"Service of type {ServiceType} is already registered.");
                previousService = (T)serviceRegistry[ServiceType];
                if (force) serviceRegistry[ServiceType] = service;
                return;
            }

            serviceRegistry[ServiceType] = service;
            previousService = default;
            return;
        }

        /// <summary>
        /// 获取已注册的服务实例。<br/>
        /// Retrieves a registered service instance.
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve. Must implement IService.</typeparam>
        /// <returns>已注册的服务实例，如果没有注册指定类型的服务，则返回默认值。<br/>The registered service instance, or default if no service of the specified type is registered.</returns>
        public static T Get<T>() where T : class
        {
            Type ServiceType = typeof(T);

            if (!serviceRegistry.ContainsKey(ServiceType))
            {
                Debug.LogWarning($"Service of type {ServiceType} is not registered.");
                return default;
            }

            return (T)serviceRegistry[ServiceType];
        }

        /// <summary>
        /// 注销已注册的服务实例。<br/>
        /// Unregisters a registered service instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Unregister<T>() where T : class
        {
            Type ServiceType = typeof(T);

            if (!serviceRegistry.TryRemove(ServiceType, out _))
            {
                Debug.LogWarning($"Service of type {ServiceType} is not registered.");
            }
        }
    }

    // 必要时添加服务注销方法
    // Additional methods for unregistering services can be added if necessary.
}