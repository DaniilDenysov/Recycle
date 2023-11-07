using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TrainingGroundsSpawn : MonoBehaviour
{
   public static EventHandler<TrainingGroundsSpawn> OnNextSlide;

   public void spawn (GameObject _object)
   {
        Instantiate(_object,transform.position,Quaternion.identity);
   }

}
