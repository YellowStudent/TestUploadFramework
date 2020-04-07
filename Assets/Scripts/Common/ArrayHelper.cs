using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class ArrayHelper
    {
        //(1)敌人类型的升序(2)敌人类任意数据的升序（3）任意类型的任意数据升序
        /// <summary>
        /// 对象数组升序
        /// </summary>
        /// <typeparam name="T">对象元素类型</typeparam>
        /// <typeparam name="Key">元素的某属性</typeparam>
        /// <param name="array">元素数组</param>
        /// <param name="handler">排序的依据</param>
        public static void OrderUp<T, Key>(T[] array, Func<T, Key> handler) where Key : IComparable
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                for (int j = i+1; j < array.Length; j++)
                {
                    if (handler(array[i]).CompareTo(array[j]) > 0)
                    {
                        T info = array[i];
                        array[i] = array[j];
                        array[j] = info;
                    }
                }
            }
        }
        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="array"></param>
        /// <param name="handler"></param>
        public static void OrderDown<T, Key>(T[] array, Func<T, Key> handler) where Key : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (handler(array[i]).CompareTo(array[j]) < 0)
                    {
                        T info = array[i];
                        array[i] = array[j];
                        array[j] = info;
                    }
                }
            }
        }
        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="array"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T GetMaxValue<T,Key>(T[] array,Func<T,Key> handler)where Key:IComparable
        {
            T maxValue = array[1];
            for (int i = 1; i < array.Length; i++)
            {
                if(handler(array[i]).CompareTo(handler(maxValue))>0 )
                {
                    maxValue=array[i];
                }
            }
            return maxValue;
        }
        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="array"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T GetMinValue<T, Key>(T[] array, Func<T, Key> handler) where Key : IComparable
        {
            T minValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (handler(array[i]).CompareTo(handler(minValue)) < 0)//用委托选取T里面要比较的数字Key
                {
                    minValue = array[i];
                }
            }
            return minValue;
        }
        /// <summary>
        /// 查找满足条件的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T[] GetMeetNeed<T>(T[] array,Func<T,bool>handler)//where Key:IComparable
        {
            //T[] newArray = new T[array.Length];
            List<T> newArray = new List<T>(array.Length);
            //int go = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(array[i]))
                {
                    newArray.Add(array[i]);
                    //go++;
                }
            }
            return newArray.ToArray();
        }
        /// <summary>
        /// 筛选对象（比如1、某对象的其他组件）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="array"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Key[] SelectObj<T, Key>(T[] array, Func<T, Key> handler)//`    where T : MonoBehaviour
        {
            Key[] newArray = new Key[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = handler(array[i]);
            }
            return newArray;
        }
    }
