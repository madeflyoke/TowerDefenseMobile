using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TD.GamePlay.Managers;

[CustomEditor(typeof(WavesSpawner), true)]
public class PathWavesEditor : Editor
{
    WavesSpawner wavesSpawner;
    private void OnEnable()
    {
        wavesSpawner = (WavesSpawner)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        int enemiesNumber=0;
        foreach (var pathWave in wavesSpawner.PathWavesList)
        {
            foreach (var enemies in pathWave.waves)
            {
                if (enemies!=null)
                {
                    enemiesNumber += enemies.Enemies.Count;
                }            
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"Enemies count: "+enemiesNumber);
    }
}
