
let folder = __SOURCE_DIRECTORY__

System.Console.Write folder

//System.Console.ReadKey() |> ignore

let DateFormat = "yyyyMMddTHHmmss"

let path = folder + "\\dict.txt"
let str = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8)

let lines = str.Split('\n') |> Array.toSeq 
 
let str' = lines |> String.concat ""

let path' =  folder + "\\Output" + System.DateTime.Now.ToString(DateFormat) + ".txt" 
System.IO.File.WriteAllText(path', str', System.Text.Encoding.UTF8)  