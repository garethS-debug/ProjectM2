using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CCTVFOV))]
public class CCTVFOVEditor : Editor
{
    void OnSceneGUI()
    {
        CCTVFOV fow = (CCTVFOV)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }




        //Attack radius
        //c = this.target as EnemyFOV;
        //Handles.color = Color.blue;
        //Handles.DrawWireDisc(c.transform.position + (c.transform.forward * c.offsetdistance) // position

        //                              , c.transform.forward                       // normal
        //                              , c.variance);                              // radius

        CCTVFOV t = target as CCTVFOV;

        //Handles.color = Color.blue;
        //Handles.Label(t.transform.position + Vector3.up * 2,
        //                     t.transform.position.ToString() + "\nShieldArea: " +
        //                     t.shieldArea.ToString());

        //---------> Auto Attack Range
        //	Handles.BeginGUI();
        //	GUILayout.BeginArea(new Rect(Screen.width - 100, Screen.height - 80, 90, 50));

        //	if (GUILayout.Button("Reset Area"))
        //		t.attackDistance = 5;

        //	GUILayout.EndArea();
        //	Handles.EndGUI();

        //	Handles.color = Color.blue;
        //	Handles.DrawWireArc(t.transform.position, t.transform.up, -t.transform.right,
        //						   360, t.attackDistance);

        //	//Handles.color = Color.blue;
        //	//t.shieldArea = Handles.ScaleValueHandle(t.shieldArea,
        //	//                t.transform.position + t.transform.forward * t.shieldArea,
        //	//                t.transform.rotation, 1, Handles.ConeCap, 1);

        //	//---------> Start Attacking Range
        //	Handles.BeginGUI();
        //	GUILayout.BeginArea(new Rect(Screen.width - 100, Screen.height - 80, 90, 50));

        //	if (GUILayout.Button("Reset Area"))
        //		t.attackRange = 5;

        //	GUILayout.EndArea();
        //	Handles.EndGUI();

        //	Handles.color = Color.magenta;
        //	Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.attackRange);
        //	Vector3 AttackviewAngleA = fow.DirFromAngle(-fow.attackViewAngle / 2, false);
        //	Vector3 AttackviewAngleB = fow.DirFromAngle(fow.attackViewAngle / 2, false);

        //	Handles.DrawLine(fow.transform.position, fow.transform.position + AttackviewAngleA * fow.attackRange);
        //	Handles.DrawLine(fow.transform.position, fow.transform.position + AttackviewAngleB * fow.attackRange);
        //}
    }
}