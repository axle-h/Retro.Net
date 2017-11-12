import { TestBed, inject } from "@angular/core/testing";

import { GameboyService } from "./gameboy.service";
import {VisibilityService} from "./visibility.service";

describe("GameboyService", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GameboyService, VisibilityService]
    });
  });

  it("should be created", inject([GameboyService], (service: GameboyService) => {
    expect(service).toBeTruthy();
  }));
});
