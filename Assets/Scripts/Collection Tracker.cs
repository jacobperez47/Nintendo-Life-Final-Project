using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionTracker
{
   static Dictionary<int, bool> collectionList = new Dictionary<int, bool>();

   public static bool add(int key)
   {
      if (!collectionList.ContainsKey(key))
      {
         collectionList.Add(key, true);
         return true;
      }
      return false;
   }


   public static bool get(int key)
   {
      if (collectionList.ContainsKey(key) && collectionList[key] == true)
      {
         return true;
      }
      return false;
   }
   
}
