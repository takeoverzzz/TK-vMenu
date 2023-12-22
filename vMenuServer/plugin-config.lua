ConfigShared = {}

-- easy drift config
ConfigShared.EasyDriftPlusPlugin = true -- toggle the easy drift plus plugin
ConfigShared.defaultmajordriftval = 15.0 -- default major assist drift angle
ConfigShared.defaultminordriftval = 35.0 -- default minor assist drift angle



-- wheelie manager config
ConfigShared.WheelieManager = true -- toggle the wheelie manager plugin
ConfigShared.defaultmaxangle = 3 -- the base max angle of the wheelie
ConfigShared.defaultenginemodlvleffect = 0.09 -- the effext the engine mod has per level on the max angle of the wheelie
ConfigShared.defaultturboeffect = 0.85 -- the effext the turbo has on the max angle of the wheelie


ConfigShared.vehiclelistwm = { -- per vehicle config
	[GetHashKey("gauntlet4")] = { -- spawn code of vehicle
		maxangle = 3.5, -- the base max angle of the wheelie
		enginemodlvleffect = 0.1, -- the effext the engine mod has per level on the max angle of the wheelie
		turboeffect = 0.9, -- the effext the turbo has on the max angle of the wheelie
	},

}


-- fh4 speedometer config
ConfigShared.FH4SpeedOMeter = true -- toggle the FH4 Speed O' Meter plugin
