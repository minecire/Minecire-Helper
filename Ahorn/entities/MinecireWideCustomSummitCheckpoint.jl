module MinecireWideCustomSummitCheckpoint

using ..Ahorn, Maple

@mapdef Entity "MinecireHelper/WideCustomSummitCheckpoint" CustomSummitCheckpoint_minecire_wide(x::Integer, y::Integer, firstDigit::String="zero", secondDigit::String="zero",
    spriteDirectory::String="MinecireHelper/summitcheckpoints_wide", confettiColors::String="fe2074,205efe,cefe20")

const placements = Ahorn.PlacementDict(
    "Custom Summit Checkpoint (wide) [Minecire Helper]" => Ahorn.EntityPlacement(
        CustomSummitCheckpoint_minecire_wide
    )
)

const numberlist = String["blank", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "plus", "minus", "questionmark", "exclaimation", "point", "onepoint", "star", "roman_i", "roman_ii", "roman_iii", "roman_iv", "roman_v", "roman_vi", "roman_x", "berry", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"]

Ahorn.editingOptions(entity::CustomSummitCheckpoint_minecire_wide) = Dict{String, Any}(
    "firstDigit" => numberlist,
    "secondDigit" => numberlist,
)

function Ahorn.selection(entity::CustomSummitCheckpoint_minecire_wide)
    directory = get(entity.data, "spriteDirectory", "MinecireHelper/summitcheckpoints_wide")
    x, y = Ahorn.position(entity)

    return Ahorn.getSpriteRectangle("$directory/base02.png", x, y)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::CustomSummitCheckpoint_minecire_wide, room::Maple.Room)
    directory = get(entity.data, "spriteDirectory", "MinecireHelper/summitcheckpoints_wide")
    digit1 = get(entity.data, "firstDigit", 0)
    digit2 = get(entity.data, "secondDigit", 0)

    Ahorn.drawSprite(ctx, "$directory/base02.png", 0, 0)
    Ahorn.drawSprite(ctx, "$directory/$digit1/numberbg.png", -3, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit1/number.png", -3, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit2/numberbg.png", 3, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit2/number.png", 3, 4)
end

end