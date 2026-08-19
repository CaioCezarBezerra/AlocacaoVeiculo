import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Veiculos {
  id: number;
  modelo: string;
  placa: string;
  grupoId: number;
  grupoNome: string;
}

export interface CriarVeiculos {
  modelo: string;
  placa: string;
  grupoId: number;
}

export interface AtualizarVeiculos {
  modelo: string;
  placa: string;
  grupoId: number;
}

@Injectable({
  providedIn: 'root'
})
export class VeiculoService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:7005/api/Veiculos';
  /*----------------------------------LISTAR----------------------------------------------------------*/
  listar(): Observable<Veiculos[]> {
    return this.http.get<Veiculos[]>(`${this.apiUrl}/ListarVeiculo`);
  }
  /*----------------------------------ATUALIZAR----------------------------------------------------------*/
  atualizar(id: number, veiculo: AtualizarVeiculos): Observable<AtualizarVeiculos> {
    return this.http.put<AtualizarVeiculos>(`${this.apiUrl}/AtualizarVeiculos/${id}`, veiculo);
  }
  /*----------------------------------DELETAR----------------------------------------------------------*/
  deletar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeletarVeiculos/${id}`);
  }
  criarVeiculos(veiculo: CriarVeiculos): Observable<CriarVeiculos>{
    return this.http.post<Veiculos>(`${this.apiUrl}/CriaVeiculo`, veiculo)
  }
}