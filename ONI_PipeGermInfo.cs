using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Database;
using STRINGS;
using Harmony;


namespace ONI_PipeGermInfo
{
    [HarmonyPatch(typeof(BuildingStatusItems), "CreateStatusItems")]
    class AddPipeGermInfo
    {
        static void Postfix(BuildingStatusItems __instance)
        {
            __instance.Pipe.resolveStringCallback = delegate (string str, object data)
            {
                Conduit obj = (Conduit)data;
                int cell2 = Grid.PosToCell(obj);
                ConduitFlow.ConduitContents contents2 = obj.GetFlowManager().GetContents(cell2);
                string text3 = BUILDING.STATUSITEMS.PIPECONTENTS.EMPTY;
                if (contents2.mass > 0f)
                {
                    Element element3 = ElementLoader.FindElementByHash(contents2.element);
                    text3 = string.Format(BUILDING.STATUSITEMS.PIPECONTENTS.CONTENTS, GameUtil.GetFormattedMass(contents2.mass), element3.name, GameUtil.GetFormattedTemperature(contents2.temperature));
                    if (contents2.diseaseIdx != byte.MaxValue)
                    {
                        text3 += string.Format(BUILDING.STATUSITEMS.PIPECONTENTS.CONTENTS_WITH_DISEASE, GameUtil.GetFormattedDisease(contents2.diseaseIdx, contents2.diseaseCount, color: true));
                    }
                }
                str = str.Replace("{Contents}", text3);
                return str;
            };

        }
    }
}
