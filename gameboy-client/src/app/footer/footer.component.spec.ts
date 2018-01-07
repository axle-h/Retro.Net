import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { FooterComponent } from "./footer.component";

describe("FooterComponent", () => {
  let component: FooterComponent;
  let fixture: ComponentFixture<FooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FooterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it(`should have all social links`, async(() => {
    const app = fixture.debugElement.componentInstance;
    const providers = app.links.map(x => x.name);
    expect(providers).toEqual(["twitter", "github", "linkedin", "google-plus"]);
  }));

  it("should render a footer tag", async(() => {
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector("footer")).toBeTruthy();
  }));
});
