using HarmonyLib;
using UnityEngine;

namespace ValheimNoStam.Patches
{

    [HarmonyPatch(typeof(Player), "UseStamina")]
    static class NoStaminaCostPatch
    {
        static void Prefix(Player __instance, ref float v)
        {   
            switch (NoStamCosts.StaminaCostSetting.Value)
            {
                // Use stamina
                case 0:
                    return;
                //  No stamina costs for everything when hold a hammer or hoe
                case 1:
                    // Debug.Log(__instance.GetRightItem().m_shared.m_name);
                    if(__instance.GetRightItem() == null)
                    {
                        return;
                    }
                    string itemName = __instance.GetRightItem().m_shared.m_name;
                    if ((itemName == "$item_hammer" || itemName == "$item_hoe"))
                        v = 0;
                    return;
                // No stamina costs in god mode only
                case 2:
                    if (__instance.InGodMode())
                        v = 0;
                    return;
                // No stamina costs at all regardless of mode
                case 3:
                    v = 0;
                    return;
                default:
                    return;
            }
        }

    }
}
