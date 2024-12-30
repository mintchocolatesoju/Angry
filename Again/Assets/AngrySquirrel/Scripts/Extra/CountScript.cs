using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountScript : MonoBehaviour
{
   private TMP_Text countText;

   void Start()
   {
      countText = GetComponent<TextMeshPro>();
      GameManager.Instance.acornText = countText;
   }
   void Update()
   {
      countText = GameManager.Instance.acornText;
   }
}
