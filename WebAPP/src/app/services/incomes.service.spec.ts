import { TestBed } from '@angular/core/testing';

import { IncomesService } from './incomes.service';

describe('IncomesService', () => {
  let service: IncomesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IncomesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
