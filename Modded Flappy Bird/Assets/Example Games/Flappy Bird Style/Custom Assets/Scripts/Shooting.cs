using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public struct AttackPlayer
{
    public string name;
    public float force;
    public Vector3 direction;
    public float duration;
    public GameObject prefab;
}


public class Shooting : MonoBehaviour
{
    [SerializeField]

    private AttackPlayer[] attackPrefabs;
    private int attackIndex;
    public Transform parentObject;

    // Start is called before the first frame update
    void Start()
    {
        attackIndex = -1;
    }


    private void Update()
    {
        IntiateAttack();
    }

    public void IntiateAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*Transform child
            Transform attackClone = Instantiate(
                GetCycleAttack(), transform.position, Quaternion.identity).transform;
            AttackPlayer currentAttack = attackPrefabs[attackIndex];
            GetComponent<RigidBody>().AddForce(currentAttack.direction * currentAttack.force, ForceMode.Impulse);*/
            AddForceToChildObjects(parentObject);
        }
    }

    private void AddForceToChildObjects(Transform parentObject)
    {
        Transform childObject = parentObject.GetChild(1);
        if (childObject.GetComponent<Rigidbody>() != null)
          {
            AttackPlayer currentAttack = attackPrefabs[attackIndex];
            childObject.GetComponent<Rigidbody>().AddForce(currentAttack.direction * currentAttack.force, ForceMode.Impulse);
            Destroy(childObject.gameObject, currentAttack.duration);
          }
    }

    private GameObject GetCycleAttack()
    {
        attackIndex++;
        if (attackIndex > attackPrefabs.Length - 1) attackIndex = 0;
        return attackPrefabs[attackIndex].prefab;
    }
}
