using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
    public ZombieHand zombieHand;
    public int zombieDamage;
    private void Start()
    {
        zombieHand.damage = zombieDamage;
    }
}