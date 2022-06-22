using UnityEngine;

namespace FayvitBasicTools
{
    public static class HierarchyTools
    {
        public static bool EstaNaHierarquia(Transform Tref,Transform hierarchyVerify)
        {
            
            while (hierarchyVerify != null)
            {
                Debug.Log(hierarchyVerify.name + " : " + Tref.name + " : " + (hierarchyVerify == Tref));
                if (hierarchyVerify == Tref)
                    return true;

                hierarchyVerify = hierarchyVerify.parent;
            }

            return false;
        }
    }
}
