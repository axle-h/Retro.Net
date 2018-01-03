import {Injectable} from "@angular/core";
import * as Rx from "rxjs/Rx";

const renderBufferTime = 1000;

/**
 * A service for determining whether the application is visible.
 * Since our primary concern is rendering the GameBoy LCD we tie this to the render loop.
 * We prefer to use the render loop over say the visibility event as some browsers,
 * notably chrome on Android, do not trigger this event when running in the background.
 */
@Injectable()
export class VisibilityService {
  private subject = new Rx.Subject<boolean>();
  private rendering = new Rx.Subject<boolean>();
  private isRendering: boolean;

  constructor() {
    this.render();

    this.rendering
      .throttleTime(renderBufferTime / 4)
      .bufferTime(renderBufferTime)
      .subscribe(values => {
        if (values.length === 0) {
          if (this.isRendering) {
            this.subject.next(false);
            this.isRendering = false;
          }
        } else {
          this.subject.next(true);
        }
      });
  }

  public visibilityStream(): Rx.Observable<boolean> {
    return this.subject.asObservable().distinctUntilChanged();
  }

  public isVisible(): boolean  { return this.isRendering; }

  render = () => {
    this.isRendering = true;
    this.rendering.next(true);
    requestAnimationFrame(this.render);
  }
}
