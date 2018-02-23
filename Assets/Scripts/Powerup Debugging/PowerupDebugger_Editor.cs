#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PowerupDebugger))]
public class PowerupDebugger_Editor : Editor
{
    private int SelectedPowerupIndex = -1;
    private int SelectedPlayerPowerupIndex = -1;

    public override void OnInspectorGUI()
    {
        //Default
        DrawDefaultInspector();
        //Get Script instance
        PowerupDebugger debugger = (PowerupDebugger)target;

        //If a Player is selected
        if (debugger.Player)
        {
            //Show player selection box with that player in it
            debugger.Player = EditorGUILayout.ObjectField("Player", debugger.Player, typeof(AbilityManager), true, null) as AbilityManager;
            //Make list of available powerup names
            List<string> AvailablePowerupStrings = new List<string>();
            //Get the types from the current assembly
            Type[] Types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            //and a dict for them
            Dictionary<string, Type> AbilityDict = new Dictionary<string, Type>();
            //for each of them
            foreach (Type type in Types)
            {
                if (type.Namespace == "Powerups" && type.IsSubclassOf(typeof(BaseAbility)))
                {
                    AvailablePowerupStrings.Add(type.Name);
                    AbilityDict.Add(type.Name, type);
                }
            }
            //Get a list of the player's current powerups
            List<string> PlayerPowerupStrings = new List<string>();
            //if they have any
            if (debugger.Player.ListAbilities() != null)
            {
                if (debugger.Player.ListAbilities().Count > 0)
                {
                    //do a check for array bounds
                    if (SelectedPlayerPowerupIndex == -1 || SelectedPlayerPowerupIndex >= debugger.Player.ListAbilities().Count)
                    {
                        //and reset if needed
                        SelectedPlayerPowerupIndex = 0;
                    }
                    //and for each powerup the Player has
                    foreach (KeyValuePair<string, BaseAbility> entry in debugger.Player.ListAbilities())
                    {
                        //add its name to the list
                        PlayerPowerupStrings.Add(entry.Value.GetName);
                        //and remove it from the list of available powerups
                        AvailablePowerupStrings.Remove(entry.Value.GetType().Name);
                    }
                }
                else
                {
                    SelectedPlayerPowerupIndex = -1;
                }
                if (AvailablePowerupStrings.Count > 0)
                {
                    if (SelectedPowerupIndex == -1 || SelectedPowerupIndex >= AvailablePowerupStrings.Count)
                    {
                        SelectedPowerupIndex = 0;
                    }
                }
                else
                {
                    SelectedPowerupIndex = -1;
                }
                //Show dropdown of powerups that are available to be added
                SelectedPowerupIndex = EditorGUILayout.IntPopup("Powerup to Add", SelectedPowerupIndex, AvailablePowerupStrings.ToArray(), null);
                //Show dropdown of powerups that are available to be removed
                SelectedPlayerPowerupIndex = EditorGUILayout.IntPopup("Powerup to Remove", SelectedPlayerPowerupIndex, PlayerPowerupStrings.ToArray(), null);

                //set the powerup selected for addition
                if (SelectedPowerupIndex != -1)
                {
                    debugger.SelectedPowerup = AbilityDict[AvailablePowerupStrings[SelectedPowerupIndex]];
                }
                else
                {
                    debugger.SelectedPowerup = null;
                }
                //Set the powerup selected for removal
                if (SelectedPlayerPowerupIndex != -1)
                {
                    debugger.SelectedPlayerPowerup = debugger.Player.ListAbilities()[PlayerPowerupStrings[SelectedPlayerPowerupIndex]];
                }
                else
                {
                    debugger.SelectedPlayerPowerup = null;
                }
            }
        }
        else
        {
            //Show empty player selection box
            debugger.Player = EditorGUILayout.ObjectField("Player", null, typeof(AbilityManager), true, null) as AbilityManager;
        }

        //If a player is selected
        if (debugger.Player)
        {
            //and a powerup is selected
            if (debugger.SelectedPowerup != null)
            {
                //Show a button to add the powerup
                if (GUILayout.Button("Add Powerup"))
                {
                    debugger.AddAbility(ConvertToAbility(debugger.SelectedPowerup));
                }
            }
            //and a player powerup is selected
            if (debugger.SelectedPlayerPowerup)
            {
                //and it is valid
                if (debugger.Player.HasAbility(debugger.SelectedPlayerPowerup))
                    //show a button to remove that powerup
                    if (GUILayout.Button("Remove Powerup"))
                    {
                        debugger.RemoveAbility(debugger.SelectedPlayerPowerup);
                    }
            }
        }
    }

    private BaseAbility ConvertToAbility(Type selectedPowerup)
    {
        return Activator.CreateInstance(selectedPowerup) as BaseAbility;
    }
}

#endif