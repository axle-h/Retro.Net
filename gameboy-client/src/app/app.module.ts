import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { GameBoyService } from "./game-boy.service";
import { FooterComponent } from "./footer/footer.component";
import { LcdComponent } from "./lcd/lcd.component";
import { DefaultComponent } from "./default/default.component";
import { GameboyComponent } from "./gameboy/gameboy.component";
import { NavComponent } from "./nav/nav.component";
import {VisibilityService} from "./visibility.service";


@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    LcdComponent,
    DefaultComponent,
    GameboyComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    NgbModule.forRoot()
  ],
  providers: [GameBoyService, VisibilityService],
  bootstrap: [AppComponent]
})
export class AppModule { }
