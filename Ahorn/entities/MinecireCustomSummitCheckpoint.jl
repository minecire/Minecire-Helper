module MinecireCustomSummitCheckpoint

using ..Ahorn, Maple

@mapdef Entity "MinecireHelper/CustomSummitCheckpoint" CustomSummitCheckpoint_minecire(x::Integer, y::Integer, firstDigit::String="zero", secondDigit::String="zero",
    spriteDirectory::String="MinecireHelper/summitcheckpoints", confettiColors::String="fe2074,205efe,cefe20")

const placements = Ahorn.PlacementDict(
    "Custom Summit Checkpoint (Minecire Helper)" => Ahorn.EntityPlacement(
        CustomSummitCheckpoint_minecire
    )
)

const numberlist = String["blank", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "plus", "minus", "questionmark", "exclaimation", "point", "onepoint", "star", "roman_i", "roman_ii", "berry", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"]

Ahorn.editingOptions(entity::CustomSummitCheckpoint_minecire) = Dict{String, Any}(
    "firstDigit" => numberlist,
    "secondDigit" => numberlist,
)

function Ahorn.selection(entity::CustomSummitCheckpoint_minecire)
    directory = get(entity.data, "spriteDirectory", "MinecireHelper/summitcheckpoints")
    x, y = Ahorn.position(entity)

    return Ahorn.getSpriteRectangle("$directory/base02.png", x, y)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::CustomSummitCheckpoint_minecire, room::Maple.Room)
    directory = get(entity.data, "spriteDirectory", "MinecireHelper/summitcheckpoints")
    digit1 = get(entity.data, "firstDigit", 0)
    digit2 = get(entity.data, "secondDigit", 0)

    Ahorn.drawSprite(ctx, "$directory/base02.png", 0, 0)
    Ahorn.drawSprite(ctx, "$directory/$digit1/numberbg.png", -2, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit1/number.png", -2, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit2/numberbg.png", 2, 4)
    Ahorn.drawSprite(ctx, "$directory/$digit2/number.png", 2, 4)
end

end