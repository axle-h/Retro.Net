import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { GameboyComponent } from "./gameboy.component";
import {MockComponent} from "ng2-mock-component";
import {FormsModule} from "@angular/forms";
import {GameboyService} from "../gameboy.service";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";

describe("GameboyComponent", () => {

  let component: GameboyComponent;
  let fixture: ComponentFixture<GameboyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, NgbModule.forRoot() ],
      declarations: [
        GameboyComponent,
        MockComponent({ selector: "gb-lcd", inputs: ["maxScale"] })
      ],
      providers: [GameboyService]
    })
    .compileComponents();
  }));

  beforeEach((() => {
    fixture = TestBed.createComponent(GameboyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
