namespace Fabtester

open FSharp.Data

module NugetService =
  type NugetStuff = JsonProvider<"https://api.nuget.org/v3/index.json">

  let search text =
    async{
      // let! html = Http.AsyncRequestString("https://api.nuget.org/v3/index.json")
      return NugetStuff.GetSample()
    }
