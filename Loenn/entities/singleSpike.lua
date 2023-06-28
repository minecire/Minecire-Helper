local spikeHelper = require('helpers.spikes')

local spikeTexture = "danger/single_spike/%s_%s00"

local spikeVariants = {
    "default",
    "outline",
    "cliffside",
    "reflection"
}



local spikeUp = spikeHelper.createEntityHandler("MinecireHelper/SingleSpikeUp", "up", false, false, spikeVariants)
local spikeDown = spikeHelper.createEntityHandler("MinecireHelper/SingleSpikeDown", "up", false, false, spikeVariants)
local spikeLeft = spikeHelper.createEntityHandler("MinecireHelper/SingleSpikeLeft", "up", false, false, spikeVariants)
local spikeRight = spikeHelper.createEntityHandler("MinecireHelper/SingleSpikeRight", "up", false, false, spikeVariants)
local spikes = {spikeUp,spikeDown,spikeLeft,spikeRight}

