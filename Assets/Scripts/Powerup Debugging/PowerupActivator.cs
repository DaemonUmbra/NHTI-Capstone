#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveAbility))]
class PowerupActivator : Editor
    {
    public override void OnInspectorGUI()
    {
        ActiveAbility Ability = (ActiveAbility)target;
        if (GUILayout.Button("Activate"))
        {
            Ability.TryActivate();
        }
        base.OnInspectorGUI();
    }
}
#endif