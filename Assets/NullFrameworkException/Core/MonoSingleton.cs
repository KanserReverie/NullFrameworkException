using System;
using UnityEngine;

namespace NullFrameworkException
{
    // the '<ExampleSingleton>' is updating the 'TSingletonType' to be ExampleSingleton
    //public class ExampleSingleton : MonoSingleton<ExampleSingleton> { }

    // Generic types are just a placeholder for a future classtype that we don't know about yet.
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> // This makes sure that SINGLETON_TYPE inherits from MonoSingleton.
    {
        public static T Instance
        {
            get
            {
                // The internal instance isn't set, so attempt to find it in the scene
                if(instance == null)
                {
                    // Basically it will search in the scene for any objects of this passed in type.
                    instance = FindObjectOfType<T>();

                    // No instance was found, so throw a NullReferenceException detailing what singleton caused the error
                    if(instance == null)
                    {
                        // The 'typeof(TSingletonType).Name' shows the exact class name of the generic type
                        // This line will also give us a stacktrace, showing where the call to the Instance was before it existed
                        throw new NullReferenceException($"No objects of type: {typeof(T).Name} was found.");
                    }
                }
                return instance;
            }
        }

        private static T instance = null;

        /// <summary>
        /// Has the singleton been generated?
        /// </summary>
        public static bool IsSingletonValid() => instance != null;

        /// <summary>
        /// Force the singleton instance to not be destroyed on scene load.
        /// </summary>
        public static void FlagAsPersistant() => DontDestroyOnLoad(Instance.gameObject);

        /// <summary>
        /// Finds the instance within the scene
        /// </summary>
        protected T CreateInstance() => Instance;
    }
}