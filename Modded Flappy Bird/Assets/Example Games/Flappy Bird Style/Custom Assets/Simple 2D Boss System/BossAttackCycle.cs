using System;
using UnityEngine;

[Serializable]
public struct Attack
{
    public string name;
    public float force;
    public Vector3 direction;
    public float duration;
    public GameObject prefab;
}

public class BossAttackCycle : MonoBehaviour
{
    [SerializeField]
    private Attack[] attackPrefabs;
    private int attackIndex;

    private void Start()
    {
        attackIndex = -1;
    }

    /// <summary>
    /// Used in the event system to start the whole process of an attack.
    /// </summary>
    public void InitiateAttack()
    {
        Transform attackClone = Instantiate(GetCycleAttack(), transform.position, Quaternion.identity).transform;
        AddForceToChildObjects(attackClone);
    }

    /// <summary>
    /// Cycle through all of the child objects of the attack prefab and adds a force.
    /// to them
    /// </summary>
    /// <param name="parentObject"></param>
    private void AddForceToChildObjects(Transform parentObject)
    {
        for (int i = 0; i < parentObject.childCount; i++)
        {
            Transform childObject = parentObject.GetChild(i);
            if(childObject.GetComponent<Rigidbody>() != null)
            {
                Attack currentAttack = attackPrefabs[attackIndex];
                childObject.GetComponent<Rigidbody>().AddForce(currentAttack.direction * currentAttack.force, ForceMode.Impulse);
                Destroy(childObject.gameObject, currentAttack.duration);
            }
        }
    }

    /// <summary>
    /// Loops through all of the attacks in order of the attackPrefabs array
    /// </summary>
    /// <returns></returns>
    private GameObject GetCycleAttack()
    {
        attackIndex++;
        if (attackIndex > attackPrefabs.Length - 1) attackIndex = 0;
        return attackPrefabs[attackIndex].prefab;
    }
}
