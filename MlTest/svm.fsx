// #I "../packages"
// open Accord.Statistics.Kernels
// open Accord.MachineLearning.VectorMachines.Learning
// open Accord.MachineLearning.VectorMachines
// #r @"Accord/lib/net45/Accord.dll"
// #r @"Accord.MachineLearning/lib/net45/Accord.MachineLearning.dll"
// #r @"Accord.Math/lib/net45/Accord.Math.dll"
// #r @"Accord.Statistics/lib/net45/Accord.Statistics.dll"

// open System
// open System.IO
// open Accord.Statistics.Models.Regression
// open Accord.Statistics.Models.Regression.Fitting

// let svmRead filename =
//   let path = __SOURCE_DIRECTORY__ + "/../Data/" + filename
//   path
//   |> File.ReadAllLines
//   |> fun lines -> lines.[1..]
//   |> Array.map (fun line -> 
//       let parsed = line.Split ',' 
//       int parsed.[0], parsed.[1..] |> Array.map float)

// let labels, images = 
//   svmRead "trainingsample.csv" |> Array.unzip

// let features = 28 * 28
// let classes = 10

// let algorithm = 
//   fun (svm: KernelSupportVectorMachine)
//       (classInputs: float[][])
//       (classOutputs: int[]) (i: int) (j: int) ->

//       let strategy = SequentialMinimalOptimization (svm, classInputs, classOutputs)
//       strategy :> ISupportVectorMachineLearning

// let kernel = Accord.Statistics.Kernels.Linear()
// let svm = new MulticlassSupportVectorMachine(features, kernel, classes)
// let learner = MulticlassSupportVectorLearning(svm, images, labels)
// let config = SupportVectorMachineLearningConfigurationFunction(algorithm)

// learner.Algorithm <- config

// let error = learner.Run ()

// let validation = svmRead "validationsample.csv"

// validation 
// |> Array.averageBy (fun (l, i) -> if svm.Compute i = l then 1. else 0.)

