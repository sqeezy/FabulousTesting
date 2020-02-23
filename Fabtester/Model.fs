namespace Fabtester

module Model =

  type Package =
    {
      Name : string
      LatestVersion : string
    }


  type AppData = 
    {
      Search : string
      Packages : Package array
      Updating : bool
    }
