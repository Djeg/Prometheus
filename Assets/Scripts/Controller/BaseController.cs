using System;
using System.Reflection;
using System.Collections;
using UnityEngine;

namespace Djeg.Prometheus.Controller
{
    /// <summary>
    /// Allow any object to contains data and event operator
    /// </summary>
    public class BaseController : MonoBehaviour
    {
        # region Properties
        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /// <summary>
        /// Retrieve a data object
        /// </summary>
        public T GetData<T>()
        {
            Type t = typeof(T);
            FieldInfo[] fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (FieldInfo item in fieldInfos)
            {
                if (item.FieldType != t)
                    continue;

                return (T)item.GetValue(this);
            }

            throw new Exception($"{gameObject.name} does not contains the data {t.Name}");
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
