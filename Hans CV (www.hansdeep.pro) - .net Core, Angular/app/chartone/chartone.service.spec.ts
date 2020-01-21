import { TestBed } from '@angular/core/testing';

import { ChartoneService } from './chartone.service';

describe('ChartoneService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChartoneService = TestBed.get(ChartoneService);
    expect(service).toBeTruthy();
  });
});
