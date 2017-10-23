import { Component, OnInit } from "@angular/core";

@Component({
  selector: "gb-nav",
  templateUrl: "./nav.component.html"
})
export class NavComponent implements OnInit {

  public navbarCollapsed = true;

  constructor() { }

  ngOnInit() {
  }

}
