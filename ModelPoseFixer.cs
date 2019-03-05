/*
 Model Pose Fixer Script
 Written by Yuzuka4573 (@yuzuka4573 / @kagamine_yu)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ModelPoseFixer : MonoBehaviour
{
    /// <summary>
    /// Game Object of model Left Arm Root object
    /// </summary>
    public GameObject LeftArmRoot;

    /// <summary>
    /// Game Object of model Right Arm Root object
    /// </summary>
    public GameObject RightArmRoot;

    /// <summary>
    /// GameObject List of all Left arm children object
    /// </summary>
    public List<GameObject> LeftList = new List<GameObject>();

    /// <summary>
    /// GameObject List of all Right arm children object
    /// </summary>
    public List<GameObject> RightList = new List<GameObject>();

    /// <summary>
    /// Mode selector (Forced Tpose /Apose)
    /// </summary>
    public bool isForcedT = true;

    /// <summary>
    /// arm angle when you select mode for "to change A pose"
    /// </summary>
    public float angle = 60.0f;

    /// <summary>
    /// Set gameObject which you want not to change angles
    /// </summary>
    public GameObject[] ExcludedBone = new GameObject[1];

    /// <summary>
    /// save all of both arm gameobjects
    /// </summary>
    public void Getter()
    {
        if (LeftArmRoot != null)
        {
            LeftList = GetAllChilderen(LeftArmRoot);
            Debug.Log("Set Left Arm Gameobjects : " + LeftList.Count);
        }
        else Debug.LogWarning("左腕の根本GameObjectが指定されていないため、腕の取得ができませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");

        if (RightArmRoot != null)
        {
            RightList = GetAllChilderen(RightArmRoot);
            Debug.Log("Set Right arm Gameobjects : " + RightList.Count);
        }
        else Debug.LogWarning("右腕の根本GameObjectが指定されていないため、腕の取得ができませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");
    }

    /// <summary>
    /// Model pose Fixer
    /// </summary>
    public void Setter()
    {
        //set all gameobject angle
        if (LeftList != null) foreach (GameObject current in LeftList) AngleFixer(current);
        else Debug.LogWarning("左腕の小要素がないため角度の変更ができませんでした");

        if (RightList != null) foreach (GameObject current in RightList) AngleFixer(current);
        else Debug.LogWarning("左腕の小要素がないため角度の変更ができませんでした");

        //When "isForcedT" is false
        if (!isForcedT)
        {
            // Fix to A pose 
            if (LeftArmRoot != null) LeftArmRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            else Debug.LogWarning("左腕の根本GameObjectが指定されていないため、指定された角度に変更できませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");

            if (RightArmRoot != null) RightArmRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -1 * angle));
            else Debug.LogWarning("右腕の根本GameObjectが指定されていないため、指定された角度に変更できませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");
        }
        else
        {
            // Fix to T pose 
            if (LeftArmRoot != null) LeftArmRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            else Debug.LogWarning("左腕の根本GameObjectが指定されていないため、指定された角度に変更できませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");

            if (RightArmRoot != null) RightArmRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            else Debug.LogWarning("右腕の根本GameObjectが指定されていないため、指定された角度に変更できませんでした\r\nInspector上でGameObjectが指定されているか確認してください。");

        }
    }


    /// <summary>
    /// Get all of Children from Current GameObject
    /// </summary>
    /// <param name="target">Start Object</param>
    /// <returns>All of Children Object from "target"</returns>
    private List<GameObject> GetAllChilderen(GameObject target)
    {
        List<GameObject> data = new List<GameObject>();
        // start getting children gameObjects from target one
        GetChild(target, data);
        // after getting children return it
        return data;
    }

    /// <summary>
    /// Get and Set one of Children from obj
    /// </summary>
    /// <param name="obj">Current GameObject</param>
    /// <param name="objList">Parameta of save results </param>
    void GetChild(GameObject obj, List<GameObject> objList)
    {
        bool canSave = true;
        Transform target = obj.GetComponentInChildren<Transform>();
        if (target.childCount == 0)
            return; //no children in obj

        foreach (Transform dump in target)
        {
            foreach (GameObject sample in ExcludedBone) if (dump.gameObject == sample) canSave = false;
            if (canSave)
            {
                objList.Add(dump.gameObject);
                // recursion it
                GetChild(dump.gameObject, objList);
            }
            else Debug.Log("not saved gameobject : " + dump.gameObject.name);
        }
    }

    /// <summary>
    /// Change current GameObject transform Localrotation to all ZERO
    /// </summary>
    /// <param name="target">GameObject which you want to change angle to all Zero</param>
    private void AngleFixer(GameObject target)
    {
        // set Gameobject rotation to ZERO (all axis)
        target.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ModelPoseFixer))]
public class ExtendedEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        VRMModelPoseFixer fixer = target as VRMModelPoseFixer;

        EditorGUILayout.HelpBox("1. このInspector上で両腕の根本ボーンをアタッチする\r\n2.\"Object Getter\"ボタンを押してボーンを取得\r\n3.\"Model pose Fixer\"ボタンを押してモデルのポーズを変える\r\n(isForcedT が True でT, Falseでangleで指定した角度でAポーズを取ります)\r\n編集後はUniVRMの書き出しと同じ方法で出力\r\n\r\n\"ExcludedBone\"の配列内に角度変更したくないオブジェクトの親を入れておくと、ポーズ変更したときに角度が修正されなくなります", MessageType.None);
        if (GUILayout.Button("Object Getter"))
        {
            fixer.Getter();
        }

        if (GUILayout.Button("Model pose Fixer"))
        {
            fixer.Setter();
        }
    }

}
#endif
