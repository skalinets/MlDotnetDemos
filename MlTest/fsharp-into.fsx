// expression
1 + 2

// values
let a = 1
let b = a + 3

// unit value
let c = ()

// mutable values
let mutable m = 5
m <- 7

// functions
let foo () = "hello"
let add x y = x + y
let addReal x =
  let addx y =
    x + y
  addx

// partial application
let add10 = add 10
let x = add10 5

// tuples
let t = 2,"hello"

let n,s = t

let ss = snd t
let nn = fst t

// pipes
let v = t |> fst |> add10
let vv = add10 (fst (t))

// types
type Person = { Name: string; Age: int }

let john = { Name = "John"; Age = 32 }

// structs
let l = [ 1;2;3 ]
let ar = [|1; 2; 3 |]
let i1 = l.[2]
let digits = [0..9]
let d = digits.[3..]

// tail recursion
let rec sum lst =
  match lst with
  | [] -> 0
  | h :: t -> h + sum t

let sumOfDigits = sum digits