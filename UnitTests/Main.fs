module Main

open System
open System.IO
open System.Text.RegularExpressions

let DateFormat = "yyyyMMddTHHmmss"

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
    let matches = Regex.Matches(str, @"\b([a-zA-Z]{3,})\b")    
    let matchToStr (item : Match) = item.Value
    let seq = matches |> Seq.cast |> Seq.map matchToStr |> Seq.map FormatWord
    seq

let ReadTextFile(path: string)= 
    let str = File.ReadAllText path
    let seq = GetWordsSeqByText str 
    seq |> Seq.distinct |> Seq.sort

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
     let seqText = ReadTextFile(folder+ "\\text.txt")
     let seqDict = GetAllTranslatedWord(folder+ "\\dict.txt")
     let seqOut = GetSeqNoneTranslatedWords(seqText, seqDict)
     //let xxx = seqDict |> Seq.filter (fun s -> seqText |> Seq.contains s)
     let str = SeqToText seqOut
     let str' = "count " + (Seq.length seqOut).ToString() + "\n\n" + str
     WriteTextFile(folder, str') 
