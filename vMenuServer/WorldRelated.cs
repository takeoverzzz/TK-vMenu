using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using static CitizenFX.Core.Native.API;

using static vMenuServer.DebugLog;
using static vMenuShared.ConfigManager;

namespace vMenuServer
{
	public class WorldRelated_Server : BaseScript

	{
       
        public WorldRelated_Server()
        {


			EventHandlers["vMenu:WorldRelated_Server"] += new Action<ExpandoObject>(WorldRelated);

            void WorldRelated(ExpandoObject obj)
        	{
                IDictionary<string, object> wwo = obj;
                SetConvarReplicated(Setting.vmenu_set_vehicle_density_multiplier.ToString(), wwo["vmenu_set_vehicle_density_multiplier"].ToString());        
        		SetConvarReplicated(Setting.vmenu_set_ped_density_multiplier.ToString(), wwo["vmenu_set_ped_density_multiplier"].ToString());        
        		SetConvarReplicated(Setting.vmenu_set_random_vehicle_density_multiplier.ToString(), wwo["vmenu_set_random_vehicle_density_multiplier"].ToString());       
        		SetConvarReplicated(Setting.vmenu_set_parked_vehicle_density_multiplier.ToString(), wwo["vmenu_set_parked_vehicle_density_multiplier"].ToString());        
        		SetConvarReplicated(Setting.vmenu_set_scenario_ped_density_multiplier.ToString(), wwo["vmenu_set_scenario_ped_density_multiplier"].ToString());        
        		SetConvarReplicated(Setting.vmenu_set_garbage_trucks.ToString(), wwo["vmenu_set_garbage_trucks"].ToString());
        		SetConvarReplicated(Setting.vmenu_set_random_boats.ToString(), wwo["vmenu_set_random_boats"].ToString());
        		SetConvarReplicated(Setting.vmenu_set_create_random_cops.ToString(), wwo["vmenu_set_create_random_cops"].ToString());
        		SetConvarReplicated(Setting.vmenu_set_create_random_cops_not_onscenarios.ToString(), wwo["vmenu_set_create_random_cops_not_onscenarios"].ToString());
        		SetConvarReplicated(Setting.vmenu_set_create_random_cops_on_scenarios.ToString(), wwo["vmenu_set_create_random_cops_on_scenarios"].ToString()); 
        	}
        }
    }
}