open System
open System.IO
open System.Drawing

let source = new Bitmap(Path.Combine(__SOURCE_DIRECTORY__, "image.jpg"))

let w = source.Width
let h = source.Height

let result = new Bitmap(w, h)

let noisemap = Array2D.create w h 0.0

let random = Random()

for x in 0 .. w - 1 do
    for y in 0 .. h - 1 do
        noisemap.[x, y] <- float (random.Next(256))

let displacement = 10.0

for x in 1 .. w - 2 do
    for y in 1 .. h - 2 do
        let n0 = float noisemap.[x, y] / 255.0
        let n1 = float noisemap.[x + 1, y] / 255.0
        let n2 = float noisemap.[x, y + 1] / 255.0
        let dx = int (Math.Floor((n1 - n0) * displacement + 0.5))
        let dy = int (Math.Floor((n2 - n0) * displacement + 0.5))
        let sx = if (x + dx) > 0 && (x + dx) < w then (x + dx) else w - 1
        let sy = if (y + dy) > 0 && (y + dy) < h then (y + dy) else h - 1
        let color = source.GetPixel(int sx, int sy)
        result.SetPixel(x, y, color)

result.Save(Path.Combine(__SOURCE_DIRECTORY__, "result.jpg"))
