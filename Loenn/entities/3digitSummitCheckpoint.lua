local drawableSprite = require("structs.drawable_sprite")

local summitCheckpoint = {}

summitCheckpoint.name = "MinecireHelper/ThreeDigitSummitCheckpoint"
summitCheckpoint.depth = 8999

summitCheckpoint.placements = {
    name = "3 Digit Summit Checkpoint",
    data = {
        firstDigit = "zero",
        secondDigit = "zero",
        thirdDigit = "zero",
        spriteDirectory = "MinecireHelper/summitcheckpoints_3digit",
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
    },
    thirdDigit = {
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
    local digit3 = entity.thirdDigit

    local backSprite = drawableSprite.fromTexture(string.format(backTexture, directory), entity)
    local backDigit1 = drawableSprite.fromTexture(string.format(digitBackground, directory, digit1), entity)
    local frontDigit1 = drawableSprite.fromTexture(string.format(digitForeground, directory, digit1), entity)
    local backDigit2 = drawableSprite.fromTexture(string.format(digitBackground, directory, digit2), entity)
    local frontDigit2 = drawableSprite.fromTexture(string.format(digitForeground, directory, digit2), entity)
    local backDigit3 = drawableSprite.fromTexture(string.format(digitBackground, directory, digit3), entity)
    local frontDigit3 = drawableSprite.fromTexture(string.format(digitForeground, directory, digit3), entity)

    backDigit1:addPosition(-4, 4)
    frontDigit1:addPosition(-4, 4)
    backDigit2:addPosition(0, 4)
    frontDigit2:addPosition(0, 4)
    backDigit3:addPosition(4, 4)
    frontDigit3:addPosition(4, 4)

    local sprites = {
        backSprite,
        backDigit1,
        backDigit2,
        backDigit3,
        frontDigit1,
        frontDigit2,
        frontDigit3
    }

    return sprites
end

return summitCheckpoint
