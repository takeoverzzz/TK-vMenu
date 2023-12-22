using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace vMenuServer
{
	public class SetPrimaryMaterial_Server : BaseScript

	{
       
        public SetPrimaryMaterial_Server()
        {
            var VehicleMaterialListPrimary = new Dictionary<int, int>();

			EventHandlers["vMenu:GetPrimaryMaterial_Sync"] += new Action<int, NetworkCallbackDelegate>(PrimaryEvent);

            void PrimaryEvent(int vehicle, NetworkCallbackDelegate SecondarynetworkCB)
        	{
           
                foreach ( var vehiclel in VehicleMaterialListPrimary)
                {
                   
                    if (vehiclel.Key == vehicle)
                        hasvaluePrimary = true;
                }
                if (hasvaluePrimary)
                {
                SecondarynetworkCB.Invoke(VehicleMaterialListPrimary[vehicle]);
                    hasvaluePrimary = false;
            	}
            	else
            	{
                SecondarynetworkCB.Invoke(0);
                    hasvaluePrimary = false;
            	}
           
        	}

            EventHandlers["vMenu:SetPrimaryMaterial_Sync"] += new Action<int, int>(PrimarySave);
            void PrimarySave(int vehicle, int material)
            {
                VehicleMaterialListPrimary[vehicle] = material;
                foreach ( var vehiclel in new Dictionary<int, int>(VehicleMaterialListPrimary))
                {
                    
                    if (!API.DoesEntityExist(API.NetworkGetEntityFromNetworkId(vehiclel.Key)))
                    {
                    	
                        VehicleMaterialListPrimary.Remove(vehiclel.Key);
                    };
                }
            }


        }

        public bool hasvaluePrimary { get; private set; }
    }
	public class SetSecondaryMaterial_Server : BaseScript

	{
       
        public SetSecondaryMaterial_Server()
        {
            var VehicleMaterialListSecondary = new Dictionary<int, int>();

			EventHandlers["vMenu:GetSecondaryMaterial_Sync"] += new Action<int, NetworkCallbackDelegate>(SecondaryEvent);

            void SecondaryEvent(int vehicle, NetworkCallbackDelegate PrimarynetworkCB)
        	{
           
                foreach ( var vehiclel in VehicleMaterialListSecondary)
                {
                    if (vehiclel.Key == vehicle)
                        hasvalueSecondary = true;
                }
                if (hasvalueSecondary)
                {
                PrimarynetworkCB.Invoke(VehicleMaterialListSecondary[vehicle]);
                    hasvalueSecondary = false;
            	}
            	else
            	{
                PrimarynetworkCB.Invoke(0);
                    hasvalueSecondary = false;
            	}
           
        	}

            EventHandlers["vMenu:SetSecondaryMaterial_Sync"] += new Action<int, int>(SecondarySave);
            void SecondarySave(int vehicle, int material)
            {
               
                VehicleMaterialListSecondary[vehicle] = material;
                foreach ( var vehiclel in new Dictionary<int, int>(VehicleMaterialListSecondary))
                {
                    
                    if (!API.DoesEntityExist(API.NetworkGetEntityFromNetworkId(vehiclel.Key)))
                    {
                    	
                        VehicleMaterialListSecondary.Remove(vehiclel.Key);
                    };
                }
            }


        }

        public bool hasvalueSecondary { get; private set; }
    }
}