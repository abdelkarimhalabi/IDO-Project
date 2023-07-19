import { TestBed } from '@angular/core/testing';

import { SearcheService } from './searche.service';

describe('SearcheService', () => {
  let service: SearcheService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearcheService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
