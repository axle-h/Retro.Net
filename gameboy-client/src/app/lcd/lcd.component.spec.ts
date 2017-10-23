import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { LcdComponent } from "./lcd.component";

describe("LcdComponent", () => {
  let component: LcdComponent;
  let fixture: ComponentFixture<LcdComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LcdComponent ]
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
