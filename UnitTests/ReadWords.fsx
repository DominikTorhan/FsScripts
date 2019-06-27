let folder = __SOURCE_DIRECTORY__

System.Console.Write(folder) |> ignore
System.Console.Read() |> ignore
 
#load "Main.fs" 
Main.GenerateOutput folder

System.Console.Read() |> ignore