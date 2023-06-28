local drawableSprite = require("structs.drawable_sprite")

local summitCheckpoint = {}

summitCheckpoint.name = "MinecireHelper/CustomSummitCheckpoint"
summitCheckpoint.depth = 8999

summitCheckpoint.placements = {
    name = "Summit Checkpoint",
    data = {
        firstDigit = "zero",
        secondDigit = "zero",
        spriteDirectory = "MinecireHelper/summitcheckpoints",
        confettiColors = "fe2074,205efe,cefe20"
    }
}

local numberlist =  { "blank", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "plus", "minus", "questionmark", "exclaimation", "point", "onepoint", "star", "roman_i", "roman_ii", "berry", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" }

summitCheckpoint.fieldInformation = {
    firstDigit = {
        options = numberlist
    },
    secondDigit = {
        options = numberlist
    }
}

local backTexture = "%s/base02"
local digitBackground = "%s/%s/numberbg"
local digitForeground = "%s/%s/number"

function summitCheckpoint.sprite(room, entity)
    local directory = entity.spriteDirectory
    local digit1 = entity.firstDigit
    local digit2 = entity.secondDigit

    local backSprite = drawableSprite.fromTexture(string.format(backTexture, directory), entity)
    local backDigit1 = drawableSprite.fromTexture(string.format(digitBackground, directory, digit1), entity)
    local frontDigit1 = drawableSprite.fromTexture(string.format(digitForeground, directory, digit1), entity)
    local backDigit2 = drawableSprite.fromTexture(string.format(digitBackground, directory, digit2), entity)
    local frontDigit2 = drawableSprite.fromTexture(string.format(digitForeground, directory, digit2), entity)

    backDigit1:addPosition(-2, 4)
    frontDigit1:addPosition(-2, 4)
    backDigit2:addPosition(2, 4)
    frontDigit2:addPosition(2, 4)

    local sprites = {
        backSprite,
        backDigit1,
        backDigit2,
        frontDigit1,
        frontDigit2
    }

    return sprites
end

return summitCheckpoint
