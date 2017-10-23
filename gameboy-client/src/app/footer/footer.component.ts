import { Component } from "@angular/core";
import {SocialLink} from "./social-link";

@Component({
  selector: "gb-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.scss"]
})
export class FooterComponent {

  links: SocialLink[];

  constructor() {
    this.links = [
      new SocialLink("twitter", "https://twitter.com/axle_h"),
      new SocialLink("github", "https://github.com/axle-h"),
      new SocialLink("linkedin", "https://www.linkedin.com/pub/alex-haslehurst/59/72a/b88/"),
      new SocialLink("google-plus", "https://plus.google.com/105632375776338945680"),
      new SocialLink("facebook", "https://www.facebook.com/alex.haslehurst")
    ];
  }
}
