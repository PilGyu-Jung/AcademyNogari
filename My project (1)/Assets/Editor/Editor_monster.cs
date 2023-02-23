using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(monster))]
public class Editor_monster : Editor
{
    private void OnSceneGUI()
    {

        Handles.color = Color.green;
        monster m_obj = (monster)target;
        Handles.DrawWireArc(m_obj.transform.position, m_obj.transform.up, m_obj.transform.right, 360, m_obj.radius_Detect);
        Handles.color = Color.red;
        Handles.DrawWireArc(m_obj.transform.position, m_obj.transform.up, m_obj.transform.right, 360, m_obj.radius_Attack);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(m_obj.transform.position, m_obj.transform.up, m_obj.transform.right, 360, m_obj.radius_Chase);


    }
}
