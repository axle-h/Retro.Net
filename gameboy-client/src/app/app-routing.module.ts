import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import {DefaultComponent} from "./default/default.component";
import {GameboyComponent} from "./gameboy/gameboy.component";

const routes: Routes = [
  {path: "", pathMatch: "full", component: DefaultComponent},
  {path: "gameboy", component: GameboyComponent},
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
