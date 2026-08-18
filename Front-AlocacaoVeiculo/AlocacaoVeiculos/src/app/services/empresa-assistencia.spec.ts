import { TestBed } from '@angular/core/testing';

import { EmpresaAssistencia } from './empresa-assistencia.service';

describe('EmpresaAssistencia', () => {
  let service: EmpresaAssistencia;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpresaAssistencia);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
