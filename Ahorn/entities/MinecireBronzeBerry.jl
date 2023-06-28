module MinecireBronzeBerry

using ..Ahorn, Maple

@mapdef Entity "MinecireHelper/BronzeBerry" BronzeBerry(x::Integer, y::Integer)

const placements = Ahorn.PlacementDict(
    "Bronze Berry (Minecire Helper)" => Ahorn.EntityPlacement(
        BronzeBerry
    )
)

const sprite = "MinecireHelper/bronzeBerry/idle00.png"

function Ahorn.selection(entity::BronzeBerry)
    x, y = Ahorn.position(entity)
    return Ahorn.getSpriteRectangle(sprite, x, y)
end

Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::BronzeBerry, room::Maple.Room) = Ahorn.drawSprite(ctx, sprite, 0, 0)

end