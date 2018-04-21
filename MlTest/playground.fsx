// open System.IO

// type Observation = { Label: string; Pixels: int[] }

// let toObservation (csvData: string) =
//   let columns = csvData.Split(',')
//   let label = columns.[0]
//   let pixels = columns.[1..] |> Array.map int
//   { Label = label; Pixels = pixels }

// let reader path =
//   let data = File.ReadAllLines path
//   data.[1..]
//   |> Array.map toObservation

// let trainingPath = __SOURCE_DIRECTORY__ +  @"\..\Data\trainingsample.csv"
// let validationPath = __SOURCE_DIRECTORY__ +  @"\..\Data\validationsample.csv"

// let manhattanDistance (pixels1, pixels2) =
//   Array.zip pixels1 pixels2
//   |> Array.sumBy (fun (x,y) -> abs (x-y))
  
// let euclidianDistance (pixels1, pixels2) =
//   Array.zip pixels1 pixels2
//   |> Array.sumBy (fun (x,y) -> pown (x - y) 2)
  

// // type Distance = int[] * int[] -> int
// let train (trainingSet:Observation[]) dist =
//   let classify (pixels:int[]) =
//     trainingSet
//     |> Array.minBy (fun x -> dist (x.Pixels, pixels))
//     |> fun x -> x.Label
//   classify

// let trainingData = reader trainingPath
// let manhattanClassifier = train trainingData manhattanDistance
// let euclidianClassifier = train trainingData euclidianDistance

// let evaluate data classifier =
//   data 
//   |> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0. )

// let validationData = reader validationPath

// let getCost classifier =
//   evaluate validationData  classifier |> (*) 100. 

// getCost euclidianClassifier |> printfn "euclidian: %.2f"
// getCost manhattanClassifier |> printfn "manhattan: %.2f"

// let img1 = trainingData.[0].Pixels
// let img2 = trainingData.[1].Pixels

// /// measure
// #time "on"

// for i in 1..5000 do
//     euclidianDistance (img1, img2)  
//     ignore()
