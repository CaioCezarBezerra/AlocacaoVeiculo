import { TestBed } from '@angular/core/testing';

import { VeiculoAssistencia } from './vinculo-veiculos.service';

describe('VeiculoAssistencia', () => {
  let service: VeiculoAssistencia;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VeiculoAssistencia);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
