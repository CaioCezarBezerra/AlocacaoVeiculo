import { TestBed } from '@angular/core/testing';

import { PlanoAssistencia } from './plano-assistencia.service';

describe('PlanoAssistencia', () => {
  let service: PlanoAssistencia;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanoAssistencia);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
