import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { LcdComponent } from "./lcd.component";
import {GameBoyService} from "../game-boy.service";
import {VisibilityService} from "../visibility.service";

describe("LcdComponent", () => {
  let component: LcdComponent;
  let fixture: ComponentFixture<LcdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LcdComponent ],
      providers: [GameBoyService, VisibilityService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LcdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
