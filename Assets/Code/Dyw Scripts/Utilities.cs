using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace DywFunctions
{
    public static class Utilities
    {
        public static void Show<T>(this ICollection<T> collection)
        {
            if (collection.Count == 0)
            {
                Debug.Log("[]");
                return;
            }

            string message = "[\n";
            foreach (var item in collection)
            {
                if (item == null) continue;
                message += "\t" + item.ToString() + "\n";
            }
            message += "]";
            Debug.Log(message);
        }
        public static void ShowDictionary<TKey, TValue>(this Dictionary<TKey, TValue> collection)
        {
            if (collection.Count == 0)
            {
                Debug.Log("[]");
                return;
            }

            string message = "[\n";
            foreach (var item in collection)
            {
                message += "\t" + "Key: " + item.Key + " Value: " + item.Value + "\n";
            }
            message += "]";
            Debug.Log(message);
        }

        public static string ConvertToString(this char[] value)
        {
            string converted = "";
            value.ForEach(x => converted += x);
            return converted;
        }

        public static Queue<T> ToQueue<T>(this ICollection<T> collection)
        {
            var queue = new Queue<T>();
            collection.ForEach((x, index) => queue.Enqueue(x));
            return queue;
        }

        public static Collider2D CheckCollision(this MonoBehaviour bh, Vector3 position, LayerMask layer)
        {
            var col = Physics2D.OverlapPoint(position, layer);
            return col;
        }

        public static string VecToString(this Vector2 vec)
        {
            return $"x:{vec.x} y:{vec.y}";
        }
        public static string VecToString(this Vector3 vec)
        {
            return $"x:{vec.x} y:{vec.y} z:{vec.z} ";
        }

        public static void WhoIsNull(params object[] objects)
        {
            List<string> nullObjects = new List<string>();
            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i] == null)
                {
                    nullObjects.Add($"Object at index {i} is Null");
                }
            }
            if (nullObjects.Count > 0)
                nullObjects.Show();
            else "No one is null".Print();
        }
        public static void Print(this object obj)
        {
            Debug.Log(obj);
        }

        public static Color MofidyAlpha(this Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);

        public static int HexToDec(string hex) => System.Convert.ToInt32(hex, 16);
        public static string DecToHex(int value) => value.ToString("X2");
        public static string FloatNormalizeToHex(float value) => DecToHex(Mathf.RoundToInt(value * 255f));
        public static float HexToFloatNormalize(string hex) => HexToDec(hex) / 255f;
        public static Color HexToColor(string hex)
        {

            var r = hex.Length >= 2 ? HexToFloatNormalize(hex.Substring(0, 2)) : 0;
            var g = hex.Length >= 4 ? HexToFloatNormalize(hex.Substring(2, 2)) : 0;
            var b = hex.Length >= 6 ? HexToFloatNormalize(hex.Substring(4, 2)) : 0;
            var a = hex.Length >= 8 ? HexToFloatNormalize(hex.Substring(6, 2)) : 1;
            return new Color(r, g, b, a);
        }

        public static Transform[] ExtractChildren(this Transform transform)
        {
            List<Transform> children = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i));
            }
            return children.ToArray();
        }
        public static Transform ExtractChild(this Transform transform, string name)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == name) return transform.GetChild(i);
            }
            return null;
        }

        public static Vector2 ModifyX(this Vector2 vec, float value)
        {
            return new Vector2(value, vec.y);
        }
        public static Vector2 ModifyY(this Vector2 vec, float value)
        {
            return new Vector2(vec.x, value);
        }


        public static Vector3 ModifyX(this Vector3 vec, float value)
        {
            return new Vector3(value, vec.y, vec.z);
        }
        public static Vector3 ModifyY(this Vector3 vec, float value)
        {
            return new Vector3(vec.x, value, vec.z);
        }
        public static Vector3 ModifyZ(this Vector3 vec, float value)
        {
            return new Vector3(vec.x, vec.y, value);
        }
        public static void ForEach<T>(this T[] collection, System.Action<T> predicate)
        {
            foreach (var item in collection)
            {
                predicate(item);
            }
        }
        public static void ForEach<T>(this T[] collection, System.Action<T, int> predicate)
        {
            int i = 0;
            foreach (var item in collection)
            {
                predicate(item, i++);
            }
        }
        public static void ForEach<T>(this ICollection<T> collection, System.Action<T, int> predicate)
        {
            int i = 0;
            foreach (var item in collection)
            {
                predicate(item, i++);
            }
        }
        public static void ForEach<T>(this ICollection<T> collection, System.Action<T> predicate)
        {
            foreach (var item in collection)
            {
                predicate(item);
            }
        }
        public static Vector3 StringToVector(string vectorString)
        {
            string[] values = vectorString.Split(':');

            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }

        public static void RemoveComponent(this Component component)
        {
            Component.Destroy(component);
        }

        public static Collider2D CastPoint(Vector3 point, LayerMask layer)
        {
            return Physics2D.OverlapPoint(point, layer);
        }

        public static bool Overlap(this Bounds bounds, Vector2 position)
        {
            return bounds.min.x <= position.x && bounds.max.x >= position.x
                && bounds.min.y <= position.y && bounds.max.y >= position.y;
        }

        public static bool Overlap(this Bounds bounds1, Bounds bounds2)
        {
            // ???????
            return bounds1.max.x >= bounds2.min.x && bounds1.min.x <= bounds2.max.x
                && bounds1.max.y >= bounds2.min.y && bounds1.min.y <= bounds2.max.y;
        }

        public static Transform FindInChildren(Transform parent, string name)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name == name)
                {
                    return child;
                }
            }
            return null;
        }

        public static Quaternion ToQuaternion(this Vector3 eulerAngles)
        {
            return Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
        }
        public static Quaternion ToQuaternion(this Vector2 eulerAngles)
        {
            return Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }

        /**
            <summary>
                Escoge un elemento random o aleatorio de un arreglo
            </summary>
            
            <returns>
                El objeto seleccionado.
            </returns>
        */
        public static T RandomChoice<T>(this T[] values)
        {
            var i = Random.Range(0, values.Length);
            var len = values.Length;

            if (len <= 0) return default(T);
            if (len == 1) return values[0];

            return values[i];
        }

        /**
            <summary>
                Escoge un elemento random o aleatorio de un arreglo
            </summary>
            
            <returns>
                El objeto seleccionado.
            </returns>
        */
        public static List<T> ToList<T>(this T[] values)
        {
            var list = new List<T>();
            foreach (var value in values)
            {
                list.Add(value);
            }
            return list;
        }
        /**
            <summary>
                Escoge un elemento random o aleatorio de un conjunto de parametros
            </summary>

            <param name="values">
                Elementos para seleccionar
            </param>
            
            <returns>
                El objeto seleccionado.
            </returns>
        */
        public static T ChoiceOne<T>(params T[] values) => values.RandomChoice();

        /// <summary>
        ///     Barajear un arreglo de datos
        /// </summary>
        /// <example>
        ///     Ejemplo: 
        ///     <code>
        ///         int[] arr = new int[] {1, 2, 3, 4, 5, 6};
        ///         arr.Shuffle()
        ///     </code>
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">El arreglo de datos</param>
        /// <param name="shuffleIterations">Numero de veces que se intercambiarán los elementos</param>
        /// <returns>El arreglo desordenado.</returns>
        public static IList<T> Shuffle<T>(this IList<T> values, int shuffleIterations = 0)
        {
            shuffleIterations = shuffleIterations == 0 ? values.Count : shuffleIterations;
            while (shuffleIterations > 1)
            {
                int k = Random.Range(0, shuffleIterations--);
                T temp = values[shuffleIterations];
                values[shuffleIterations] = values[k];
                values[k] = temp;
            }
            return values;
        }
        public static T[] Shuffle<T>(this T[] values, int shuffleIterations = 0)
        {
            return new List<T>(values).Shuffle<T>().ToArray();
        }

        public static void Print<T>(this List<T> list)
        {
            list.ForEach(x => Debug.Log(x));
        }

        public static void Unchild(this Transform t)
        {
            t.parent = null;
        }

        public static GameObject InstatiateFromResource(string path, Vector3 position, Quaternion rotation)
        {
            var g = Resources.Load<GameObject>(path);
            var g_instance = g ? GameObject.Instantiate(g, position, rotation) : null;

            if (!g_instance)
            {
                var pathArr = path.Split('/');
                var objectName = pathArr[pathArr.Length - 1];
                Debug.LogError($"The object named {objectName} doesn't exists in Resources/${path}");
            }
            return g_instance;
        }

        public static string VectorToString(Vector3 vector)
        {
            return vector.x + ":" + vector.y + ":" + vector.z;
        }

        public static Vector3 RandomNavMeshPoint(Vector3 origin, float radius, int areaMask)
        {
            // Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * radius;
            NavMeshHit navHit;
            int iterations = 0;
            var offsetOrigin = origin + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));

            while (!NavMesh.SamplePosition(offsetOrigin, out navHit, radius, areaMask))
            {
                offsetOrigin += new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
                iterations++;
                if (iterations > 100)
                {
                    Debug.LogWarning("Bucle Error");
                    return origin;
                }
            }

            return navHit.position;
        }

        public static Vector3 GetRandomVector3(float scaler = 1)
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * scaler;
        }

        public static Vector2 GetRandomVector2(float scaler = 1)
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * scaler;
        }

        public static void Repeat(System.Action function, int quantity)
        {
            while (quantity > 0)
            {
                function();
                quantity--;
            }
        }

        public static void Repeat(System.Action<int> function, int quantity)
        {
            int count = 0;
            while (quantity > 0)
            {
                function(count);
                quantity--;
                count++;
            }
        }

        public static bool Contains(this LayerMask layer, LayerMask layerMask)
        {
            return (layer.value & 1 << layerMask) != 0;
        }

        public static LayerMask CreateLayerMask(params int[] layers)
        {
            int resultLayer = 0;

            foreach (var layer in layers)
            {
                resultLayer |= 1 << layer;
            }
            return resultLayer;
        }
        public static LayerMask CreateLayerMask(params GameObject[] gameObjects)
        {
            int resultLayer = 0;

            foreach (var gameObject in gameObjects)
            {
                resultLayer |= 1 << gameObject.layer;
            }
            return resultLayer;
        }
    }

}