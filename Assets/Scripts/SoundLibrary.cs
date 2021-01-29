﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "New SoundLibrary", menuName = "CG TOOLS/Sound Library", order = 0)]
public class SoundLibrary : ScriptableObject
{
    public List<string> soundItems;
    [SerializeField]private List<SoundEntity> entities;
    
    public SoundEntity GetEntitiy(SoundItem item)
    {
        return entities.Single(p => p.item == item);
    }

    private void OnValidate()
    {
        foreach (var entity in entities)
        {
            entity.name = entity.item.ToString();
        }
    }
}

[Serializable]
public class SoundEntity
{
    [HideInInspector] public string name;
    public SoundItem item;
    public SoundType type;
    public AudioClip clip;
    [Range(-3f, 3f)] public float pitch = 1;
    [Range(0f, 1f)]  public float volume = 1;
    public bool loop;
}
