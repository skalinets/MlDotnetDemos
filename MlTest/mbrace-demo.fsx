#load "ThespianCluster.fsx"
//#load "AzureCluster.fsx"
//#load "AwsCluster.fsx"

// Note: Before running, choose your cluster version at the top of this script.
// If necessary, edit AzureCluster.fsx to enter your connection strings.

open System
open System.IO
open MBrace.Core
open MBrace.Core.BuilderAsyncExtensions
open MBrace.Flow

type Observation = { Label: string; Pixels: int[] }

let toObservation (csvData: string) =
  let columns = csvData.Split(',')
  let label = columns.[0]
  let pixels = columns.[1..] |> Array.map int
  { Label = label; Pixels = pixels }

let optimizedDistance (pixels1:int[], pixels2:int[]) =
  let dim = pixels1.Length
  let mutable dist = 0
  for i in 0..(dim-1) do
    let x = pixels1.[i] - pixels2.[i]
    dist <- dist + x * x
  dist

let train (trainingSet:Observation[]) dist =
  let classify (pixels:int[]) =
    trainingSet
    |> Array.minBy (fun x -> dist (x.Pixels, pixels))
    |> fun x -> x.Label
  classify

// Initialize client object to an MBrace cluster
let cluster = Config.GetCluster() 
let fullpath = @"C:\Users\admin\work\aiconf-workshop\Data\trainingsample.csv"

let large = 
  CloudFile.Upload(fullpath, "data/large.cvs")
  |> cluster.RunLocally

let cloudValidation =
  cloud {
    let! data = CloudFile.ReadAllLines large.Path
    let training = data.[1..2000] |> Array.map toObservation
    let validation = data.[2000..] |> Array.map toObservation
    let model = train training optimizedDistance
    let! correct = 
      validation
      |> CloudFlow.OfArray
      |> CloudFlow.withDegreeOfParallelism 4
      |> CloudFlow.averageBy (fun e -> if model e.Pixels = e.Label then 1. else 0.)
    return correct }
 
     

cloudValidation |> cluster.Run