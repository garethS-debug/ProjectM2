﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    public override void Die()
    {
        base.Die();
        //add ragdoll / death anim
        Destroy(gameObject);
        //add loot
    }
}
