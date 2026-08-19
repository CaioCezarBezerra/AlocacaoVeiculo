import { TestBed } from '@angular/core/testing';

import { GrupoVeiculo } from './grupo-veiculo.service';

describe('GrupoVeiculo', () => {
  let service: GrupoVeiculo;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GrupoVeiculo);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
