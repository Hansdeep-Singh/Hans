import { TestBed } from '@angular/core/testing';

import { ChartfourService } from './chartfour.service';

describe('ChartfourService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChartfourService = TestBed.get(ChartfourService);
    expect(service).toBeTruthy();
  });
});
