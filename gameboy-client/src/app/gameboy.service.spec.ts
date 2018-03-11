import { TestBed, inject } from "@angular/core/testing";

import { GameBoyService } from "./game-boy.service";
import {VisibilityService} from "./visibility.service";

describe("GameBoyService", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GameBoyService, VisibilityService]
    });
  });

  it("should be created", inject([GameBoyService], (service: GameBoyService) => {
    expect(service).toBeTruthy();
  }));
});
