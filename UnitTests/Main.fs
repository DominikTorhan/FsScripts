module Main

open System
open System.IO
open System.Text.RegularExpressions
open System.Text

let DateFormat = "yyyyMMddTHHmmss"
let PathDict (folder:string)= folder + "\\dict.txt"
let PathText (folder:string)= folder + "\\text.txt"

let SeqToText(seq: seq<string>)=
    let str = seq |> String.concat "\n" 
    str

let FormatWord(str: string)= 
    let seq = str |> Seq.map (fun c -> Char.ToLower c)
    let head = seq |> Seq.head |> Char.ToUpper
    let seqRest = seq |> Seq.skip 1 |> String.Concat
    let result = head.ToString() + seqRest
    result



let GetWordsSeqByText(str: string)=
    let matches = Regex.Matches(str, @"\b([a-zA-Z]{3,})\b")    //uwaga, nie ma polskich znaków
    let matchToStr (item : Match) = item.Value
    let seq = matches |> Seq.cast |> Seq.map matchToStr |> Seq.map FormatWord 
    //let g = seq |> Seq.groupBy (fun x -> x) |> Seq.sortByDescending (fun x -> x.)
    let getCount(str:string, count:int) = count
    let getText(str:string, count:int) = str
    let seq' = seq |> Seq.countBy (fun x -> x) |> Seq.sortByDescending getCount |> Seq.map getText
    seq'
    
let ReadTextFile(path: string)= 
    let str = File.ReadAllText path
    GetWordsSeqByText str 

let IsLineContainsTranslataion (line:string)= 
    let strs = line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
    strs.Length > 1
        
let GetFirstWordFromLine(line:string)=
    let strs = line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) 
    match strs.Length with
        | 0 -> ""
        | _ -> strs.[0]
    
let SplitToLines(str: string)=
    let seq = str.Split([|"\n"|], StringSplitOptions.RemoveEmptyEntries) |> Array.toSeq   
    seq 
     

let GetAllTranslatedWord (path: string)=
    let str = File.ReadAllText path 
    let seq = SplitToLines str 
    seq |> Seq.filter IsLineContainsTranslataion 
        |> Seq.map GetFirstWordFromLine  

let WriteTextFile (folder: string, text: string)=
    let path =  folder + "\\Output" + DateTime.Now.ToString(DateFormat) + ".txt" 
    File.WriteAllText(path, text)  
    ()
    
let GetSeqNoneTranslatedWords(seqText:seq<string>, seqDict:seq<string>)= 
    let predicate (s:string) = not (seqDict |> Seq.exists (fun x -> x= s))
    seqText |> Seq.filter predicate 

 
let GenerateOutput(folder:string)=
    let seqText = ReadTextFile(PathText folder)
    let seqDict = GetAllTranslatedWord(PathDict folder)
    let seqOut = GetSeqNoneTranslatedWords(seqText, seqDict)
    let str = SeqToText seqOut
    let str' = "count text " + (Seq.length seqText).ToString() + "\n\n" + str
    let str' = "count dict " + (Seq.length seqDict).ToString() + "\n\n" + str'
    let str' = "count out " + (Seq.length seqOut).ToString() + "\n\n" + str'
    WriteTextFile(folder, str') 
      
let AddSignToLine(line:string)=  
    match line with
        | line when line.Length < 3 -> line
        | _ -> line + " *" 


let AddTranslationSign(folder : string) =
    let str = System.IO.File.ReadAllText(PathDict(folder))
    let lines = str.Split('\n') |> Array.toSeq |> Seq.map (fun s->s.Replace("\r", "")) 
    let str' = lines |> Seq.map AddSignToLine |> String.concat "\r\n"
    WriteTextFile(folder, str') 

let SortDictionary(folder : string) =    
    let str = System.IO.File.ReadAllText(PathDict(folder))
    let lines = SplitToLines str 
    let linesSorted = lines |> Seq.sort
    let str' = SeqToText linesSorted
    WriteTextFile(folder, str') 
    
zmiany do git revert