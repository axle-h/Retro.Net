import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { GameboyService } from "./gameboy.service";
import { FooterComponent } from "./footer/footer.component";
import { LcdComponent } from "./lcd/lcd.component";
import { DefaultComponent } from "./default/default.component";
import { GameboyComponent } from "./gameboy/gameboy.component";
import { NavComponent } from "./nav/nav.component";


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
    AppRoutingModule,
    NgbModule.forRoot(),
    FormsModule
  ],
  providers: [GameboyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
