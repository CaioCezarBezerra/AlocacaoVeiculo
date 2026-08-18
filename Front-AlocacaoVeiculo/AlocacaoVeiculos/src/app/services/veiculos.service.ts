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

export interface VeiculoCriar {
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

  listar(): Observable<Veiculos[]> {
    console.log("passei aqui")
    return this.http.get<Veiculos[]>(
      `${this.apiUrl}/ListarVeiculo`

    );
   
  }

  atualizar(
    id: number,
    veiculo: AtualizarVeiculos
  ): Observable<AtualizarVeiculos> {

    return this.http.put<AtualizarVeiculos>(
      `${this.apiUrl}/AtualizarVeiculos/${id}`,
      veiculo
    );
  }

  deletar(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/DeletarVeiculos/${id}`
    );
  }
}