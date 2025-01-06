using System;
using System.Collections.Generic;

namespace ClassicByte.DataBase
{
    /// <summary>
    /// 表示一个数据库对象，提供加载，实例化数据库的方法。<br/>
    /// Represents a database object that provides methods for loading and instantiating the database.
    /// </summary>
    [Serializable]
    public class DataBase
    {
        /// <summary>
        /// 实例化一个新的数据库对象。<br>
        /// </summary>
        /// <param name="name">数据库的名称</param>
        public DataBase(String name)
        {
            Root = new DataKey();
            Name = name;
        }
        /// <summary>
        /// 数据库的名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 数据库的根键。
        /// </summary>
        public DataKey Root { get; }
    }

    /// <summary>
    /// 表示数据库中的一个键。
    /// </summary>
    [Serializable]
    public class DataKey
    {
        /// <summary>
        /// 实例化一个新的键对象。
        /// </summary>
        public DataKey()
        {
            _values = new Dictionary<string, object>();
            _subkeys = new Dictionary<string, DataKey>();
        }

        /// <summary>
        /// 获取指定路径的值<br/>
        /// Get selected value
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Object GetValue(String path)
        {
            try
            {
                var pathsAndVals = path.Split('/');
                RemoveLast(pathsAndVals, out string[] paths, out string val);
                DataKey key = this;
                foreach (var item in paths)
                {
                    if (String.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    key = key.OpenKey(item);
                }
                return key.GetValue(val);
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void RemoveLast<T>(T[] originalArray, out T[] newArray, out T lastElement)
        {
            if (originalArray == null || originalArray.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty.");
            }

            // 获取最后一个元素
            lastElement = originalArray[originalArray.Length - 1];

            // 创建一个新数组，长度比原数组少1
            newArray = new T[originalArray.Length - 1];

            // 将原数组的元素（除了最后一个）复制到新数组
            Array.Copy(originalArray, newArray, newArray.Length);
        }

        /// <summary>
        /// 设置当前键的值。<br>
        /// Set the value of current DataKey
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetValue(String name, Object value)
        {
            try
            {
                if (_values.ContainsKey(name))
                {
                    _values.Remove(name);
                    _values[name] = value;
                }
                else
                {
                    _values.Add(name, value);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Dictionary<String, Object> _values { get; set; }

        private Dictionary<String, DataKey> _subkeys { get; set; }

        /// <summary>
        /// 根据给定的路径新建键。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataKey CreateKey(String path)
        {
            try
            {
                var pathsAndVals = path.Split('/');
                RemoveLast(pathsAndVals, out string[] paths, out string val);
                DataKey key = this;
                foreach (var item in paths)
                {
                    if (String.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    key = key.CreateKey(item);
                }
                return key;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 根据给定的路径打开已有的键。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataKey OpenKey(String name)
        {
            try
            {
                var pathsAndVals = name.Split('/');
                RemoveLast(pathsAndVals, out string[] paths, out string val);
                DataKey key = this;
                foreach (var item in paths)
                {
                    if (String.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    key = key.OpenKey(item);
                }
                return key;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 移除当前键的子键。
        /// </summary>
        /// <param name="name"></param>
        public void RemoveKey(String name)
        {
            _subkeys.Remove(name);
        }

        /// <summary>
        /// 移除当前键的Value。
        /// </summary>
        /// <param name="name"></param>
        public void RemoveValue(String name)
        {
            _values.Remove(name);
        }
    }


}
