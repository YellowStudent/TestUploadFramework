using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransformHelper : MonoBehaviour 
{
    //优点：将复杂的问题简单化。  缺点：每次调用自身，都需要重新分配空间，如果次数过多可能造成内存泄漏。
    //备注：如果出现异常（StackOverflowException），表示递归死循环
    /// <summary>
    /// 1.递归找需要的子物体（注意：目的是范围的缩小）
    /// </summary>
    /// <param name="byTrs">起始位子</param>
    /// <param name="name">寻找子物体的名称</param>
    /// <returns></returns>
    public static Transform FindTransform(Transform byTrs, string name)
    {
        Transform trs = byTrs.Find(name);
        if (trs == null)
        {
            for (int i = 0; i < byTrs.childCount; i++)
            {
                trs = FindTransform(byTrs.GetChild(i), name);
                if (trs != null) return trs;
            }
        }
        return trs;
    }
    /// <summary>
    /// 注视方向旋转
    /// </summary>
    /// <param name="rotationObj">需要旋转的物体</param>
    /// <param name="targetDir">旋转方向</param>
    /// <param name="speed">速度</param>
    public static void LookDirection(Transform currentObj, Vector3 targetDir, float speed)
    {
        Quaternion qua=Quaternion.LookRotation(targetDir);
        currentObj.rotation = Quaternion.Lerp(currentObj.rotation, qua, Time.deltaTime*speed);
    }
    /// <summary>
    /// 注视目标旋转
    /// </summary>
    /// <param name="rotationObj">需要旋转的物体</param>
    /// <param name="target">注视目标</param>
    /// <param name="speed">速度 </param>
    public static void LookTarget(Transform currentObj, Transform target, float speed)
    {
        Quaternion qua = Quaternion.LookRotation(target.position - currentObj.position);
        currentObj.rotation = Quaternion.Lerp(currentObj.rotation, qua, Time.deltaTime*speed);
    }
    /// <summary>
    /// 计算周边满足条件物体
    /// </summary>
    /// <param name="currentObj">目标物体</param>
    /// <param name="tagNames">标签名字集合</param>
    /// <param name="distance">距离</param>
    /// <param name="angle">角度0到360</param>
    /// <returns></returns>
    private static Transform[] CountNearbyObj(Transform currentObj, string[] tagNames, float distance, float angle)
    {
        List<Transform> meet=null;
        foreach (var tagName in tagNames)//根据标签找物体
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag(tagName);
            if (enemys.Length == 0) continue;//如果当前标签物体，不存在于场景，跳过这次循环
            meet = new List<Transform>(enemys.Length);
            Transform[] trsm = ArrayHelper.SelectObj(enemys, (trs) => { return trs.transform; });
            meet.AddRange(trsm);
        }
        meet=meet.FindAll((Transform o) =>
        {
            return Vector3.Distance(currentObj.position, o.position) < distance &&
            Vector3.Angle(currentObj.forward, o.position) < angle / 2;
        });
        return meet.ToArray();
        //for (int i = 0; i < enemys.Length; i++)
        //{
        //    if (Vector3.Distance(currentObj.position, enemys[i].transform.position) < distance)
        //    {
        //        float relativelyAngel = Mathf.Acos(Vector3.Dot(currentObj.forward, enemys[i].transform.position)) * Mathf.Rad2Deg;
        //        if(relativelyAngel<angle)
        //        {
        //            meet.Add(enemys[i]);
        //        }
        //    }
        //}
        //return meet.ToArray();
    }
}
