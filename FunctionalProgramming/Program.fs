open System.IO
open System.Text.RegularExpressions
open System.Text
open Delivery
open Graphics

let getSumDigitInDegree (n : int) =
    2.0 ** n |> bigint |> string|> Seq.map(string >> System.Int32.Parse) |> Seq.sum

let lab1() =
    let inputVal = System.Console.ReadLine()
    let intVal = System.Int32.Parse(inputVal)
    let result = getSumDigitInDegree(intVal)
    printf "%d"  result

let getMostFrequentLetter(text : string) =
    let letters = [|'а' .. 'я'|]
    let string2arr = text.ToLower().ToCharArray()
    let count pred = Array.filter pred >> Array.length
    letters |> Array.map(fun ch -> ch, count ((=)ch) string2arr) 
            |> Array.maxBy (fun (_,n) -> n)

let getPopularLetter(path : string) = 
    let text = File.ReadAllText(path, Encoding.UTF8)
    let words = Regex.Matches(text, "(\w+-\w+)|(\w+)") |> Seq.cast<Match> |> Seq.map(fun matchWord -> matchWord.Value) 
    let every3rd (tuple : int * string) = 
        match fst tuple%3 with
        | 0 -> Some(snd tuple)
        | _ -> None
    let every3rdWords = words |> Seq.indexed 
                              |> Seq.choose every3rd
    String.concat "" every3rdWords |> getMostFrequentLetter
    
let lab2() =
    let popularLetter = getPopularLetter(@"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\master_text.txt")  
    let letter = fst popularLetter
    let count = snd popularLetter
    printf "Символ %c является самым частым. Он встретился %d раз(а)" letter count

let arrayManipulation (a : array<int>) =
    printf "Начальные данные : %A \n" a
    for i = Array.length a - 2 downto 0 do
        Array.set a i (a[i] + a[i+1])
    printf "Результат : %A \n" a
    0

let lab3() = 
    let a1 = [|3;2;1;0;4|]
    let test_a1 = arrayManipulation a1
    let a2 = [|7;2;9;10;3;4|] // [35 28 26 17 7 4]
    arrayManipulation a2

let lab4() = 
    let bicycleCoirier : ICouirer = new BicycleCoirier(1,"Николай","Абрамов","+791512532525")
    let carCoirier : ICouirer = new CarCoirier(2,"Иван","Омётов","+795745832589")
    let meat = new Product(1,"Мясо по голландски", 155)
    let milk = new Product(2,"Молоко Волжское", 50)
    let order1 = new Order(1,"Улица Есенина 15", meat)
    let order2 = new Order(2,"Улица Гоголя 35", milk)
    bicycleCoirier.Deliver(order1)
    carCoirier.Deliver(order2)
    bicycleCoirier.CancelCurrentOrder()
    carCoirier.CancelCurrentOrder()  


let lab5()=
    let filePaths = [|@"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\master_text.txt"; 
                      @"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\pushkin_text.txt"; 
                      @"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\argun_text.txt"|]
    let asyncTask(path : string) = 
        async {
            let popularLetter = getPopularLetter(path)
            let letter = fst popularLetter
            let count = snd popularLetter
            return $"Символ {letter} является самым частым. Он встретился {count} раз(а)"
        }
    let asyncResults = filePaths |> Seq.map asyncTask 
                                  |> Async.Parallel
                                  |> Async.RunSynchronously

    let outputPath = @"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\output.txt"
    let outputText = Array.map2(fun res path -> $"Путь - {path}, результат - {res} \n") asyncResults filePaths
    use sw = new StreamWriter(outputPath)
    outputText |> Array.map(fun text -> fprintf sw "%s" text)  |> ignore
    sw.Close()


let lab6() =
    let outputPath = @"d:\bitmap.jpg"
    let gr = [('F',str "F+F--F+F")]
    let PI = 3.141592653589
    let lsys = NApply 2 gr (str "+FF-[FF-FF+]")
    let B = TurtleBitmapVisualizer 40.0 (PI/180.0*60.0) lsys
    B.Save(outputPath)

[<EntryPoint>]
let main(args : string[]) = 
    lab5()
    0