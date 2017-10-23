import { TestBed, inject } from '@angular/core/testing';

import { GameboyService } from './gameboy.service';

describe('GameboyService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GameboyService]
    });
  });

  it('should be created', inject([GameboyService], (service: GameboyService) => {
    expect(service).toBeTruthy();
  }));
});
