using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Reflection.Editor {
    public static class ReflectionUtils {
        /// <summary>
        /// Retrieves a list of MethodInfo which have the attribute T
        /// </summary>
        /// <param name="obj">The target on which to search for attribute T</param>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <returns>List of MethodInfo</returns>
        public static List<MethodInfo> GetAllMethodsWithAttribute<T>(object obj) where T : Attribute {
            if (obj == null) {
                Debug.LogError("Object is null");
                return null;
            }

            var types = obj.GetAllTypes();

            var result = new List<MethodInfo>();
            for (int i = types.Count - 1; i >= 0; --i) {
                var methods = types[i]
                    .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                BindingFlags.NonPublic | BindingFlags.DeclaredOnly).Where(m =>
                        m.GetCustomAttributes(typeof(T), true).Length > 0);

                foreach (var methodInfo in methods) {
                    if (!result.Exists(m => m.Name == methodInfo.Name)) {
                        result.Add(methodInfo);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns all types and base types of an object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>List of all object types</returns>
        private static List<Type> GetAllTypes(this object obj) {
            var result = new List<Type>();

            var currentType = obj.GetType();
            while (currentType != null) {
                result.Add(currentType);
                currentType = currentType.BaseType;
            }

            return result;
        }
    }
}