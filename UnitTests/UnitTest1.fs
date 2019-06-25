namespace Tests

open NUnit.Framework
open System
open System.Text.RegularExpressions

[<TestClass>]
type TestClass () =
 
    member this.SplitToLines(str: string)=
        let seq = str.Split "\n" |> Array.toSeq 
        seq 
 
    member this.IsLineContainsWord(str: string, word: string) = 
        let matches = Regex.Matches(str, word)    
        matches.Count > 0

    member this.FormatWord(str: string)= 
        let seq = str |> Seq.map (fun c -> Char.ToLower c)
        let head = seq |> Seq.head |> Char.ToUpper
        let seqRest = seq |> Seq.skip 1 |> String.Concat
        let result = head.ToString() + seqRest
        result
 
    member this.StringToWords(str: string)=
        let matches = Regex.Matches(str, @"\b([a-zA-Z]{3,})\b")    
        let matchToStr (item : Match) = item.Value
        let seq = matches |> Seq.cast |> Seq.map matchToStr |> Seq.map this.FormatWord
        seq
         
    member this.SeqToText(seq: seq<string>)=
        let str = seq |> String.concat "\n" 
        str

    member this.TextFileToSeq(path:string) = 
        let str = System.IO.File.ReadAllText path
        let seq = this.StringToWords str 
        let seq' = seq |> Seq.distinct |> Seq.sort
        this.SeqToText seq'
    
    member this.IsLineContainsTranslataion (line:string)= 
        let strs = line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        strs.Length > 1
        
    member this.GetFirstWordFromLine(line:string)=
        let strs = line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) 
        match strs.Length with
            | 0 -> ""
            | _ -> strs.[0]

    member this.GetAllTranslatedWord (path: string)=
        let str = System.IO.File.ReadAllText path 
        let seq = this.SplitToLines str 
        seq |> Seq.filter this.IsLineContainsTranslataion 
            |> Seq.map this.GetFirstWordFromLine
    

    [<SetUp>]
    member this.Setup () = 
        ()
           
    [<Test>]
    member this.Test10()= 
        let seqNew = seq ["Make"; "Zip"; "Put";"Hear";]  
        let seqDict = seq ["Make"; "Zip"; ]    
        let predicate (s:string) = not (seqDict |> Seq.contains s)
        let seqOut = seqNew |> Seq.filter predicate 
        Assert.AreEqual(seq ["Put";"Hear";]  , seqOut )

    [<TestCase("word", "word x")>] 
    [<TestCase("line", "line line")>] 
    [<TestCase("line", "line")>] 
    [<TestCase("", "")>] 
    member this.TestGetFirstWordFromLine(expected:string, line : string) =  
        Assert.AreEqual(expected, this.GetFirstWordFromLine line)

    //[<Test>]
    //member this.Test1 () =
    //    Assert.Pass() 
    [<TestCase(true, "word x")>] 
    [<TestCase(true, "line line")>] 
    [<TestCase(false, "line")>] 
    [<TestCase(false, "line ")>] 
    member this.TestIsLineContainsTranslataion (expected:bool, line : string) =  
        Assert.AreEqual(expected, this.IsLineContainsTranslataion line)
 
 

    [<TestCase(true, "word x", "word")>] 
    [<TestCase(false, "line line", "word")>] 
    [<TestCase(true, "line line", "line")>] 
    member this.TestIsLineContainsWord (expected:bool, line : string, word : string) = 
        let x = this.IsLineContainsWord(line, word)
        Assert.AreEqual(expected, x)
          
    [<Test>] 
    member this.TestGetAllTranslatedWord () =   
        let seq = this.GetAllTranslatedWord "Dict.txt"
        Assert.Pass()

    [<Test>]
    member this.Test2 () =
        let str = "abc  sdf  " 
        let strings = str.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        Assert.AreEqual(2, strings.Length)
         
    [<Test>]
    member this.Test3 () =
        let str = "abc 
        sdf" 
        let strings = str.Split "\n" 
        Assert.AreEqual(2, strings.Length)
 
    [<Test>] 
    member this.Test4 ()=
        let seqA = seq ["ab"; "ab"; "bb";] 
        let seqB = seqA |> Seq.groupBy (fun s-> s)
        let len = Seq.length seqB
        Assert.AreEqual(2, len) 
 
    [<Test>] 
    member this.Test5 ()=
        let seqA = seq ["ab"; "ab"; "bb";] 
        let len = seqA |> Seq.distinct |> Seq.length
        Assert.AreEqual(2, len)

    [<Test>] 
    member this.Test6 () =
        let str = "1line \n 2line\n\n4line" 
        let seq = str.Split "\n" |> Array.toSeq 
        let seq' = seq |> Seq.filter (fun s -> s <> "")
        Assert.AreEqual(3, seq' |> Seq.length)
 
 
    [<Test>] 
    member this.Test7 () =
        let str = " 1line \n 2line   \n\n4line" 
        let seq = str.Split "\n" |> Array.toSeq 
        let seq' = seq |> Seq.filter (fun s -> s <> "") |> Seq.map (fun s -> s.Trim() + " -") 
        let strNew = seq' |> String.concat " " 
        Assert.AreEqual("1line - 2line - 4line -", strNew)
         
    [<Test>] 
    member this.Test8 () =
        //let str = "AAA, bb. BB-aaa    \n bb Aaa bb 1234065(bb) " 
        let str = "Regular expression active pattern" 
        let matches = Regex.Matches(str, @"\b([a-zA-Z]{3,})\b") 
        Assert.AreEqual(4, matches.Count)
         
    [<Test>] 
    member this.Test9 () =
        let str = " 1line \n 2line   \n3line\n4line\n\n" 
        let matches = Regex.Matches(str, @".+")
        let seq = matches |> Seq.cast
        let len = seq |> Seq.length
        Assert.AreEqual(4, len)
             
    [<TestCase("AAa", "Aaa")>] 
    [<TestCase("big", "Big")>] 
    [<TestCase("PRIVATE", "Private")>] 
    member this.TestFormatWord(str : string, expected : string)=
        let result = this.FormatWord str
        Assert.AreEqual(expected, result)
        
    [<Test>]
    member this.TestSeqToText()=
        let seq = seq ["Make"; "Zip"; "Put";"Hear";"Tear";"Jot";"Upper";"Zest";"Wit";"Steam";"Ream";]  
        //let seqSorted = seq |> Seq.sort
        //let str = seqSorted |> String.concat "\n" 
        let str = this.SeqToText seq
        Assert.Pass() 
 
    [<TestCase("TextFile1.txt")>] 
    [<TestCase("C:\Users\Admin\OneDrive\Documents\Managing_Oneself.txt")>] 
    member this.TestTextFileToSeq(path:string)= 
        let str = this.TextFileToSeq path  
        Assert.Pass() 
