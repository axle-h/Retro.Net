import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { By } from "@angular/platform-browser";
import { DefaultComponent } from "./default.component";
import {MockComponent} from "ng2-mock-component";

describe("DefaultComponent", () => {
  let component: DefaultComponent;
  let fixture: ComponentFixture<DefaultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ RouterTestingModule ],
      declarations: [
        DefaultComponent,
        MockComponent({ selector: "gb-lcd", inputs: ["maxScale"] })
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should render gameboy router url", () => {
    const href = fixture.debugElement.query(By.css("#gb-join-in-link")).nativeElement.getAttribute("href");
    expect(href).toEqual("/gameboy");
  });
});
