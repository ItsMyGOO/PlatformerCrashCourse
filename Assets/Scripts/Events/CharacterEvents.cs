using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    public static UnityAction<GameObject, int> CharacterDamaged;
    public static UnityAction<GameObject, int> CharacterHealed;
}