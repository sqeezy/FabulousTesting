module Home

open System
open Fabulous.XamarinForms

type Model =
    { StartTime : DateTime
      TimeFromStart : TimeSpan }

let init() =
    { StartTime = DateTime.Now
      TimeFromStart = TimeSpan.Zero }

let view model =
    View.StackLayout
        (children = [ View.Label(text = model.StartTime.ToString())
                      View.Label(text = model.TimeFromStart.ToString()) ])
