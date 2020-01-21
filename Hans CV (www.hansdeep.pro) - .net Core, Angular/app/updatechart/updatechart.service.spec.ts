import { TestBed } from '@angular/core/testing';

import { UpdatechartService } from './updatechart.service';

describe('UpdatechartService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UpdatechartService = TestBed.get(UpdatechartService);
    expect(service).toBeTruthy();
  });
});
