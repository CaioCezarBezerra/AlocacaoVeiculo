import { TestBed } from '@angular/core/testing';

import { EmpresaAssistencia, EmpresaAssistenciaService } from './empresa-assistencia.service';

describe('EmpresaAssistencia', () => {
  let service: EmpresaAssistenciaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpresaAssistenciaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
