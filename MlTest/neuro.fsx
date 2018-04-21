// open System.IO
// open Accord.Statistics
// open AForge.Neuro
// open Accord.Neuro.Learning
// open Accord.Neuro
// #I "../packages"
// #r @"Accord/lib/net45/Accord.dll"
// #r @"Accord.MachineLearning/lib/net45/Accord.MachineLearning.dll"
// #r @"Accord.Math/lib/net45/Accord.Math.dll"
// #r @"Accord.Statistics/lib/net45/Accord.Statistics.dll"
// #r "Accord.Neuro/lib/net45/Accord.Neuro.dll"
// #r "AForge.Neuro/lib/AForge.Neuro.dll"
// #r "AForge/lib/AForge.dll"

// printfn "hello %s" "john" 


// let neuRead filename =
//   let path = __SOURCE_DIRECTORY__ + "/../Data/" + filename
//   path
//   |> File.ReadAllLines
//   |> fun lines -> lines.[1..]
//   |> Array.map (fun line -> 
//       let parsed = line.Split ',' 
//       int parsed.[0], parsed.[1..] |> Array.map float)

// let trainNetwork (epochs: int) =
//   let features = 28*28
//   let labels, images = 
//     neuRead "trainingsample.csv" |> Array.unzip
//   let learningLabels = Tools.Expand(labels, -1., 1.)
//   let network = ActivationNetwork(BipolarSigmoidFunction(), features, [| 100; 10 |])
//   NguyenWidrow(network).Randomize()

//   let teacher = new ParallelResilientBackpropagationLearning(network)
//   let rec learn iter =
//     let error = teacher.RunEpoch(images, learningLabels)
//     printfn "%.3f / %i" error iter
//     if error < 0.01 then ignore ()
//     elif iter > epochs then ignore () 
//     else learn (iter + 1)

//   learn 0

//   network

// let ann = trainNetwork 50

// let validate = neuRead "validationsample.csv"

// validate 
// |> Array.averageBy (fun (label, image) -> 
//     let predicted = 
//       ann.Compute(image)
//       |> Array.mapi (fun i x -> i,x)
//       |> Array.maxBy snd
//       |> fst
//     if label = predicted then 1. else 0.)