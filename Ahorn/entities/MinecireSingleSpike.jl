module MinecireSingleSpike

using ..Ahorn, Maple

@mapdef Entity "MinecireHelper/SingleSpike" SingleSpike_minecire(x::Integer, y::Integer, type::String="default", direction::String="Up")

const placements = Ahorn.PlacementDict(
    "Single Spike (Up, Default) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Up", "type" => "default")
    ),
    "Single Spike (Left, Default) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Left", "type" => "default")
    ),
    "Single Spike (Down, Default) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Down", "type" => "default")
    ),
    "Single Spike (Right, Default) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Right", "type" => "default")
    ),

    "Single Spike (Up, Outline) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Up", "type" => "outline")
    ),
    "Single Spike (Left, Outline) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Left", "type" => "outline")
    ),
    "Single Spike (Down, Outline) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Down", "type" => "outline")
    ),
    "Single Spike (Right, Outline) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Right", "type" => "outline")
    ),

    "Single Spike (Up, Cliffside) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Up", "type" => "cliffside")
    ),
    "Single Spike (Left, Cliffside) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Left", "type" => "cliffside")
    ),
    "Single Spike (Down, Cliffside) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Down", "type" => "cliffside")
    ),
    "Single Spike (Right, Cliffside) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Right", "type" => "cliffside")
    ),

    "Single Spike (Up, Reflection) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Up", "type" => "reflection")
    ),
    "Single Spike (Left, Reflection) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Left", "type" => "reflection")
    ),
    "Single Spike (Down, Reflection) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Down", "type" => "reflection")
    ),
    "Single Spike (Right, Reflection) (Minecire Helper)" => Ahorn.EntityPlacement(
        SingleSpike_minecire,"rectangle",Dict{String, Any}("direction" => "Right", "type" => "reflection")
    )
)

const typelist = String["default", "outline", "cliffside", "reflection"]
const directionlist = String["Up", "Left", "Down", "Right"]

Ahorn.editingOptions(entity::SingleSpike_minecire) = Dict{String, Any}(
    "type" => typelist,
    "direction" => directionlist,
)


function Ahorn.selection(entity::SingleSpike_minecire)
    directory = "MinecireHelper/danger/single_spike"
    x, y = Ahorn.position(entity)
    
    direction = get(entity.data, "direction", "Up")
    
    offsetX = (direction == "Left" || direction == "Right") ? 4 : 2
    offsetY = (direction == "Up" || direction == "Down") ? 4 : 2

    return Ahorn.getSpriteRectangle(string(directory,"/default_",lowercase(direction),"00.png"), x+offsetX, y+offsetY)
end

function Ahorn.render(ctx::Ahorn.Cairo.CairoContext, entity::SingleSpike_minecire, room::Maple.Room)
    directory = "MinecireHelper/danger/single_spike"
    type = get(entity.data, "type", "default")
    direction = get(entity.data, "direction", "Up")
    offsetX = (direction == "Left" || direction == "Right") ? 4 : 2
    offsetY = (direction == "Up" || direction == "Down") ? 4 : 2
    Ahorn.drawSprite(ctx, string(directory,"/",type,"_",lowercase(direction),"00"),   offsetX, offsetY)

end

end