// #I "../packages"
// #r @"Accord/lib/net45/Accord.dll"
// #r @"Accord.MachineLearning/lib/net45/Accord.MachineLearning.dll"
// #r @"Accord.Math/lib/net45/Accord.Math.dll"
// #r @"Accord.Statistics/lib/net45/Accord.Statistics.dll"

// open System
// open System.IO
// open Accord.Statistics.Models.Regression
// open Accord.Statistics.Models.Regression.Fitting

// let readLogistic filename =
//   let path = __SOURCE_DIRECTORY__ + "/../Data/" + filename
//   path
//   |> File.ReadAllLines
//   |> fun lines -> lines.[1..]
//   |> Array.map (fun line -> 
//       let parsed = line.Split ',' |> Array.map float
//       parsed.[0], parsed.[1..])

// let training = readLogistic "trainingsample.csv"
// let validation = readLogistic "validationsample.csv" 

// let labeler = function  
//   | 4. -> 0.
//   | 9. -> 1.
//   | _ -> failwith "unknown digit"

// let fours = training |> Array.filter (fun (label,_) -> label = 4.)
// let nines = training |> Array.filter (fun (label,_) -> label = 9.)

// let labels, images =
//   Array.append fours nines
//   |> Array.map (fun (label, image) -> labeler label, image)
//   |> Array.unzip

// let features = 28 * 28
// let model = LogisticRegression(features)

// let trainLogistic model = 
//   let learner = LogisticGradientDescent model
//   let minDelta = 0.001
//   let rec improve () =
//     let delta = learner.Run(images, labels)
//     printfn "%.4f" delta
//     if delta <= minDelta then ignore () else improve () 
//     ()

//   improve ()


// let accuracy () =
//   validation
//   |> Array.filter (fun (l,_) -> l = 4. || l = 9.)
//   |> Array.map (fun (label, image) -> (if label = 4. then 0. else 1.), image)
//   |> Array.averageBy (fun (label, image) ->
//       let predicted = if model.Compute (image) > 0.5 then 1. else 0.
//       let real = label
//       if predicted = real then 1. else 0.)

// let oneVsAll =
//   let labels = [0. .. 9.]
//   let models = 
//     labels 
//     |> List.map (fun target ->
//         printfn "training label %.0f" target

//         let trainingLabels, trainingFeatures =
//           training
//           |> Array.map (fun (l,f) -> if l = target then 1., f else 0., f)
//           |> Array.unzip

//         let model = LogisticRegression features
//         let learner = LogisticGradientDescent model

//         let maxIterations = 1000
//         let rec improve iteration =
//           if iteration = maxIterations then ignore ()
//           else
//             let delta = learner.Run(trainingFeatures, trainingLabels)
//             if delta <= 0.001 then ignore ()
//             else 
//               improve (iteration + 1)
//         improve 0        
//         target, model)
//   let classifier (image: float[]) =
//     models
//     |> List.maxBy (fun (_,m) -> m.Compute image)
//     |> fun (label, confidence) -> label

//   classifier


// let oneVsAllAccuracy () =
//   validation
//   |> Array.averageBy (fun (l, i) -> if (oneVsAll i) = l then 1. else 0.)
   

                   





