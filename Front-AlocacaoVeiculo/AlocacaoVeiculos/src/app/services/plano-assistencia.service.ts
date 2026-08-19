import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs';



export interface PlanoAssistencia {
  plano: string;
  id: number
  descricao: string;
  valorCobertura: number;
  empresaId: number;
}


export interface CriaPlano {
  empresaId: number;
  plano: string;
  valorCobertura: number;
  descricao: string;
}


export interface AtualizarPlano {
  empresaId: number;
  plano: string;
  valorCobertura: number;
  descricao: string;
}


export interface DeletarPlano{
  id: number;
  plano: string;
  empresaId: number;
  empresaNome: string;
  valorCobertura: number;
  descricao: string;
}



//@Injectable({
//providedIn: 'root'
//})


@Service()
export class PlanoAssistencia {
  private readonly http = inject(HttpClient)

  private readonly apiUrl = 'http://localhost:7005/api/PlanosAssistencias'

  /*----------------------------------LISTAR----------------------------------------------------------*/
  listar(): Observable<PlanoAssistencia[]> {
    return this.http.get<PlanoAssistencia[]>(`${this.apiUrl}/ListarPlano`);
  }

  /*----------------------------------DELETAR----------------------------------------------------------*/
  deletar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeletarPlanos/${id}`);

  }
  /*----------------------------------ATUALIZAR----------------------------------------------------------*/
  atualizar(id: number, plano: AtualizarPlano): Observable<AtualizarPlano> {
    return this.http.put<AtualizarPlano>(`${this.apiUrl}/AtualizarPlanos/${id}`, plano);
  }
  /*----------------------------------CRIAR----------------------------------------------------------*/
  criarPlano(plano: CriaPlano): Observable<CriaPlano> {
    return this.http.post<CriaPlano>(`${this.apiUrl}/CriarPlanos`, plano)
  }

}
