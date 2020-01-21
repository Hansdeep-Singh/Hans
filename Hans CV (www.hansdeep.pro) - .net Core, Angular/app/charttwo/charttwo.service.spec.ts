import { TestBed } from '@angular/core/testing';

import { CharttwoService } from './charttwo.service';

describe('CharttwoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CharttwoService = TestBed.get(CharttwoService);
    expect(service).toBeTruthy();
  });
});
