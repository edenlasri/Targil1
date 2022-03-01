// Learn more about F# at http://fsharp.org

open System
open System.IO

let GatBinaryAritmeticCommand command :string = 
    let a="
    @SP
    A=M-1
    D=M	
    A=A-1		
    M=D"+command.ToString()+"M
    @SP		
    M=M-1"
    a

let GetUnaryAritmeticCommand command :string = 
    let a="
    @SP
    M=M-1
    A=M		
    M= "+command.ToString()+"M
    @SP		
    M=M+1"
    a


//let WriteCommand (ASMfile:string,command:string)= 
//    let readFile = File.ReadAllLines(ASMfile)
//    let result = readFile |> Seq.fold(fun acc x -> acc+x) ""
//    let t= [result+command]
//    File.WriteAllLines(ASMfile,t)

let WriteCommand (ASMfile:StreamWriter,command:string)= 
    ASMfile.WriteLine(command)
    


let VMconvertASM (ASMfile:StreamWriter,VMFile:string) = 
    // Read in a file with StreamReader.
     use stream = new StreamReader(VMFile.ToString())
     // Continue reading while valid lines.
     while (not stream.EndOfStream) do
        let line = stream.ReadLine()

        if not(line=null && line.[0]='\\' && line.[1]='\\') then
            let words=line.Split(" ")
            if(words.[0]="add") then
               WriteCommand (ASMfile,GatBinaryAritmeticCommand "+")
            elif(words.[0]="sub") then
               WriteCommand (ASMfile,GatBinaryAritmeticCommand "-")
            elif(words.[0]="and") then
               WriteCommand (ASMfile,GatBinaryAritmeticCommand "&")
            elif(words.[0]="or") then
               WriteCommand (ASMfile,GatBinaryAritmeticCommand "|")
            elif(words.[0]="neg") then
               WriteCommand (ASMfile,GetUnaryAritmeticCommand "-")
            elif(words.[0]="not") then
               WriteCommand (ASMfile,GetUnaryAritmeticCommand "!")
            elif(words.[0]="eq") then
               printfn "a is less than 20\n"
            elif(words.[0]="gt") then
               printfn "a is less than 20\n"
            elif(words.[0]="lt") then
               printfn "a is less than 20\n"
            elif(words.[0]="pop") then
                printfn "a is less than 20\n"
            elif(words.[0]="push") then
                printfn "a is less than 20\n"
     


[<EntryPoint>]
let main argv =

    
    let DictPath= System.Console.ReadLine()
    let ArrayFile= System.IO.Directory.GetFiles(DictPath,"*.vm")
    
    //C:\Users\edenl\F#\Targil_0\Targil_0.asm For example
    let path = DictPath.Split '\\'
    let Path=DictPath+'\\'.ToString()+(Array.get path (path.Length - 1)) + ".asm"
     
    //Ope File.ASM
    let ASMfile =File.CreateText(Path)
    

    for VMFile in ArrayFile do
         VMconvertASM (ASMfile,VMFile)

    ASMfile.Close()
    0 // return an integer exit code
