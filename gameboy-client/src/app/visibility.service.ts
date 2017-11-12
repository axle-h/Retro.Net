import {Inject, Injectable} from "@angular/core";
import {DOCUMENT} from "@angular/common";
import * as Rx from "rxjs/Rx";

@Injectable()
export class VisibilityService {
  private subject = new Rx.Subject<boolean>();
  private isHidden: () => boolean;

  constructor(@Inject(DOCUMENT) document) {
    if (typeof document.addEventListener === "undefined") {
      console.log("No event listener API support.");
      return;
    }

    // Set the name of the hidden property and the change event for visibility
    let visibilityChange: string;
    if (typeof document.hidden !== "undefined") { // Opera 12.10 and Firefox 18 and later support
      this.isHidden = () => document.hidden;
      visibilityChange = "visibilitychange";
    } else if (typeof document.msHidden !== "undefined") {
      this.isHidden = () => document.msHidden;
      visibilityChange = "msvisibilitychange";
    } else if (typeof document.webkitHidden !== "undefined") {
      this.isHidden = () => document.webkitHidden;
      visibilityChange = "webkitvisibilitychange";
    } else {
      console.log("No Page Visibility API support.");
      return;
    }

    // Arrow function required for 'this'.
    const handleVisibilityChange = () => {
      this.subject.next(!this.isHidden());
    };

    // Handle page visibility change
    document.addEventListener(visibilityChange, handleVisibilityChange, false);
  }

  public visibilityStream(): Rx.Observable<boolean> { return this.subject.asObservable(); }

  public isVisible(): boolean  { return !this.isHidden || !this.isHidden(); }
}
