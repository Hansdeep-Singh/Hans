import { TestBed } from '@angular/core/testing';

import { ChartthreeService } from './chartthree.service';

describe('ChartthreeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChartthreeService = TestBed.get(ChartthreeService);
    expect(service).toBeTruthy();
  });
});
