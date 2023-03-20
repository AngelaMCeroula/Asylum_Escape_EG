using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CabinetBehaviour : MonoBehaviour
{
   [SerializeField] private GameObject cabinetFront;
   [SerializeField] private GameObject KeyItem;

   
   private void Start()
   {
      KeyItem.SetActive(false);
   }

   public void OpenCabinet()
   {
      if (cabinetFront != null)
      {
         KeyItem.SetActive(true);
         Destroy(cabinetFront);
      }
   }
}
