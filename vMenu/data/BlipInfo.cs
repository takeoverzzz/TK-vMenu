﻿using System.Collections.Generic;

using Newtonsoft.Json;

using static CitizenFX.Core.Native.API;

namespace vMenuClient.data
{
    public static class BlipInfo
    {
        public static int GetBlipSpriteForVehicle(int vehicle)
        {
            var model = (uint)GetEntityModel(vehicle);
            var sprites = new Dictionary<uint, int>()
            {
                { (uint)GetHashKey("taxi"), 56 },
                //
                { (uint)GetHashKey("nightshark"), 225 },
                //
                { (uint)GetHashKey("rhino"), 421 },
                //
                { (uint)GetHashKey("lazer"), 424 },
                { (uint)GetHashKey("besra"), 424 },
                { (uint)GetHashKey("hydra"), 424 },
                //
                { (uint)GetHashKey("insurgent"), 426 },
                { (uint)GetHashKey("insurgent2"), 426 },
                { (uint)GetHashKey("insurgent3"), 426 },
                //
                { (uint)GetHashKey("limo2"), 460 },
                //
                { (uint)GetHashKey("blazer5"), 512 },
                //
                { (uint)GetHashKey("phantom2"), 528 },
                { (uint)GetHashKey("boxville5"), 529 },
                { (uint)GetHashKey("ruiner2"), 530 },
                { (uint)GetHashKey("dune4"), 531 },
                { (uint)GetHashKey("dune5"), 531 },
                { (uint)GetHashKey("wastelander"), 532 },
                { (uint)GetHashKey("voltic2"), 533 },
                { (uint)GetHashKey("technical2"), 534 },
                { (uint)GetHashKey("technical3"), 534 },
                { (uint)GetHashKey("technical"), 534 },
                //
                { (uint)GetHashKey("apc"), 558 },
                { (uint)GetHashKey("oppressor"), 559 },
                { (uint)GetHashKey("halftrack"), 560 },
                { (uint)GetHashKey("dune3"), 561 },
                { (uint)GetHashKey("tampa3"), 562 },
                { (uint)GetHashKey("trailersmall2"), 563 },
                //
                { (uint)GetHashKey("alphaz1"), 572 },
                { (uint)GetHashKey("bombushka"), 573 },
                { (uint)GetHashKey("havok"), 574 },
                { (uint)GetHashKey("howard"), 575 },
                { (uint)GetHashKey("hunter"), 576 },
                { (uint)GetHashKey("microlight"), 577 },
                { (uint)GetHashKey("mogul"), 578 },
                { (uint)GetHashKey("molotok"), 579 },
                { (uint)GetHashKey("nokota"), 580 },
                { (uint)GetHashKey("pyro"), 581 },
                { (uint)GetHashKey("rogue"), 582 },
                { (uint)GetHashKey("starling"), 583 },
                { (uint)GetHashKey("seabreeze"), 584 },
                { (uint)GetHashKey("tula"), 585 },
                //
                { (uint)GetHashKey("avenger"), 589 },
                { (uint)GetHashKey("avenger2"), 589 },
                //
                { (uint)GetHashKey("stromberg"), 595 },
                { (uint)GetHashKey("deluxo"), 596 },
                { (uint)GetHashKey("thruster"), 597 },
                { (uint)GetHashKey("khanjali"), 598 },
                { (uint)GetHashKey("riot2"), 599 },
                { (uint)GetHashKey("volatol"), 600 },
                { (uint)GetHashKey("barrage"), 601 },
                { (uint)GetHashKey("akula"), 602 },
                { (uint)GetHashKey("chernobog"), 603 },
                //
                { (uint)GetHashKey("seasparrow"), 612 },
                { (uint)GetHashKey("caracara"), 613 },
                //
                { (uint)GetHashKey("pbus2"), 631 },
                { (uint)GetHashKey("terbyte"), 632 },
                { (uint)GetHashKey("menacer"), 633 },
                { (uint)GetHashKey("scramjet"), 634 },
                { (uint)GetHashKey("pounder2"), 635 },
                { (uint)GetHashKey("mule4"), 636 },
                { (uint)GetHashKey("speedo4"), 637 },
                { (uint)GetHashKey("blimp3"), 638 },
                { (uint)GetHashKey("oppressor2"), 639 },
                { (uint)GetHashKey("strikeforce"), 640 },
                //
                { (uint)GetHashKey("bruiser"), 658 },
                { (uint)GetHashKey("bruiser2"), 658 },
                { (uint)GetHashKey("bruiser3"), 658 },
                { (uint)GetHashKey("brutus"), 659 },
                { (uint)GetHashKey("brutus2"), 659 },
                { (uint)GetHashKey("brutus3"), 659 },
                { (uint)GetHashKey("cerberus"), 660 },
                { (uint)GetHashKey("cerberus2"), 660 },
                { (uint)GetHashKey("cerberus3"), 660 },                
                { (uint)GetHashKey("deathbike"), 661 },
                { (uint)GetHashKey("deathbike2"), 661 },
                { (uint)GetHashKey("deathbike3"), 661 },                
                { (uint)GetHashKey("dominator4"), 662 },
                { (uint)GetHashKey("dominator5"), 662 },
                { (uint)GetHashKey("dominator6"), 662 },
                { (uint)GetHashKey("impaler2"), 663 },
                { (uint)GetHashKey("impaler3"), 663 },
                { (uint)GetHashKey("impaler4"), 663 },
                { (uint)GetHashKey("imperator"), 664 },
                { (uint)GetHashKey("imperator2"), 664 },
                { (uint)GetHashKey("imperator3"), 664 },
                { (uint)GetHashKey("issi4"), 665 },
                { (uint)GetHashKey("issi5"), 665 },
                { (uint)GetHashKey("issi6"), 665 },
                { (uint)GetHashKey("monster3"), 666 },
                { (uint)GetHashKey("monster4"), 666 },
                { (uint)GetHashKey("monster5"), 666 },
                { (uint)GetHashKey("scarab"), 667 },
                { (uint)GetHashKey("scarab2"), 667 },
                { (uint)GetHashKey("scarab3"), 667 },
                { (uint)GetHashKey("slamvan4"), 668 },
                { (uint)GetHashKey("slamvan5"), 668 },
                { (uint)GetHashKey("slamvan6"), 668 },
                { (uint)GetHashKey("zr380"), 669 },
                { (uint)GetHashKey("zr3802"), 669 },
                { (uint)GetHashKey("zr3803"), 669 },
                //
                { (uint)GetHashKey("everon"), 734 },
                { (uint)GetHashKey("outlaw"), 735 },
                { (uint)GetHashKey("vagrant"), 736 },
                { (uint)GetHashKey("zhaba"), 737 },
                { (uint)GetHashKey("minitank"), 742 },
                //
                { (uint)GetHashKey("winky"), 745 },
                { (uint)GetHashKey("avisa"), 746 },
                { (uint)GetHashKey("veto"), 747 },
                { (uint)GetHashKey("veto2"), 748 },
                { (uint)GetHashKey("verus"), 749 },
                { (uint)GetHashKey("vetir"), 750 },
                { (uint)GetHashKey("seasparrow2"), 753 },
                { (uint)GetHashKey("seasparrow3"), 753 },
                { (uint)GetHashKey("dinghy5"), 754 },
                { (uint)GetHashKey("patrolboat"), 755 },
                { (uint)GetHashKey("toreador"), 756 },
                { (uint)GetHashKey("squaddie"), 757 },
                { (uint)GetHashKey("alkonost"), 758 },
                { (uint)GetHashKey("annihilator2"), 759 },
                { (uint)GetHashKey("kosatka"), 760 },
                //
                { (uint)GetHashKey("patriot3"), 818 },
                { (uint)GetHashKey("jubilee"), 820 },
                { (uint)GetHashKey("granger2"), 821 },
                { (uint)GetHashKey("deity"), 823 },
                { (uint)GetHashKey("champion"), 824 },
                { (uint)GetHashKey("buffalo4"), 825 },
                //
                { (uint)GetHashKey("avenger3"), 589 },
                { (uint)GetHashKey("avenger4"), 589 },
                { (uint)GetHashKey("raiju"), 861 },
                { (uint)GetHashKey("conada2"), 862 },
                { (uint)GetHashKey("streamer216"), 865 },
            };

            string jsonData = LoadResourceFile(GetCurrentResourceName(), "config/vehblips.json") ?? "{}";
            var vehblips = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(jsonData);

            foreach (var blips in vehblips["vehblips"])
            {
                    uint veh = (uint)GetHashKey(blips.Key);
                    sprites[veh] = blips.Value;
            }


            if (sprites.ContainsKey(model))
            {
                return sprites[model];
            }
            else if (IsThisModelABike(model))
            {
                return 348;
            }
            else if (IsThisModelABoat(model))
            {
                return 427;
            }
            else if (IsThisModelAHeli(model))
            {
                return 422;
            }
            else if (IsThisModelAPlane(model))
            {
                return 423;
            }
            return 225;
        }
    }
}
