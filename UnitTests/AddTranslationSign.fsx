
let folder = __SOURCE_DIRECTORY__

System.Console.Write folder
 
#load "Main.fs" 
Main.AddTranslationSign folder

//System.Console.ReadKey() |> ignore

//let DateFormat = "yyyyMMddTHHmmss"

//let path = folder + "\\dict.txt"
//let str = System.IO.File.ReadAllText(path)

//let lines = str.Split('\n') |> Array.toSeq 
 
//let str' = lines |> String.concat ""

//let path' =  folder + "\\Output" + System.DateTime.Now.ToString(DateFormat) + ".txt" 
//System.IO.File.WriteAllText(path', str')   

System.Console.Read() |> ignore