
//"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\FSharp"

//fsi.exe "C:\Users\dominik\OneDrive\Repo\Fs scripts\Script2.fsx"

//C:\Users\dominik\OneDrive\Repo\Fs scripts\Script2.fsx


/// Shell executing a program
open System.Diagnostics
open System
open System.Windows.Forms



//let text = Clipboard.GetText TextDataFormat.Text 
let text = "Abdomen, (brzuch)     
Audacious - śmiały, zuchwały,     bezczelny"
 
//let array = text.Split([|' '; '.'; ','; '-'; '('; ')'|])
let chars = [|' '; '.'; ','; '-'; '('; ')'; '\n'|]
//let chars = [|'\n'|]
let array = text.Split(chars, StringSplitOptions.RemoveEmptyEntries)

//let x = array |> Array.filter (fun x -> x <> "")  
let arraySorted = Array.sort array

let count = arraySorted.Length

//let text3 = Environment.CurrentDirectory
//let text2 = text + " " + text3 + " " + count.ToString()

let text4 = arraySorted |> String.concat "\n" 
let text5 = count.ToString() + "\n" + text4

Clipboard.SetText text5


let startInfo = new ProcessStartInfo()
startInfo.FileName <- "Notepad.exe"
startInfo.Arguments <- ""
startInfo.UseShellExecute <- true

let proc = Process.Start(startInfo)
proc.WaitForExit()
()


