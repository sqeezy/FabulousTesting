// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace Fabtester

open System
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

module App =
    type ModelObject =
        { Id : int }

    type Page =
        | Home
        | About

    type Model =
        { CurrentPage : Page
          Home : Home.Model }

    type Msg =
        | ShowPage of Page
        | UpdateStartTime

    let initModel =
        { CurrentPage = Home
          Home = Home.init() }

    let init() = initModel, Cmd.none
    let updateStartTime model =
        { model with Home =
                         { model.Home with TimeFromStart =
                                               (DateTime.Now
                                                - model.Home.StartTime) } }

    let updateStartTimeTick =
        async {
            do! Async.Sleep 500
            return UpdateStartTime
        }
        |> Cmd.ofAsyncMsg

    let update msg model =
        match msg with
        | ShowPage p ->
            let newModel = { model with CurrentPage = p }
            match p with
            | Home -> newModel, updateStartTimeTick
            | _ -> newModel, Cmd.none
        | UpdateStartTime ->
            match model.CurrentPage with
            | Home -> (model |> updateStartTime), updateStartTimeTick
            | _ -> model, Cmd.none

    let view (model : Model) dispatch =
        View.ContentPage(content = (match model.CurrentPage with
                                    | Home -> Home.view model.Home
                                    | About -> About.view),
                         title = match model.CurrentPage with
                                 | Home -> "Home"
                                 | About -> "About")
            .ToolbarItems([ View.ToolbarItem
                                (text = "Home",
                                 command = (fun () -> dispatch (ShowPage Home)))

                            View.ToolbarItem
                                (text = "About",
                                 command = (fun () -> dispatch (ShowPage About))) ])

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App() as app =
    inherit Application()
    let runner =
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif

        |> XamarinFormsProgram.run app
#if DEBUG
// Uncomment this line to enable live update in debug mode.
// See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
//
//do runner.EnableLiveUpdate()
#endif
